using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3.0f;
    public Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);
    void Start()
    {
        transform.localPosition += offset;
        Destroy(gameObject, destroyTime);
    }

}
