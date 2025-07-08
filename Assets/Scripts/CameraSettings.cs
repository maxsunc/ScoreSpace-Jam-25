using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    // for now it just follows the player we will do more later
    [SerializeField]
    private Transform follow;
    [SerializeField]
    private Vector3 offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + offset;
    }
}
