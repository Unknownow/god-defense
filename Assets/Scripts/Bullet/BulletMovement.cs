using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    protected BulletProperties _bulletProperties;

    public GameObject _bulletSpawnPoint;

    private void Awake()
    {
        _bulletProperties = gameObject.GetComponent<BulletProperties>();
    }

    public void ForwardShoot(Vector3 position, Vector3 direction, Transform parent)
    {

        // Vector3 selfX = transform.right;
        // Vector3 selfY = transform.up;
        // Vector3 selfZ = transform.forward;
        
        // Vector3 shootingAngle = selfZ;

        // RaycastHit hit;
        // LayerMask mask = new LayerMask();
        // if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
        // {
        //     Debug.Log("aimed at something");
        //     HandleContactPosition(hit);
        // } else {
        //     float speed = _bulletProperties.BulletSpeed;
        //     Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        //     Debug.Log("NOT AIMED at something");
        //     rb.velocity = transform.forward * speed;
        // }

        float speed = _bulletProperties.BulletSpeed;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.velocity = direction * speed;
    }

    void HandleContactPosition(RaycastHit hit)
    {
        Vector3 startPosition = _bulletSpawnPoint.transform.position;
        Vector3 endPosition = hit.point;

        float shootingDistance = Vector3.Distance(startPosition, endPosition);
        float distanceRatio = shootingDistance / 1.0f;

        Vector3 shootingVector = Vector3.Lerp(startPosition, endPosition, distanceRatio);

        float speed = _bulletProperties.BulletSpeed;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        Debug.Log(shootingVector);

        rb.velocity = shootingVector * speed;
    }
}
