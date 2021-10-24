using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager aRRaycastManager;
    public ARPlaneManager aRPlaneManager;
    public Camera arCamera;
    
    private Pose _placementPose;
    [SerializeField] private bool usePlacementIndicator = false;
    private bool _placementPoseIsValid = false;

    [SerializeField] private GameObject tempGhostObject = null;
    [SerializeField] private GameObject tempObject = null;
    
    private GameObject _ghostObject; // holder for transparent game object for placement visualisation
    public GameObject[] placementConformationButtons;

    private void Start()
    {
        PanelController.ShopPanel += HandleShopPanel;
        UpdateConfirmButtons();
    }

    private void OnDestroy()
    {
        PanelController.ShopPanel -= HandleShopPanel;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (_ghostObject != null) UpdateGhostObject();

        foreach (var plane in aRPlaneManager.trackables)
            plane.gameObject.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseIsValid)
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        
        placementIndicator.SetActive(usePlacementIndicator && _placementPoseIsValid);
    }

    private void UpdatePlacementPose()
    {
        var screenPosition = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.6f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenPosition, hits, TrackableType.Planes); // only want to detect planes

        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid) _placementPose = hits[0].pose;
    }
    
    void UpdateGhostObject()
    {
        _ghostObject.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
    }

    void HandleShopPanel(bool panelStatus)
    {
        usePlacementIndicator = panelStatus;
    }

    public void SpawnGhostObject(string nameOfObject)
    {
        DestroyGhostObject();

        _ghostObject = (GameObject) Instantiate(tempGhostObject, _placementPose.position, _placementPose.rotation);
        UpdateConfirmButtons();
    }

    public void SpawnRealObject()
    {
        if (_ghostObject == null) return;
        Instantiate(tempObject, _placementPose.position, _placementPose.rotation);
        DestroyGhostObject();
    }

    void UpdateConfirmButtons()
    {
        foreach (var GO in placementConformationButtons)
        {
            GO.SetActive(_ghostObject != null);
        }
    }

    public void DestroyGhostObject()
    {
        if (_ghostObject == null) return;
        Destroy(_ghostObject);
        _ghostObject = null;
        
        UpdateConfirmButtons();
    }
}
