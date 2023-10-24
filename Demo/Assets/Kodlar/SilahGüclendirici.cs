using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilahGüclendirici : MonoBehaviour
{
    PlayerAttack data;
    float timer = 0f;
    bool timerBaslat = false;
   // bool buffVar = false;
    [SerializeField]int hasarArtisi = 1;
    private void Awake()
    {
        data = FindObjectOfType<PlayerAttack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timerBaslat = true;
            BuffAl();
            Destroy(gameObject);
        }
    }
    void BuffAl()
    {
        data.damageArttir(hasarArtisi);
      /*  int suankiHasar = data.damage;
        FindObjectOfType<AttackArea>().setDamage(suankiHasar++);*/
       /* if (buffVar == false)
        {
            data.damageAzalt(hasarArtisi);
            // FindObjectOfType<AttackArea>().setDamage(suankiHasar--);
        }*/
    }

    void Update()
    {
        if (timerBaslat == true)
        {
           // buffVar = true;
            timer += Time.deltaTime;
            Debug.Log("Saniye"+timer);
            if (timer >= 30)
            {
                timerBaslat = false;
                Debug.Log("30 saniye doldu ve timer durdu.");
                //buffVar = false;
                timer = 0f;

            }
        }


    }
}