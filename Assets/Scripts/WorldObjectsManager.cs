using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectsManager : MonoBehaviour
{
    public GameObject clearButton = null;
    private List<GameObject> worldObjects = new List<GameObject>();
    private void Start()
    {
        ObjectController.NewObject += ProcessNewObject;
        ObjectController.RemoveObject += ProcessRemoveObject;
        UpdateClearButton();
    }

    private void OnDestroy()
    {
        ObjectController.NewObject -= ProcessNewObject;
        ObjectController.RemoveObject -= ProcessRemoveObject;
    }

    void UpdateClearButton()
    {
        clearButton.SetActive(worldObjects.Count>0);
    }

    void ProcessNewObject(GameObject newObject)
    {
        worldObjects.Add(newObject);
        UpdateClearButton();
    }

    void ProcessRemoveObject(GameObject objectToRemove)
    {
        worldObjects.Remove(objectToRemove);
        UpdateClearButton();
    }

    public void ClearAllObjects()
    {
        foreach (var worldObject in worldObjects)
        {
            Destroy(worldObject);
        }
        
        worldObjects.Clear();
        UpdateClearButton();
    }
}
