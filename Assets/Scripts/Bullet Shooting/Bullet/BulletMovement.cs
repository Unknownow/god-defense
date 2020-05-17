using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private GameObject _explosionVFX;
    protected BulletProperties _bulletProperties; 

    private void Start() {
        _bulletProperties = gameObject.GetComponent<BulletProperties>();
    }

    private void Update() {
        
    }

    // Di chuyen

    public void ForwardShoot()
    {
        float speed = _bulletProperties.MovementSpeed;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10;
    }

    // Contact voi enemy
    private void OnTriggerEnter(Collider other) {
        if(other.transform.CompareTag("Enemy")){
            Debug.Log("Bullet contacts enemy");
            // _explosionVFX = gameObject.transform.GetChild(1).gameObject;
            // _explosionVFX.SetActive(true);
            BulletFactory.DestroyBullet(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
            if(other.transform.CompareTag("Enemy")){
            // _explosionVFX = gameObject.transform.GetChild(1).gameObject;
            // _explosionVFX.SetActive(true);
            BulletFactory.DestroyBullet(gameObject);
            Destroy(gameObject);
        }
    }
}
