using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private TrapProperties _trapProperties;

    private void OnEnable()
    {
        StartCoroutine(DestroyTrap());
    }

    private void Awake()
    {
        _trapProperties = gameObject.GetComponent<TrapProperties>();
        StartCoroutine(DestroyTrap());
    }

    IEnumerator DestroyTrap()
    {
        yield return new WaitForSeconds(_trapProperties.Duration);
        _trapProperties.Destroy();
    }
}
