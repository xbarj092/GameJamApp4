using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Camera _camera;
    private static float startScreenHight = -1;

    private void Awake() {
        _camera = Camera.main;
        if(startScreenHight == -1) startScreenHight = _camera.orthographicSize;
    }

    void Update()
    {
        transform.localScale = Vector3.one * _camera.orthographicSize/startScreenHight;
    }
}
