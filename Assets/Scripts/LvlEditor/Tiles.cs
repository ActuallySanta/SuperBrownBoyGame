using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(bool _isOffset)
    {
        spriteRenderer.color = _isOffset ? offsetColor : baseColor;
    }
}
