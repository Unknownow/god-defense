using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    private Rigidbody _enemyBody;
    public void AddPushForce(Vector3 forceDirection, float forceMagnitude)
    {
        forceDirection = forceDirection.normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
    }

    public void AddExplosionForce(float forceMagnitude, Vector3 explosionCenter, float explosionRadius)
    {
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, explosionCenter, explosionRadius, 1.0f, ForceMode.Impulse);
    }
}
