using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuzaklar : MonoBehaviour
{
    // Start is called before the first frame update
    Health karakterH;
    [SerializeField]int alinanHasar = 1;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<PlayerMovement>().DamageTepki();
        karakterH = collision.gameObject.GetComponent<Health>();
        karakterH.Damage(alinanHasar);
    }

    
}
