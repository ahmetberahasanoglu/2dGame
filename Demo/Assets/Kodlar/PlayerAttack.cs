using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    public int damage=2;
    public bool attacking = false;
    private float timeToAttack = 0.35f;
    private float timer = 0f;
    Animator saldýrý;
    void Start()
    {
        saldýrý = GetComponent<Animator>();
        attackArea = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if(attacking)
        {
            saldýrý.SetBool("isRunning", false);
            saldýrý.SetBool("isJumping", false);
            saldýrý.SetBool("isAttacking", true);
            timer += Time.deltaTime;
            if(timer >=timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
        else
        {
            saldýrý.SetBool("isAttacking", false);
        }
    }
    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        

    }
    public void damageArttir(int damage1)
    {
        damage = damage + damage1;
    }
    public void damageAzalt(int damage1)
    {
        damage = damage - damage1;
    }
}
