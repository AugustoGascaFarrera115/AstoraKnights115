using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{

    public LayerMask enemyMask;
    public float radius = 0.10f;
    public float damageCount = 10f;

    EnemyHealth enemyHealth;
    bool collided;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyMask);


        foreach(Collider c in hits)
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHealth>();


            collided = true;


        }


        if(collided)
        {
            enemyHealth.TakeDamage(damageCount);
            enabled = false;
        }


    }
}
