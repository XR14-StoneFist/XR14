using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;
    
    private Vector3 _origin;
    private Vector3 _destination;
    private float _distance;
    private float _lerpPosition;
    private bool _isMovingForward;
    private float _stopTime;
    
    private void Start()
    {
        _origin = startPosition.transform.position;
        _destination = endPosition.transform.position;
        _lerpPosition = 0;
        _isMovingForward = true;
        _stopTime = 0;
    }
    
    private void Update()
    {
        UpdateDistance();
        MovePlatform();
    }
    
    private void UpdateDistance()
    {
        _distance = Vector3.Distance(_origin, _destination);
    }
    
    private void MovePlatform()
    {
        if (_lerpPosition >= 1)
        {
            if (_isMovingForward)
            {
                _origin = endPosition.transform.position;
                _destination = startPosition.transform.position;
            }
            else
            {
                _origin = startPosition.transform.position;
                _destination = endPosition.transform.position;
            }

            _isMovingForward = !_isMovingForward;
            _lerpPosition = 0;
            _stopTime = stopTime;
        }

        if (_stopTime > 0)
        {
            _stopTime -= Time.deltaTime;
        }
        else
        {
            _lerpPosition += speed * Time.deltaTime / _distance;
            platform.transform.position = Vector2.Lerp(_origin, _destination, _lerpPosition);
        }
        
    }
}
