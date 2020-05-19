using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    protected BulletProperties _bulletProperties; 

    private void Awake() {
        _bulletProperties = gameObject.GetComponent<BulletProperties>();
    }

    public void ForwardShoot()
    {
        float speed = _bulletProperties.MovementSpeed;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
}
