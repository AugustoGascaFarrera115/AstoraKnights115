using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    Animator anim;
    private float enemyHealth = 100f;


    Image healthImage;


    private void Awake()
    {
        anim = GetComponent<Animator>();

        if(tag == "Boss")
        {
            healthImage = GameObject.Find("Health Foregroung Boss").GetComponent<Image>();
        }
        else
        {
            healthImage = GameObject.Find("Health Foreground Enemy").GetComponent<Image>();
        }



    }


    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;


        healthImage.fillAmount = enemyHealth / 100f;


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
