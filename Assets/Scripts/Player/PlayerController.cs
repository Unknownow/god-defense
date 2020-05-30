using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _playerParent;
    protected PlayerProperties _playerProperties;
    private AudioSource _staffAudioSource;
    private GameObject _staffVFX;
    private Transform _bulletSpawnPoint;

    private void Start()
    {
        _playerParent = gameObject.transform;
        _playerProperties = gameObject.GetComponent<PlayerProperties>();
        _bulletSpawnPoint = gameObject.transform.GetChild(1);
        _staffAudioSource = gameObject.GetComponent<AudioSource>();
        _staffVFX = gameObject.transform.GetChild(2).transform.GetChild(1).gameObject;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        GameObject bullet;
        bullet = BulletFactory.SpawnBullet(_bulletSpawnPoint.position, _bulletSpawnPoint.forward, _playerParent);
        BulletMovement _bulletMovement = bullet.GetComponent<BulletMovement>();
        _bulletMovement.ForwardShoot(_bulletSpawnPoint.position, _bulletSpawnPoint.forward, _playerParent);

        _staffAudioSource.Play();

        _staffVFX.SetActive(true);
        _staffVFX.GetComponent<ParticleSystem>().Play();
        Invoke("StaffEffect", _staffVFX.GetComponent<ParticleSystem>().main.duration);
    }

    private void StaffEffect() 
    {
        _staffVFX.GetComponent<ParticleSystem>().Stop();
        _staffVFX.SetActive(false);
    }
}
