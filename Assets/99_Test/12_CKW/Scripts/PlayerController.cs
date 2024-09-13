using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] [Range(0.5f, 1.0f)] private float lookUpValue = 0.5f;
    [SerializeField] [Range(0.0f, 0.5f)] private float lookDownValue = 0.5f;

    private bool _isLookingUp;
    private bool _isLookingDown;

    private CinemachineFramingTransposer _cft;

    private void Start()
    {
        _isLookingUp = false;
        _isLookingDown = false;
        _cft = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    void Update()
    {
        _isLookingUp = Input.GetKey(KeyCode.W);
        _isLookingDown = Input.GetKey(KeyCode.S);
        if (_isLookingUp)
            _cft.m_ScreenY = lookUpValue;
        else if (_isLookingDown)
            _cft.m_ScreenY = lookDownValue;
        else
            _cft.m_ScreenY = 0.5f;
    }
}
