﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator anim;
    private CharacterController chararacterController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool canMove;
    private bool finished_Movement = true;

    private Vector3 target_Position = Vector3.zero;
    private Vector3 player_Move = Vector3.zero;

    private float player_toPointDistance;

    private float gravity = 9.8f;
    private float height;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        chararacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();

        chararacterController.Move(player_Move);
    }


    void MoveThePlayer()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //calculate where is the position in the space once click event
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;


            if(Physics.Raycast(ray,out rayHit))
            {
                if(rayHit.collider is TerrainCollider)
                {
                    player_toPointDistance = Vector3.Distance(transform.position, rayHit.point);

                    if(player_toPointDistance >= 1.0f)
                    {
                        canMove = true;
                        target_Position = rayHit.point;
                    }

                }
            }

           
        }

        if (canMove)
        {

            anim.SetFloat("Walk", 1.0f);

            Vector3 target_temporal = new Vector3(target_Position.x, transform.position.y, target_Position.z);


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target_temporal - target_Position), 15.0f * Time.deltaTime);

                player_Move = transform.forward * moveSpeed * Time.deltaTime;


                //0.5f means the distance
                if (Vector3.Distance(transform.position, target_Position) <= 0.5f)
                {
                    canMove = false;
                }
           
        }
        else
        {
            player_Move.Set(0f, 0f, 0f);
            anim.SetFloat("Walk", 0f);
        }
    }
}
