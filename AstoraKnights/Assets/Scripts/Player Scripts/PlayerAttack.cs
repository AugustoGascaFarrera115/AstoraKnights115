using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public Image fillWaitImage_1;
    public Image fillWaitImage_2;
    public Image fillWaitImage_3;
    public Image fillWaitImage_4;
    public Image fillWaitImage_5;
    public Image fillWaitImage_6;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };

    private Animator animator;
    private bool canAttack = true;

    private PlayerController playerMove;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        CheckToFade();
        CheckInput();

    }

    public void CheckInput()
    {
        if(animator.GetInteger("Atk") == 0)
        {
            playerMove.FinishedMovement = false;

            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                playerMove.FinishedMovement = true;
            }

        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMove.TargetPosition = transform.position;


            //fade images 0 meaning image thats at index 0 e.g the first image
            if(playerMove.FinishedMovement && fadeImages[0] != 1 && canAttack)
            {
                fadeImages[0] = 1;
                animator.SetInteger("Atk", 1);
            }

        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
             if (playerMove.FinishedMovement && fadeImages[1] != 1 && canAttack)
            {
                fadeImages[1] = 1;
                animator.SetInteger("Atk", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (playerMove.FinishedMovement && fadeImages[2] != 1 && canAttack)
            {
                fadeImages[2] = 1;
                animator.SetInteger("Atk", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (playerMove.FinishedMovement && fadeImages[3] != 1 && canAttack)
            {
                fadeImages[3] = 1;
                animator.SetInteger("Atk", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (playerMove.FinishedMovement && fadeImages[4] != 1 && canAttack)
            {
                fadeImages[4] = 1;
                animator.SetInteger("Atk", 5);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (playerMove.FinishedMovement && fadeImages[5] != 1 && canAttack)
            {
                fadeImages[5] = 1;
                animator.SetInteger("Atk", 6);
            }
        }
        else
        {
            animator.SetInteger("Atk",0);
        }


        if(Input.GetKey(KeyCode.Space))
        {
            Vector3 targetPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if(Physics.Raycast(ray,out rayHit))
            {
                targetPosition = new Vector3(rayHit.point.x,transform.position.y,rayHit.point.z);
            }


            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition - transform.position),15f * Time.deltaTime);

        }


    } //check input



    bool FadeAndWait(Image fadeImage,float fadeTime)
    {
        bool faded = false;


        if (fadeImage == null)
            return faded;

        if(!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;


        if(fadeImage.fillAmount <= 0.0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }

    private void CheckToFade()
    {
        if(fadeImages[0] == 1)
        {
            if(FadeAndWait(fillWaitImage_1,1.0f))
            {
                fadeImages[0] = 0;
            }
        }

        if (fadeImages[1] == 1)
        {
            if (FadeAndWait(fillWaitImage_2, 0.7f))
            {
                fadeImages[1] = 0;
            }
        }

        if (fadeImages[2] == 1)
        {
            if (FadeAndWait(fillWaitImage_3, 0.1f))
            {
                fadeImages[2] = 0;
            }
        }

        if (fadeImages[3] == 1)
        {
            if (FadeAndWait(fillWaitImage_4, 0.2f))
            {
                fadeImages[3] = 0;
            }
        }

        if (fadeImages[4] == 1)
        {
            if (FadeAndWait(fillWaitImage_5, 0.3f))
            {
                fadeImages[4] = 0;
            }
        }

        if (fadeImages[5] == 1)
        {
            if (FadeAndWait(fillWaitImage_6, 0.08f))
            {
                fadeImages[5] = 0;
            }
        }
    }
}
