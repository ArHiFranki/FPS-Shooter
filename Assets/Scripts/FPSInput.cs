using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6.0f;

    private CharacterController _characterController;
    private float _deltaX;
    private float _deltaZ;
    private Vector3 _movement;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _deltaX = Input.GetAxis("Horizontal") * _moveSpeed;
        _deltaZ = Input.GetAxis("Vertical") * _moveSpeed;

        _movement = new Vector3(_deltaX, 0, _deltaZ);
        _movement = Vector3.ClampMagnitude(_movement, _moveSpeed);
        _movement *= Time.deltaTime;
        _movement = transform.TransformDirection(_movement);
        _characterController.Move(_movement);
    }
}
