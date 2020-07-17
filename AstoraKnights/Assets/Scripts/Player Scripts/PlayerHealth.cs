using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;

    public bool isShield;

    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
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

            if(health <= 0)
            {
                anim.SetBool("Death", true);


                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                {
                    Destroy(this.gameObject, 1f);
                }

            }
        }
    }



}
