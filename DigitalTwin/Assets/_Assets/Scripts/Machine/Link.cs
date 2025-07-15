using System;
using UnityEngine;

enum Vector3Direction
{
    Up,
    Right,
    Forward
}

public class Link : MonoBehaviour
{
    [SerializeField] private Vector3 rototationDirection;
    [SerializeField] private float rotateAngle = 0;
    public Action<float> OnChangeRotateAngle;
    
    public const int MinValue = -180;
    public const int MaxValue = 180;
    
    public float RotateAngle
    {
        get
        {
            return rotateAngle;
        }
        set
        {
            var newValue = Mathf.Clamp(value, MinValue, MaxValue);
            rotateAngle = newValue;
            OnChangeRotateAngle?.Invoke(newValue);
        }
    }
    
    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(RotateAngle, rototationDirection.normalized);
        transform.localRotation = rotation;
    }
}
