using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandler : MonoBehaviour
{
    public Transform handleGrabber;
    public Transform handle;
    Rigidbody rb;

    GameObject door;
    float lerpTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        door = this.transform.parent.gameObject;
    }

    private void Update()
    {
        if(door.transform.eulerAngles.y <= 10f)
        {
            lerpTime += Time.deltaTime;
            Destroy(door.GetComponent<HingeJoint>());
            Destroy(door.GetComponent<Rigidbody>());
            Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), lerpTime * 0.5f);
        }
    }
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
