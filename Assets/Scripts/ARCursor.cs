using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManagerAR;
    public Camera arCamera;

    private bool useCursor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if(useCursor) UpdateCursor();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = arCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManagerAR.Raycast(screenPosition, hits, TrackableType.Planes); // only want to detect planes

        if (hits.Count == 0) return;
        transform.position = hits[0].pose.position;
        transform.rotation = hits[0].pose.rotation;
    }
}
