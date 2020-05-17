﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _playerParent;
    protected PlayerProperties _playerProperties;

    private void Start()
    {
        _playerParent = gameObject.transform;
        _playerProperties = gameObject.GetComponent<PlayerProperties>();
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
        bullet = BulletFactory.SpawnBullet(transform.position, _playerProperties.PlayerDirection, _playerParent);
        BulletMovement _bulletMovement = bullet.GetComponent<BulletMovement>();


        //_bulletMovement.ForwardShoot();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 10;
    }
}