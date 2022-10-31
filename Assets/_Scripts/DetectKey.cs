using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKey : MonoBehaviour
{
    public GameObject doorHandler;
    public GameObject useKeyUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Key") && !GameManager.instance.keyGrabbed)
        {
            other.gameObject.transform.position = this.transform.position;
            other.gameObject.transform.rotation = this.transform.rotation;
            doorHandler.SetActive(true);
            other.transform.parent = this.transform.parent;
            useKeyUI.SetActive(false);
        }
    }

}
