using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaToplama : MonoBehaviour
{

    [SerializeField] int skorArtt�r = 100;
    [SerializeField] AudioClip paraSesi;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindAnyObjectByType<GameSession>().AddToScore(skorArtt�r);
            AudioSource.PlayClipAtPoint(paraSesi, Camera.main.transform.position);

            Destroy(gameObject);


        }
    }
}
    

