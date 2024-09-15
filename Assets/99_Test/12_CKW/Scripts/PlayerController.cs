using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] [Range(0.5f, 1.0f)] private float lookUpValue = 0.5f;
    [SerializeField] [Range(0.0f, 0.5f)] private float lookDownValue = 0.5f;

    public bool IsLookingUp;
    public bool IsLookingDown;

    private CinemachineFramingTransposer _cft;

    private void Start()
    {
        IsLookingUp = false;
        IsLookingDown = false;
        _cft = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    void Update()
    {
        IsLookingUp = Input.GetKey(KeyCode.W);
        IsLookingDown = Input.GetKey(KeyCode.S);
        if (IsLookingUp)
            _cft.m_ScreenY = lookUpValue;
        else if (IsLookingDown)
            _cft.m_ScreenY = lookDownValue;
        else
            _cft.m_ScreenY = 0.5f;
    }
}
