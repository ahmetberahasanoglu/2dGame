using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    //GameSession gameSession;
    public int Can = 5;
    public bool isAlive = true;
    bool hasarAlindi = false;
    float timer;
    [SerializeField] float olumsuzSure=0.8f;
    bool timerBasla = false;
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
        //karakterin caný azalýnca animasyon girsin
        if(Can>0)
        {
            Can -= amount;
            hasarAlindi = true;

            //throw new System.ArgumentOutOfRangeException("negatif hasar alamazsýn");
        }
        if (bCollision.tag == "Player" && hasarAlindi==true)
        {
            timerBasla=true;
            //timer baslat
            collision.enabled = false;//Daha iyi bir çözüm bul!! collider'ý kapatmak yerine damage Fonksiyonunu iþlevsiz hale getir

            //karakter 10 salise olumsuz olsun
            if (timer == olumsuzSure)
            {
                collision.enabled = true;
                //Buraya animasyon ekle(karakteri kýrmýzý/beyaz yap)
                hasarAlindi = false;
                timerBasla = false;
                timer = 0;
            }
            
            /*if (bir kosul bul)//Eðer hasar alýrsak timer baþlat ve 15 saliseliðine timer çalýþsýn o timer çalýþtýðý süre boyunca karakterin capsule collider'ý kapalý kalsýn
            {
                timerBaslat = true;
            }*/
            
        }


        // gameSession.TakeLife(amount);//bozuk

    }
    public void Update()
    {
        if(timerBasla==true)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
           /* if(timer > olumsuzSure)
            {
                timer = 0;
            }*/
        }
        else
        {
            timer = 0;
        }
       
       
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
             throw new System.ArgumentOutOfRangeException("negatif heal alamazsýn");
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
            Debug.Log("OLDUM");
        }
        else
        {
            Destroy(collision.gameObject);
        }
       
             
    }
}
