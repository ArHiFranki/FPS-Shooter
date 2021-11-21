using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    private void Update()
    {
        transform.Rotate(0, _speed, 0, Space.World);
    }
}
