using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{

    public float healAmount = 0f;


    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health < 100 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health > 0)
        {
            healAmount = 20f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health += healAmount;
            print("player health is " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health);

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health >= 100)
            {
                healAmount = 0f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health = 100;
                print("Sorry your max life is 100");
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
