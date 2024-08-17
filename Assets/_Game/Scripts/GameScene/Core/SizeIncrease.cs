using UnityEngine;

public class SizeIncrease : MonoBehaviour
{
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float scaleCap;
    [SerializeField] private float SegmentMultiplier;
    [SerializeField] private float CameraSizeMultiplier;
    [SerializeField] private float CameraSizeCap;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public float GetRadius()
    {
        return transform.localScale.x / 2;
    }

    public float GetSegmentSize()
    {
        return GetRadius() * SegmentMultiplier;
    }

    public void UpdateCoreSize()
    {
        if (transform.localScale.x > scaleCap) return;
        transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
    }

    public void UpdateCameraSize()
    {
        if (cam.orthographicSize > CameraSizeCap) return;

        cam.orthographicSize += CameraSizeMultiplier;
    }
}
