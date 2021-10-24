using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static event Action<GameObject> NewObject;
    public static event Action<GameObject> RemoveObject;

    private Touch touch;
    private Quaternion rotationY;
    public float rotateSpeedModifier = 0.1f;

    public bool isActive = false;
    public GameObject[] isActiveGameObjects;
    private void OnEnable()
    {
        NewObject?.Invoke(this.gameObject);
    }

    private void OnDestroy()
    {
        RemoveObject?.Invoke(this.gameObject);
    }

    private void Update()
    {
        foreach (var GO in isActiveGameObjects)
        {
            GO.SetActive(isActive);
        }
        if (!isActive) return;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }

    public void ToggleIsActive()
    {
        isActive = !isActive;
    }
}
