using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager aRRaycastManager;
    public Camera arCamera;
    
    private Pose _placementPose;
    [SerializeField] private bool usePlacementIndicator = true;
    private bool _placementPoseIsValid = false;


    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        if(usePlacementIndicator) UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseIsValid)
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        
        placementIndicator.SetActive(usePlacementIndicator && _placementPoseIsValid);
    }

    private void UpdatePlacementPose()
    {
        var screenPosition = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.75f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenPosition, hits, TrackableType.Planes); // only want to detect planes

        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid) _placementPose = hits[0].pose;
    }
}
