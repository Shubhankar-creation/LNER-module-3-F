using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
   public OVRCameraRig cameraRig;
    

    private Vector3 teleportPos;

    private GameObject tpColor;
    private Color initialColor;

    private void Start()
    {
        tpColor = transform.GetChild(0).gameObject;
        initialColor = tpColor.GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }
    public void OnTeleportHover()
    {
        tpColor.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
    }
    public void OnTeleportUnhover()
    {
        tpColor.GetComponent<Renderer>().material.SetColor("_EmissionColor", initialColor);
    }
    public void OnTeleportSelect()
    {
        teleportPos = gameObject.transform.position;

        cameraRig.transform.position = new Vector3(teleportPos.x, cameraRig.transform.position.y, teleportPos.z);
    }
}
