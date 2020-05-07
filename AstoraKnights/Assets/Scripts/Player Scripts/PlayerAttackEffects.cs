using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{

    public GameObject groundImpact_Spawn, kickFX_Spawn, fireTornado_Spawn, fireShield_Spawn;
    public GameObject groundImpact_Prefab, kickFX_Prefab, fireTornadoFX_Prefab, fireShieldFX_Prefab, healFX_Prefab, thunderFX_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GroundImpact()
    {
        Instantiate(groundImpact_Prefab,groundImpact_Spawn.transform.position, UnityEngine.Quaternion.identity);
    }

    void Kick()
    {
        Instantiate(kickFX_Prefab, kickFX_Spawn.transform.position, UnityEngine.Quaternion.identity);
    }


    void FireTornado()
    {
        Instantiate(fireTornadoFX_Prefab, fireTornado_Spawn.transform.position, UnityEngine.Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireShieldObject = Instantiate(fireShieldFX_Prefab,fireShield_Spawn.transform.position, UnityEngine.Quaternion.identity) as GameObject;

        fireShieldObject.transform.SetParent(transform);
    }


    void Heal()
    {
        UnityEngine.Vector3 temp_position = transform.position;
        temp_position.y += 2f;
        GameObject healObject = Instantiate(healFX_Prefab, temp_position, UnityEngine.Quaternion.identity) as GameObject;
        healObject.transform.SetParent(transform);


    }

    void ThunderAttack()
    {
        for(int i = 0;i < 8;i++)
        {
            UnityEngine.Vector3 position = UnityEngine.Vector3.zero;

            if(i == 0)
            {
                position = new UnityEngine.Vector3(transform.position.x - 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 1)
            {
                position = new UnityEngine.Vector3(transform.position.x + 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 2)
            {
                position = new UnityEngine.Vector3(transform.position.x, transform.position.y + 2f, transform.position.z -4f);
            }
            else if (i == 3)
            {
                position = new UnityEngine.Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
            }
            else if (i == 4)
            {
                position = new UnityEngine.Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                position = new UnityEngine.Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                position = new UnityEngine.Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                position = new UnityEngine.Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }



            Instantiate(thunderFX_Prefab,position, UnityEngine.Quaternion.identity);
        }
    }


}// main class
