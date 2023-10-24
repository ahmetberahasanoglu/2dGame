using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class solucan : MonoBehaviour
{
    Rigidbody2D düsmanRigid;
    [SerializeField] float düsmanHiz = 5f;
    Health karakterH;
    [SerializeField] int alinanHasar = 1;
    void Start()
    {
        düsmanRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //asis();
        Run();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            düsmanHiz = -düsmanHiz;
            dondur();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         * if (myBCollider.IsTouchingLayers(LayerMask.GetMask("platform")))
        {
            myAnim.SetBool("isJumping", false);
            LastOnGroundTime = Data.coyoteTime;
        }*/
        if (collision.tag == "Player")
        {
            karakterH = collision.GetComponent<Health>();
            karakterH.Damage(alinanHasar);
        }


    }
    /*  void asis()
      {
          if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
          {


          }
      }*/

        void Run()
    {
        düsmanRigid.velocity = new Vector2(düsmanHiz, 0f);
    }
    void dondur()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

