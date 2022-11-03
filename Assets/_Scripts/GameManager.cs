using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject fireReportUIs, evacReportUIs;

    public Transform rightAnchor, leftAnchor;
    public Transform handRight, handLeft;
    public GameObject controllers;

    public GameObject leftRayVisual, rightRayVisual;
    public OVRManager ovrManager;
    public GameObject Rope;
    private bool ropeOn;
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
    private void Awake()
    {
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
        if(!ropeOn && controllers.active)
        {
            ropeOn = true;
            pickExtUI.SetActive(true);
            Rope.SetActive(true);
        }

        if (pinRemoved && OVRInput.Get(OVRInput.Button.One) && GameManager.instance.extinguisherSize >= 0f)
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RHand);
            GameManager.instance.extinguisherSize -= Time.deltaTime;

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
