using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandler : MonoBehaviour
{
    public Transform handleGrabber;
    public Transform handle;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(handleGrabber.transform.position);
    }

    public void OnGrabEnd()
    {
        handleGrabber.position = handle.position;
        handleGrabber.rotation = handle.rotation;
        handleGrabber.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        handleGrabber.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
