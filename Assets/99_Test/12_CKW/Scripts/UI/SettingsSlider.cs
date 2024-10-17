using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectMask2D mask;

    private Slider _slider;
    private float _maxRightMask;
    private float _initialRightMask;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _maxRightMask = barRect.rect.width - mask.padding.x - mask.padding.z;
        _initialRightMask = mask.padding.z;
    }

    private void Update()
    {
        SetValue(_slider.value);
    }

    private void SetValue(float newValue)
    {
        float targetWidth = newValue * _maxRightMask;
        float newRightMask = _maxRightMask + _initialRightMask - targetWidth;
        Vector4 padding = mask.padding;
        padding.z = newRightMask;
        mask.padding = padding;
    }
}
