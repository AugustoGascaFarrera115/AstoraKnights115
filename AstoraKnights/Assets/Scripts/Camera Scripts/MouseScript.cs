using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTexture;

    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotspot = Vector2.zero;

    public GameObject mousePoint;
    private GameObject mouseInstantiated;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTexture,hotspot,cursorMode);

        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if(Physics.Raycast(ray,out rayHit))
            {
                if(rayHit.collider is TerrainCollider)
                {
                    Vector3 temporal_position = rayHit.point;
                    temporal_position.y = 0.20f;

                    //Instantiate(mousePoint, temporal_position, Quaternion.identity);

                if(mouseInstantiated == null)
                {
                        mouseInstantiated = Instantiate(mousePoint) as GameObject;

                        mouseInstantiated.transform.position = temporal_position;
                    }
                    else
                    {
                        Destroy(mouseInstantiated);
                        mouseInstantiated = Instantiate(mousePoint) as GameObject;
                        mouseInstantiated.transform.position = temporal_position;
                    }

                }
            }

        }

    }
}
