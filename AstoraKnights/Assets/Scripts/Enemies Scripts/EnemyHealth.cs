﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    Animator anim;
    private float enemyHealth = 100f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;



        print("Curemt enemy health: " + enemyHealth);

        if(enemyHealth <= 0)
        {
            anim.SetBool("Death", true);

            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                Destroy(this.gameObject, 0.5f);
            }


        }

    }
}
