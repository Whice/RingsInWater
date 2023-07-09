using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void AddForce1()
    {
        rb.AddForce(Vector3.up * 500f + Vector3.left * 10, ForceMode.Force);
    }
}
