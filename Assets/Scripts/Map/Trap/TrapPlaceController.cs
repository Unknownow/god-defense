﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrapPlaceController : MonoBehaviour
{

    private GameObject currentPlaceableObject;

    [SerializeField]
    private LayerMask _attackableLayer;

    // [SerializeField]
    private float hitRadius = 0.1f;

    [SerializeField]
    private TrapType _trapType;
    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private bool isPlaceable = false;

    [SerializeField]
    private Image cooldownImage;

    private float coolDownTime;

    private bool isCoolingDown = false;
    Vector3 truePos;

    void Start() {
        cooldownImage.gameObject.SetActive(false);
    }

    private void Update() {
        if (cooldownImage.fillAmount > 0) {
            isCoolingDown = true;
            cooldownImage.fillAmount -= 1 / coolDownTime * Time.deltaTime;
        } else {
            isCoolingDown = false;
        }
    }

    private void LateUpdate()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {

        if (Input.GetKeyDown(KeyCode.B)) {
            if (currentPlaceableObject != null) {
                Destroy(currentPlaceableObject);
            }

            currentPlaceableObject = TrapFactory.SpawnTrap(TrapType.Booby, new Vector3(0,0,0));
        }
    }

    public void onButtonClick() {
        if (isCoolingDown) {
            return;
        }
        if (currentPlaceableObject != null) {
                Destroy(currentPlaceableObject);
            }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        Vector3 position = Vector3.zero;

        if (Physics.Raycast(ray, out hitInfo))
        {
            position = hitInfo.point;
        }

        currentPlaceableObject = TrapFactory.SpawnTrap(_trapType, position);

    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            Collider[] _colliders = new Collider[1];
            
            float gridSize = GameManager.Instance().gridSize;
            truePos.x = Mathf.Floor(hitInfo.point.x / gridSize) * gridSize;
            truePos.y = 0.1f;
            truePos.z = Mathf.Floor(hitInfo.point.z / gridSize) * gridSize;

            Vector3 centerPoint = truePos;
            centerPoint.x = truePos.x + 0.5f;
            centerPoint.z = truePos.z + 0.5f;

            Debug.DrawLine (Camera.main.transform.position, centerPoint, Color.red);

            currentPlaceableObject.transform.position = truePos;

            int countTarget = Physics.OverlapSphereNonAlloc(centerPoint, hitRadius, _colliders, _attackableLayer);
            if (countTarget > 0)
            {
                isPlaceable = true;
                Renderer renderer = currentPlaceableObject.GetComponent<Renderer>();
                
                if (renderer != null) {
                    Material mat = renderer.material;
                    mat.color = Color.green;
                    renderer.material = mat;
                }
            } 
            else
            {
                isPlaceable = false;
                Renderer renderer = currentPlaceableObject.GetComponent<Renderer>();
                
                if (renderer != null) {
                    Material mat = renderer.material;
                    mat.color = Color.red;
                    renderer.material = mat;
                }
            }
            

        } else {
            Debug.Log("Failed");
        }
    }

    void StartCoolDown() {
        isCoolingDown = true;
        cooldownImage.gameObject.SetActive(true);
        cooldownImage.fillAmount = 1;
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isPlaceable) {
                Renderer renderer = currentPlaceableObject.GetComponent<Renderer>();
                    
                if (renderer != null) {
                    Material mat = renderer.material;
                    mat.color = Color.white;
                    renderer.material = mat;
                }

                TrapController controller = currentPlaceableObject.GetComponent<TrapController>();
                if (controller != null) {
                    Debug.Log("Placed!!!");
                    coolDownTime = controller.GetProperties().Cooldown;
                    controller.OnPlaced();
                    StartCoolDown();
                }

            } else {
                DestroyImmediate(currentPlaceableObject);
            }

            isPlaceable = false;
            currentPlaceableObject = null;
        }
    }
}