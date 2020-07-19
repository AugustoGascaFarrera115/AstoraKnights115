using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS2 : MonoBehaviour
{

    PH2 playerHealth;


    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PH2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerHealth.Shielded = true;
        print("The player is shielded");
    }

    private void OnDisable()
    {
        playerHealth.Shielded = false;
        print("The player is not shielded");
    }





}
