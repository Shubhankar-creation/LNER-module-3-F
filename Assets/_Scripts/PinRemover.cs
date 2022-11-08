using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinRemover : MonoBehaviour
{

    public GameObject grabPin;
    public GameObject sprayExt;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Pin") && GameManager.instance.canRemovePin)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2f)
            {
                other.gameObject.transform.parent = null;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                GameManager.instance.pinRemoved = true;

                grabPin.SetActive(false);
                sprayExt.SetActive(true);
                gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(setSparyUnactive());
            }
        }
    }

    IEnumerator setSparyUnactive()
    {
        yield return new WaitForSeconds(2.5f);
        sprayExt.SetActive(false);
    }
}
