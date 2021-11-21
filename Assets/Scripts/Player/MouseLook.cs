using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MouseLook : MonoBehaviour
{
    [SerializeField] private enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXAndY;
    [SerializeField] private float _sensitivityHorizontal = 9.0f;
    [SerializeField] private float _sensitivityVertical = 9.0f;

    [SerializeField] private float _verticalMin = -45.0f;
    [SerializeField] private float _verticalMax = 45f;

    private float _rotationX = 0;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityHorizontal, 0);
        }
        else if (_axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, _verticalMin, _verticalMax);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, _verticalMin, _verticalMax);

            float delta = Input.GetAxis("Mouse X") * _sensitivityHorizontal;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
