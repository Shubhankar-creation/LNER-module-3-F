using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthRug : MonoBehaviour
{
    public Transform mainCamera;

    private bool canLerp;
    private Vector3 velocity = Vector3.zero;
    public float lerpTime;

    void FixedUpdate()
    {
        if(canLerp)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(mainCamera.position.x, mainCamera.position.y - 0.13f, mainCamera.position.z + 0.06f), ref velocity, lerpTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(new Vector3(180f, 0f, 180f)), lerpTime);
        }
    }

    public void OnClothSelect()
    {
        if(GameManager.instance.inClothPlace)
        {
            canLerp = true;
            StartCoroutine(disableCloth());
        }
    }

    IEnumerator disableCloth()
    {
        yield return new WaitForSeconds(1f);
        canLerp = false;
        this.gameObject.transform.parent = mainCamera;
    }
}
