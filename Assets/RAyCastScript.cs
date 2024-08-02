using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RAyCastScript : MonoBehaviour
{

    public GameObject spawned_prefab;
    GameObject spawned_object;

    bool object_spawned;
    ARRaycastManager arrayman;
    List <ARRaycastHit> hits = new List<ARRaycastHit>();


    // Start is called before the first frame update
    void Start()
    {
        object_spawned = false;
        arrayman = GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            var hitpose = hits[0].pose;
            if(arrayman.Raycast(Input.GetTouch(0).position,hits,TrackableType.PlaneWithinPolygon))
            {
                if(!object_spawned)
                {
                    spawned_object=Instantiate(spawned_prefab,hitpose.position,hitpose.rotation);
                    object_spawned = true;
                }
                else
                {
                    spawned_object.transform.position = hitpose.position;
                }
            }
        }
        
    }
}
