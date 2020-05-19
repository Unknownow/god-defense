using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject _bulletEffect;
    private GameObject _explosionVFX;
    protected BulletProperties _bulletProperties; 
    private bool explosionPlayed;

    private void Awake() {
        _bulletProperties = gameObject.GetComponent<BulletProperties>();

        _bulletEffect = gameObject.transform.GetChild(0).gameObject;
        _explosionVFX = gameObject.transform.GetChild(1).gameObject;
    }

    private void OnEnable() 
    {
        StartCoroutine(DestroyBulletOutRange(_bulletProperties.LifeTime));
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.CompareTag("Enemy")){

            _bulletEffect.SetActive(false);
            _explosionVFX.SetActive(true);
            _explosionVFX.GetComponent<ParticleSystem>().Play();

            Invoke("RecycleBullet", _explosionVFX.GetComponent<ParticleSystem>().main.duration);
        }
    }

    private void RecycleBullet() 
    {
        BulletFactory.DestroyBullet(gameObject);
        _explosionVFX.GetComponent<ParticleSystem>().Stop();
        _explosionVFX.SetActive(false);
        _bulletEffect.SetActive(true);
    }

    protected virtual IEnumerator DestroyBulletOutRange(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BulletFactory.DestroyBullet(gameObject);
    }
}
