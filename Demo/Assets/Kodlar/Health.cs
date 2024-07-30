using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //GameSession gameSession;
    public int Can = 5;
    public bool isAlive = true;
    bool hasarAlindi = false;
    float timer;
    [SerializeField] float olumsuzSure=0.8f;
   // bool timerBasla = false; BU OLACAK!!!
    // private int maxHp = 100;
    CapsuleCollider2D collision;
    BoxCollider2D bCollision;
    

    void Awake()
    {

        collision = GetComponent<CapsuleCollider2D>();
        bCollision = GetComponent<BoxCollider2D>();
       // gameSession = GetComponent<GameSession>();
    }
   
    public void Damage(int amount)
    {
        //karakterin can� azal�nca animasyon girsin
        if(Can>0)
        {
            Can -= amount;
            hasarAlindi = true;

            //throw new System.ArgumentOutOfRangeException("negatif hasar alamazs�n");
        }
        if (bCollision.tag == "Player" && hasarAlindi==true)
        {
            //  timerBasla=true;   BU OLACAK!!!
            FindObjectOfType<GameSession>().TakeLife();

            
          //  collision.enabled = false;//Daha iyi bir ��z�m bul!! collider'� kapatmak yerine damage Fonksiyonunu i�levsiz hale getir

            //karakter 10 salise olumsuz olsun
            if (timer == olumsuzSure)
            {
                collision.enabled = true;
                //Buraya animasyon ekle(karakteri k�rm�z�/beyaz yap)
                hasarAlindi = false;
                //  timerBasla = false;    BU OLACAK!!!
                timer = 0;
            }
            
            /*if (bir kosul bul)//E�er hasar al�rsak timer ba�lat ve 15 saliseli�ine timer �al��s�n o timer �al��t��� s�re boyunca karakterin capsule collider'� kapal� kals�n
            {
                timerBaslat = true;
            }*/
            
        }


        // gameSession.TakeLife(amount);//bozuk

    }
    public void Update()
    {
        
        /* if(timerBasla==true)
         {
             timer += Time.deltaTime;
             //Debug.Log(timer);
            /* if(timer > olumsuzSure)
             {
                 timer = 0;
             }
         }
         else BURAYI DUZELT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
         {
             timer = 0;
         }
        */

        if (Can <= 0)
        {
            Can = 0;
            Die();
        }
        
    }
    /* 
     * 
     * 
     * public void Heal(int amount)
     {
         if(amount<0)
         {
             throw new System.ArgumentOutOfRangeException("negatif heal alamazs�n");
         }
         bool wouldBeOverMaxHealth = health + amount > maxHp;
         if (wouldBeOverMaxHealth)
         {
             this.health = maxHp;
         }
         else
         {
             this.health += amount;
         }
     }*/

    private void Die()
    {
        if(collision.tag == "Player")
        {
            isAlive = false;
        }
        else
        {
            Destroy(collision.gameObject);
        }
       
             
    }
}
