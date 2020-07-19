using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PH2 : MonoBehaviour
{

    public float health = 100f;

    bool isShielded;

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
            return isShielded = true;
        }
        set
        {
            isShielded = value;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(!isShielded)
        {
            health -= damage;

            print("Current Charactewr Health is: " + health);

            if(health <= 0)
            {
                anim.SetBool("Death", true);

                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
                {
                    Destroy(this.gameObject, 1f);
                }


            }



        }
    }



}
