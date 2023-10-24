using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    [SerializeField] GameObject ates;
    [SerializeField] Transform atesChild;
    Rigidbody2D rb;
    Vector2 xMove;
    Vector2 yMove;
   

    [SerializeField]float karakterHizi =5f;
    [SerializeField]float ziplamaHizi =5f;
    void Start()
    {
        
        rb= GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void FixedUpdate()
    {
        Run();
        Jump();
        Flip();

    }
    void Update()
    {
        Ates();
    }
    void Run()
    {
        xMove = new Vector2(Input.GetAxis("Horizontal")*karakterHizi, rb.velocity.y);
        Debug.Log(xMove);
        rb.velocity = xMove;
    }
    void Jump()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("platform")))
        {
            yMove = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * ziplamaHizi);
            rb.velocity = yMove;
        }
    }
    void Flip()
    {
        if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)//karakterin - yada + yönde x yönünde bir hýzý varsa 
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
      
    }
    void Ates()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& transform.localScale.x ==1f)
        {
            Instantiate(ates,atesChild.position,ates.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && transform.localScale.x == -1f)
        {
            ates.transform.rotation =new Vector3 (0f,0f,90f).ConvertTo<Quaternion>();
            Instantiate(ates, atesChild.position, ates.transform.rotation);
        }
    }
}
