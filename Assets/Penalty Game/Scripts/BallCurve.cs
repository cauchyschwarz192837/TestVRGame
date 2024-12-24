using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCurve : MonoBehaviour
{
    public float speed = 10f;
    public float curveAmount = 0.5f;
    public float curveAngle = 30f;

    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = -transform.forward;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 curveVector = Quaternion.AngleAxis(curveAngle, Vector3.up) * direction;
            Vector3 forceVector = curveVector * curveAmount + direction * speed;
            rb.AddForce(forceVector, ForceMode.Impulse);
        }
    }
}
