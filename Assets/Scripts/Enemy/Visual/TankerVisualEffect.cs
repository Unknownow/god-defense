using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerVisualEffect : MonoBehaviour, IEnemyVisualEffect
{
    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private float _percentageBuffedCap;
    private Color _freezeColor;
    private Color32 _buffedFreezeColor;
    private Color32 _defaultColor;

    private void Awake()
    {
        _defaultColor = new Color(255, 255, 255, 0);
        _freezeColor = new Color32(9, 132, 227, 100);
        _buffedFreezeColor = new Color32(55, 66, 250, 200);
    }

    public void Init()
    {
        _renderer.materials[1].color = _defaultColor;
    }

    public void Freeze(float percentage)
    {
        if (percentage <= _percentageBuffedCap)
        {
            _renderer.materials[1].color = _freezeColor;
            return;
        }
        _renderer.materials[1].color = _buffedFreezeColor;
    }

    public void Unfreeze()
    {
        _renderer.materials[1].color = _defaultColor;
    }
}
