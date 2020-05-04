using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float follow_height = 15f;
    public float follow_distance = 13f;

    private Transform player;

    private float target_Height;
    private float current_Height;
    private float current_Rotation;


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target_Height = transform.position.y + follow_height;

        current_Rotation = transform.eulerAngles.y;

        current_Height = Mathf.Lerp(transform.position.y,target_Height,0.9f * Time.deltaTime);

        transform.LookAt(player);

    }
}
