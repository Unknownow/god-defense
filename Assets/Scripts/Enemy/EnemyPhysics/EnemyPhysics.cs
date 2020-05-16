using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    private Rigidbody _enemyBody;
    public void AddPushForce(Vector3 sourcePosition, float forceMagnitude)
    {
        gameObject.GetComponent<Rigidbody>().AddForce((transform.position - sourcePosition) * forceMagnitude, ForceMode.Impulse);
    }
}
