using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject fireReportUIs, evacReportUIs;

    public Transform rightAnchor, leftAnchor;
    public Transform handRight, handLeft;

    public GameObject leftRayVisual, rightRayVisual;
    public OVRManager ovrManager;
    public GameObject Rope;
    public GameObject pickExtUI;

    public AudioClip callRing;
    public AudioClip popUI;

    public bool canAlert;
    public bool alertDone;
    public bool phoneDone;
    public bool maskDone;
    public bool fireDone;
    public bool windowsDone;
    public bool doorDone;
    public bool shoutDone;

    public string dialedNumber;

    public bool canRemovePin;
    public bool pinRemoved;
    public bool canExt;

    public GameObject particles;

    public int fireSize = 100;
    public float extinguisherSize;

    public float wrongExtTime = 2f;

    public bool keyGrabbed;

    public float totalTime = 0;

    [HideInInspector]
    public float alarmTime = 0, phoneTime = 0, maskTime = 0, windowTime = 0, doorTime = 0, shoutTime = 0, fireTime = 0, evacTime = 0;

    private bool doNothing;

    
    public int averageAlert = 10;
    public int averageCall = 25;
    public int averageMask = 30;

    public int averageFire = 30;
    public int averageShout = 80;

    public int averageWindow = 6;
    public int averageDoor = 10;

    public int averageFight = 115;
    public int averageEvac = 40;

    public GameObject key;
    [HideInInspector]
    public bool onAlarmTP, onPhoneTP, onClothTP;
    public GameObject ropeMesh;
    private void Awake()
    {
        ropeMesh = Rope.transform.GetChild(0).gameObject;
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        
        if(!canRemovePin && rightAnchor.gameObject.active && leftAnchor.gameObject.active)
        {
            pickExtUI.SetActive(true);
            Rope.SetActive(true);
            for(int i = 0; i<ropeMesh.transform.childCount; i++)
            {
                ropeMesh.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }

        }

        if (pinRemoved && OVRInput.Get(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RHand);

            if (!doNothing)
            {
                particles.GetComponent<AudioSource>().Play();
                particles.GetComponent<ParticleSystem>().Play();
                doNothing = true;
            }

        }
        else if(doNothing)
        {
            doNothing = false;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RHand);
            particles.GetComponent<AudioSource>().Stop();
            particles.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void OnKeyGrabbed()
    {
        keyGrabbed = true;
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.4f)
        {
            key.transform.parent = handRight.transform;
        }
        else
        {
            key.transform.parent = handLeft.transform;
        }
    }
}
