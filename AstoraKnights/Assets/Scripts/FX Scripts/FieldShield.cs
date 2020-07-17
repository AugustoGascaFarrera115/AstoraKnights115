using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldShield : MonoBehaviour
{

    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        playerHealth.Shielded = true;
        print("The player is Shielded");
    }

    private void OnDisable()
    {
        playerHealth.Shielded = false;
        print("The player is not Shielded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
