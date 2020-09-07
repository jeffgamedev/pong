using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _width = 28f;

    private void LateUpdate()
    {
        _camera.orthographicSize = _width / _camera.aspect;
    }
}
