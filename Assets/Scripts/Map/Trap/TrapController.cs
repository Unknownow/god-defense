using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    protected TrapProperties _trapProperties;
    protected Vector3 truePos;

    protected virtual void OnEnable()
    {
        Initialize();
    }

    protected virtual void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        Initialize();
    }

    public virtual void OnPlaced()
    {
        Initialize();
    }

    protected virtual IEnumerator DestroyTrap(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _trapProperties.Destroy();
    }

    // void LateUpdate() {
    //     float gridSize = GameManager.Instance().gridSize;
    //     truePos.x = Mathf.Floor(this.transform.position.x / gridSize) * gridSize;
    //     truePos.y = 0.1f;
    //     truePos.z = Mathf.Floor(this.transform.position.z / gridSize) * gridSize;

    //     this.transform.position = truePos;
    // }

    public TrapProperties GetProperties()
    {
        return this._trapProperties;
    }

    protected virtual void Initialize()
    {
        StartCoroutine(DestroyTrap(_trapProperties.Duration));
    }
}
