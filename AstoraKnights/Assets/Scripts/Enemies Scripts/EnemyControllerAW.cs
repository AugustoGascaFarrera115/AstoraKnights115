using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAW : MonoBehaviour
{
    public Transform[] pointsList;
    int walkIndex = 0;


    Animator anim;

    Transform playerTarget;
    NavMeshAgent navMesh;
    GameObject pl;


    float walkDistance = 8f;
    float attackDistance = 2f;

    float currentAttackTime = 0f;
    float waitAttackTime = 1f;


    Vector3 nextDestination = Vector3.zero;


    // Start is called before the first frame update
    void Awake()
    {
        //pointsList = new Transform[transform.childCount];
        anim = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
        pl = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAIBehaviour();
    }


    void EnemyAIBehaviour()
    {


        if(playerTarget != null)
        {
            float distanceBetweenEnemyandPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceBetweenEnemyandPlayer > walkDistance || pl == null)
            {
                if (navMesh.remainingDistance <= 0.5f)
                {
                    navMesh.isStopped = false;

                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                    anim.SetInteger("Atk", 0);


                    int randomPosition = Random.Range(0, 4);
                    nextDestination = pointsList[randomPosition].position;
                    navMesh.SetDestination(nextDestination);


                    if (randomPosition == pointsList.Length - 1)
                    {
                        randomPosition = 0;
                    }
                    else
                    {
                        randomPosition++;
                    }


                }
            }
            else
            {
                if (distanceBetweenEnemyandPlayer > attackDistance)
                {
                    navMesh.isStopped = false;

                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);
                    anim.SetInteger("Atk", 0);

                    navMesh.SetDestination(playerTarget.position);


                }
                else
                {
                    navMesh.isStopped = true;

                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);

                    Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);


                    if (currentAttackTime >= waitAttackTime)
                    {
                        int atkRange = Random.Range(1, 3);
                        anim.SetInteger("Atk", atkRange);
                        currentAttackTime = 0f;
                    }
                    else
                    {
                        anim.SetInteger("Atk", 0);
                        currentAttackTime++;
                    }


                }
            }

        }



    }

}
