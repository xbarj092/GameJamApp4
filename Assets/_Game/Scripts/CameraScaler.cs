using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Camera _camera;
    private float startScreenHight;

    private void Awake() {
        _camera = Camera.main;
        startScreenHight = _camera.orthographicSize;
    }

    void Update()
    {
        transform.localScale = Vector3.one * _camera.orthographicSize/startScreenHight;
    }
}
