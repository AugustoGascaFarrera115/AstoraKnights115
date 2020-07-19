using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTM2 : MonoBehaviour
{

    public LayerMask enemyMask;
    public float radius = 1f;
    public float damageCount = 40f;
    public GameObject fireExplosion;


    EnemyHealth enemyHealth;
    bool collided;


    float tornadoSpeed = 5f;

    GameObject player;
    GameObject enemy;

    // Start is called before the first frame update
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();

        player = GameObject.FindGameObjectWithTag("Player");

        transform.rotation = Quaternion.LookRotation(player.transform.forward);

        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        MoveTornado();
        CheckForDamage();

    }

    public void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyMask);


        foreach (Collider c in hits)
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHealth>();

            collided = true;
        }


        if (collided)
        {
            enemyHealth.TakeDamage(damageCount);


            Vector3 temporalPosition = transform.position;
            temporalPosition.y += 1f;
            Instantiate(fireExplosion, temporalPosition, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }


    public void MoveTornado()
    {
        transform.Translate(Vector3.forward * (tornadoSpeed * Time.deltaTime));
    }



}
