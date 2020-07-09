using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    IDLE,
    WALK,
    RUN,
    PAUSE,
    GOBACK,
    ATTACK,
    DEATH
}


public class EnemyController : MonoBehaviour
{

    private float attack_distance = 1.5f;
    private float alert_attack_distance = 8f;
    private float followDistance = 15f;

    private float enemyToPlayerDistance;

    [HideInInspector]
    public EnemyState enemy_CurrentState = EnemyState.IDLE;

    private EnemyState enemy_LastState = EnemyState.IDLE;

    private Transform playerTarget;
    private Vector3 initialPosition;

    private float move_Speed = 2f;
    private float walk_Speed = 1f;

    private CharacterController characterController;
    private Vector3 where_ToMove = Vector3.zero;

    //Attack variables
    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private Animator animator;
    private bool finished_Animation = true;
    private bool finished_Movement = true;

    private NavMeshAgent navAgent;
    private Vector3 where_ToNavigate;

    //HEALTH SCRIPT

    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        initialPosition = transform.position;
        where_ToNavigate = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if health is <= 0 we will set the sate to death
        if (enemy_CurrentState != EnemyState.DEATH)
        {

            enemy_CurrentState = SetEnemyState(enemy_CurrentState,enemy_LastState,enemyToPlayerDistance);

            if (finished_Movement)
            {
                GetStateControl(enemy_CurrentState);
            }
            else
            {
                if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    finished_Movement = true;
                } else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Atk1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
                {
                    animator.SetInteger("Atk", 0);
                }
            }
        }
        else
        {
            animator.SetBool("Death",true);
            characterController.enabled = false;
            navAgent.enabled = false;

            if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(this.gameObject, 2f);
            }

        }
    }


    EnemyState SetEnemyState(EnemyState curState, EnemyState lastState, float enemyToPlayerDistance)
    {

        enemyToPlayerDistance = Vector3.Distance(transform.position, playerTarget.position);

        float initialDistance = Vector3.Distance(initialPosition, transform.position);


        if (initialDistance > followDistance)
        {
            lastState = curState;
            curState = EnemyState.GOBACK;
        }
        else if (enemyToPlayerDistance <= attack_distance)
        {
            lastState = curState;
            curState = EnemyState.ATTACK;
        } else if (enemyToPlayerDistance >= alert_attack_distance && lastState == EnemyState.PAUSE || lastState == EnemyState.ATTACK)
        {
            lastState = curState;
            curState = EnemyState.PAUSE;
        } else if (enemyToPlayerDistance <= alert_attack_distance && enemyToPlayerDistance > attack_distance)
        {
            if (curState != EnemyState.GOBACK || lastState == EnemyState.WALK)
            {
                lastState = curState;
                curState = EnemyState.PAUSE;
            }
        } else if (enemyToPlayerDistance > alert_attack_distance && lastState != EnemyState.GOBACK && lastState != EnemyState.PAUSE)
        {
            lastState = curState;
            curState = EnemyState.WALK;
        }

        return curState;
    }


    void GetStateControl(EnemyState curState)
    {
        if(curState != EnemyState.ATTACK)
        {
            Vector3 targetPosition = new Vector3(playerTarget.position.x,transform.position.y,playerTarget.position.z);

           if(Vector3.Distance(transform.position,targetPosition) >= 2.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);

                navAgent.SetDestination(targetPosition);

                

            }



        }else if(curState == EnemyState.ATTACK)
        {
            animator.SetBool("Run", false);
            where_ToMove.Set(0f, 0f, 0f);

            navAgent.SetDestination(transform.position);


            Vector3 target_temporal = new Vector3(playerTarget.position.x - transform.position.x, 0, playerTarget.position.z - transform.position.z);

            Quaternion playerRot = Quaternion.LookRotation(target_temporal);


            transform.rotation = Quaternion.Slerp(transform.rotation,playerRot,5f * Time.deltaTime);

            if(currentAttackTime >= waitAttackTime)
            {
                int atkRange = Random.Range(1,3);

                animator.SetInteger("Atk", atkRange);
                finished_Animation = false;
                currentAttackTime = 0f;

            }
            else
            {
                animator.SetInteger("Atk", 0);
                currentAttackTime += Time.deltaTime;
            }
        }else if(curState == EnemyState.GOBACK)
        {
            animator.SetBool("Run", true);

            Vector3 targetPosition = new Vector3(initialPosition.x,transform.position.y,initialPosition.z);

            navAgent.SetDestination(targetPosition);

            if(Vector3.Distance(targetPosition,initialPosition) <= 3.5f)
            {
                enemy_LastState = curState;
                curState = EnemyState.WALK;
            }

        }else if(curState == EnemyState.WALK)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);

            if(Vector3.Distance(transform.position,where_ToNavigate) <= 2f)
            {
                where_ToNavigate.x = Random.Range(initialPosition.x - 5f,initialPosition.x + 5f);
                where_ToNavigate.z = Random.Range(initialPosition.z - 5f,initialPosition.z + 5f);
                
            }
            else
            {
                navAgent.SetDestination(where_ToNavigate);
               
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            where_ToMove.Set(0f, 0f, 0f);
            navAgent.isStopped = true;
        }

        

    }


    private void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
    }
}
