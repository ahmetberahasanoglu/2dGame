using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class atesatma : MonoBehaviour
{
    Rigidbody2D rb;
    PolygonCollider2D poligon;
    [Header("Ates Gonderme")]
    [SerializeField]float atesAtmaHizi = 5f;
    PlayerMovement karakterYonu;
    float atesXHizi;
    private int damage = 3;

    void Start()
    {
        karakterYonu= FindAnyObjectByType<PlayerMovement>();
        poligon = GetComponent<PolygonCollider2D>();
        rb= GetComponent<Rigidbody2D>();
        atesXHizi = karakterYonu.transform.localScale.x * atesAtmaHizi;
    }
    void FixedUpdate()
    {
        atesAtma();
        
    }
    void atesAtma()
    {
        rb.velocity = new Vector2(atesXHizi, 0f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (poligon.IsTouchingLayers(LayerMask.GetMask("dusman")))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.Damage(damage);//hatalý
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
