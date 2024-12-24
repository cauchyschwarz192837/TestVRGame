using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform mainCamera;
    private Vector3 currentPos;
    private Vector3 displacement;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = gameObject.transform.position;
        displacement = currentPos - mainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = mainCamera.position + displacement;
    }
}
