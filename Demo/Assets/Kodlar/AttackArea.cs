using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    PlayerAttack data;

    void Awake()
    {
        data= GetComponentInParent<PlayerAttack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)//iki collidera da deðil yalnýzca capsulecollider'a vurmasýný sagla
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health health = collision.GetComponent<Health> ();
            health.Damage(data.damage);
            
        }
    }
   /* public int getDamage()
    {
        return data.damage;
    }
    public void setDamage(int damage)
    {
        this.damage = damage;
    }*/
}
