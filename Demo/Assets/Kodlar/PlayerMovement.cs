using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData Data;
    PlayerAttack atak;

    #region Variables
    public Rigidbody2D RB { get; private set; }
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }

    public float LastOnGroundTime { get; private set; }


    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    private bool isRunning;


    [SerializeField] Vector2 deathKick = new Vector2(5f, 4f);
    [SerializeField] Vector2 hasarKick = new Vector2(0, 4f);
    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }
    BoxCollider2D myBCollider;
    CapsuleCollider2D myCapsuleCollider;
    [SerializeField] GameObject ates;
    [SerializeField] Transform atesChild;
    Animator myAnim;
    GameSession gameSession;
    Health can;
    [Space(5)]
   
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        can= GetComponent<Health>();
        RB = GetComponent<Rigidbody2D>();
        myBCollider=GetComponent<BoxCollider2D>();
        myAnim=GetComponent<Animator>();
        atak= FindObjectOfType<PlayerAttack>();
        gameSession = GetComponent<GameSession>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;
    }
    private void Update()
    {
        if(RB.velocity.y>50)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        #endregion

        #region INPUT HANDLER
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        Ates();//unutma
        Die();
        
        if (_moveInput.x != 0)
        {
            CheckDirectionToFace(_moveInput.x > 0);
            myAnim.SetBool("isRunning", true);
            if (atak.attacking==true)
            {
                myAnim.SetBool("isRunning", false);
                myAnim.SetBool("isJumping", false);
            }
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }
            

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUpInput();
        }
        #endregion

     

        #region COLLISION CHECKS
        if (!IsJumping)
        {
            //Ground Check
            if (myBCollider.IsTouchingLayers(LayerMask.GetMask("platform")))
            {
                myAnim.SetBool("isJumping", false);
                LastOnGroundTime = Data.coyoteTime;
            }
        }
        #endregion

        #region JUMP CHECKS
        if (IsJumping && RB.velocity.y < 0)
        {
            IsJumping = false;
            _isJumpFalling = true;
        }

        if (LastOnGroundTime > 0 && !IsJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }

        //Jump
        if (CanJump() && LastPressedJumpTime > 0)
        {
            IsJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            Jump();
        }
        #endregion

        #region GRAVITY
        //Higher gravity if we've released the jump input or are falling
        if (RB.velocity.y < 0 && _moveInput.y < 0)
        {
            //Much higher gravity if holding down
            SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
            //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
        }
        else if (_isJumpCut)
        {
            //Higher gravity if jump button released
            SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
        }
        else if ((IsJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else if (RB.velocity.y < 0)
        {
            //Higher gravity if falling
            SetGravityScale(Data.gravityScale * Data.fallGravityMult);
            //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
        }
        else
        {
            //Default gravity if standing on a platform or moving upwards
            SetGravityScale(Data.gravityScale);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        //Handle Run
        if (!can.isAlive) { return; }
        Run(1);


    }

    #region INPUT CALLBACKS
    //Methods which whandle input detected in Update()
    public void OnJumpInput()
    {
        LastPressedJumpTime = Data.jumpInputBufferTime;
    }

    public void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }
    #endregion

    //MOVEMENT METHODS
    #region RUN METHODS
    private void Run(float lerpAmount)//koþma
    {
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((IsJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - RB.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        if (atak.attacking == false)
        {
            RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
    }

    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    #endregion

    #region JUMP METHODS
    private void Jump()
    {

        //Ensures we can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = Data.jumpForce;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        myAnim.SetBool("isRunning", false);
        myAnim.SetBool("isJumping", true);//anim
        
        #endregion
    }
    #endregion


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private bool CanJump()
    {
       
        return LastOnGroundTime > 0 && !IsJumping;  
    }


    private bool CanJumpCut()
    {
        return IsJumping && RB.velocity.y > 0;
    }

     void Ates()
    {
        if (Input.GetKeyDown(KeyCode.C) )
        {
            Instantiate(ates,atesChild.position,transform.rotation);
        }
        
    }
    public void DamageTepki()
    {
        RB.velocity = hasarKick;
    }
    void Die()
    {
        // if (myBCollider.IsTouchingLayers(LayerMask.GetMask("tuzak")))
        //var a = gameSession.playerCanlari;
        if (can.isAlive == false)
        {
            myAnim.SetTrigger("Dying");
            RB.velocity = deathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            //myBCollider.enabled = false;
            //  myCapsuleCollider.enabled = false;
        }





    }

    #endregion

}

