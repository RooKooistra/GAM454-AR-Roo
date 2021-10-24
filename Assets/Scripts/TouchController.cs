using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public LayerMask layerMask;
    public Camera camera;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<ObjectController>())
                    hit.collider.gameObject.GetComponent<ObjectController>().ToggleIsActive();
            }
        }
        
    }
}
