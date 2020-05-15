using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private TrapProperties _trapProperties;
    Vector3 truePos;

    private void OnEnable()
    {
        // StartCoroutine(DestroyTrap());
    }

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
    }

    public void onPlaced() {
        StartCoroutine(DestroyTrap());
    }

    IEnumerator DestroyTrap()
    {
        yield return new WaitForSeconds(_trapProperties.Duration);
        _trapProperties.Destroy();
    }

    // void LateUpdate() {
    //     float gridSize = GameManager.Instance().gridSize;
    //     truePos.x = Mathf.Floor(this.transform.position.x / gridSize) * gridSize;
    //     truePos.y = 0.1f;
    //     truePos.z = Mathf.Floor(this.transform.position.z / gridSize) * gridSize;

    //     this.transform.position = truePos;
    // }

    public TrapProperties GetProperties() {
        return this._trapProperties;
    }
}
