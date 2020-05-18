using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private GameObject _explosionVFX;
    protected BulletProperties _bulletProperties; 

    private void Awake() {
        _bulletProperties = gameObject.GetComponent<BulletProperties>();
    }

    private void OnEnable() {
        StartCoroutine(DestroyBulletOutRange(_bulletProperties.LifeTime));
    }

    public void ForwardShoot()
    {
        float speed = _bulletProperties.MovementSpeed;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.CompareTag("Enemy")){
            _explosionVFX = gameObject.transform.GetChild(1).gameObject;
            _explosionVFX.SetActive(true);
            BulletFactory.DestroyBullet(gameObject);
            //Destroy(gameObject);
        }
    }

    protected virtual IEnumerator DestroyBulletOutRange(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BulletFactory.DestroyBullet(gameObject);
    }
}
