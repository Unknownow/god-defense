using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory instance;
    
    [SerializeField]
    private GameObject _bulletPrefab;

    private Transform _factoryParent;
    private Queue<GameObject> _bulletPool;

    private void Awake() 
    {
        instance = this;
        instance._bulletPool = new Queue<GameObject>();

        _factoryParent = gameObject.transform;
    }

    public static GameObject SpawnBullet(Vector3 position, Vector3 direction, Transform parent)
    {
        GameObject bullet;
        if (instance._bulletPool.Count <= 0)
        {
            bullet = Instantiate(instance._bulletPrefab, position, Quaternion.identity, instance._factoryParent);
        }
        else 
        {
            bullet = instance._bulletPool.Dequeue();
            bullet.transform.position = position;
            bullet.SetActive(true);
        }
        bullet.transform.forward = direction;
        return bullet;
    }

    public static void DestroyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        instance._bulletPool.Enqueue(bullet);
    }
}
