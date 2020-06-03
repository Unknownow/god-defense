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
        float speed = _bulletProperties.BulletSpeed;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.velocity = direction * speed;
    }
}
