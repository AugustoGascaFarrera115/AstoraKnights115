using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    public float damageAmount = 20f;


    private Transform playerTartget;
    Animator anim;
    bool finishedAttack = true;
    float damageDistance = 2f;


    private PlayerHealth playerHealth;

    NavMeshAgent navMesh;

    Vector3 nextDestination = Vector3.zero;

    public Transform[] walkPoints;


    // Use this for initialization
    void Start()
    {
        playerTartget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();


        playerHealth = playerTartget.GetComponent<PlayerHealth>();

        navMesh = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (finishedAttack)
        {
            //deal damage
            if (playerTartget)
            {
                DealDamage(CheckIfAttacking());

            }
            else if (playerTartget == null)
            {
                //anim.SetBool("Run", true);


                if (navMesh.remainingDistance <= 0.5f)
                {
                    //the false condition say that the enemy is walking and it will become in true the enemy has stopped
                    navMesh.isStopped = false;


                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                    anim.SetInteger("Atk", 0);

                    int randomPointsPosition = Random.Range(0, 4);
                    nextDestination = walkPoints[randomPointsPosition].position;
                    navMesh.SetDestination(nextDestination);



                    if (randomPointsPosition == walkPoints.Length - 1)
                    {
                        randomPointsPosition = 0;
                    }
                    else
                    {
                        randomPointsPosition++;
                    }


                    //nextDestination = walkPoints[walkIndex].position;
                    //navMesh.SetDestination(nextDestination);

                    //if (walkIndex == walkPoints.Length - 1)
                    //{
                    //    walkIndex = 0;
                    //}
                    //else
                    //{
                    //    walkIndex++;
                    //}

                }


            }





        }
        else
        {
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishedAttack = true;
            }
        }
    }


    bool CheckIfAttacking()
    {
        bool isAttacking = false;

        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Atk1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                isAttacking = true;
                finishedAttack = false;
            }
        }

        return isAttacking;
    }


    void DealDamage(bool isAttacking)
    {
        if (isAttacking)
        {
            if (Vector3.Distance(transform.position, playerTartget.position) <= damageDistance)
            {
                playerHealth.TakeDamage(damageAmount);
            }





        }
    }













}
