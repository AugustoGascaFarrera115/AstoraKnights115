using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;

    public bool isShield;

    Animator anim,otheranim;


    GameObject animenemy;


    Image healthImage;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        animenemy = GameObject.FindGameObjectWithTag("Enemy");
        otheranim = animenemy.GetComponent<Animator>();


        healthImage = GameObject.Find("Health Icon").GetComponent<Image>();
       

    }


    public bool Shielded
    {
        get
        {
            return isShield;
        }
        set
        {
            isShield = value;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float damage)
    {
        

        if(!isShield)
        {
            health -= damage;

            print("Current Player's Health : " + health);

            healthImage.fillAmount = health / 100f;

            if(health <= 0)
            {
                //otheranim.SetBool("Walk", true);
                anim.SetBool("Death", true);

                

                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                {
                    Destroy(this.gameObject, 1f);
                    
                }

            }
        }
    }


    public void HealPlayer(float healthAmount)
    {
        health += healthAmount;

        if(health > 100)
        {
            health = 100f;
            print("YOU CANNOT INCRESE YOUR LIFE");
        }

        healthImage.fillAmount = health / 100f;

    }



}
