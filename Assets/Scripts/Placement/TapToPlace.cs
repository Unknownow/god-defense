using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

[RequireComponent(typeof(ARReferencePointManager))]
public class TapToPlace : MonoBehaviour
{

    [SerializeField]
    private GameObject objectToPlace;
    [SerializeField]
    private GameObject placementIndicator;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _crosshairCanvas;

    private ARSessionOrigin arOrigin;
    private ARPlaneManager arPlaneManager;
    private ARPointCloudManager arPointCloudManager;
    private ARReferencePointManager arReferencePointManager;

    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool isFinding = true;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arReferencePointManager = arOrigin.GetComponent<ARReferencePointManager>();
        arPointCloudManager = arOrigin.GetComponent<ARPointCloudManager>();
        arPlaneManager = arOrigin.GetComponent<ARPlaneManager>();

        isFinding = true;

        _player = GameObject.Find("Player");
        _crosshairCanvas = GameObject.Find("CrosshairCanvas");
        _player.SetActive(false);
        _crosshairCanvas.SetActive(false);
    }

    void Update()
    {
        if (isFinding)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }  
    }

    private void PlaceObject()
    {
        if (arReferencePointManager != null)
        {
            //arReferencePointManager.referencePointPrefab = objectToPlace;
            //arReferencePointManager.referencePointPrefab.transform.localScale = objectToPlace.transform.localScale / 25;
            ARReferencePoint referencePoint = arReferencePointManager.TryAddReferencePoint(placementPose);
            //referencePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // var goStage = Instantiate(objectToPlace,placementPose.position, placementPose.rotation);
        // goStage.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = false;
            List<ARPlane> planes = new List<ARPlane>();
            arPlaneManager.GetAllPlanes(planes);
            foreach (ARPlane plane in planes) {
                plane.gameObject.SetActive(arPlaneManager.enabled);
            }
        }

        if (arPointCloudManager != null)
        {
            arPointCloudManager.enabled = false;
            GameObject goPointCloud = arPointCloudManager.pointCloudPrefab;
            if (goPointCloud != null)
            {
                var point = GameObject.Find(goPointCloud.name + "(Clone)");
                if (point != null)
                {
                    point.SetActive(arPointCloudManager.enabled);
                }
            }
        }

        //Lay toa do dat stage
        GameStateManager gameStateManager = GameObject.FindObjectOfType<GameStateManager>();
        gameStateManager.SetStagePosition(GetPlacementPosition());

        //Bat nguoi choi
        _player.SetActive(true);
        _crosshairCanvas.SetActive(true);

        //Bat in-game canvas + bat dau countdown
        GameUIManager gameUIManager = GameObject.FindObjectOfType<GameUIManager>();
        gameUIManager.ingameCanvas.SetActive(true);
        gameUIManager.OnStageLoaded();

        placementIndicator.SetActive(false);
        isFinding = false;
    }

    public Vector3 GetPlacementPosition()
    {
        return placementPose.position;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}