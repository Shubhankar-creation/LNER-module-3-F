using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform rightAnchor, leftAnchor;
    public GameObject controllers;

    public GameObject rayVisual;

    public GameObject Rope;
    private bool ropeOn;
    public GameObject pickExtUI;

    public AudioClip callRing;
    public AudioClip popUI;

    public bool canAlert;
    public bool alertPressed;

    [HideInInspector]
    public bool inClothPlace;

    public string dialedNumber;

    public bool pinRemoved;

    public GameObject particles;

    public int fireSize = 100;
    public float extinguisherSize;

    public float wrongExtTime = 2f;

    public bool keyGrabbed;


    public float totalTime;
    public float alarmTime;
    public float phoneTime;
    public float maskTime;
    public float fireTime;
    public float evacTime;

    private bool doNothing;

    public int averageFight = 25;
    public int averageEvac = 40;
    public int averageAlert = 10;
    public int averageCall = 25;
    public int averageMask = 30;
    public int averageShout = 80;
    public int averageFire = 30;

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

        if (pinRemoved && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.8f)
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RHand);
            doNothing = true;

            if (!particles.GetComponent<AudioSource>().isPlaying)
                particles.GetComponent<AudioSource>().Play();
            else if(!particles.GetComponent<ParticleSystem>().isPlaying)
                particles.GetComponent<ParticleSystem>().Play();
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
        if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.4f)
        {
            this.transform.parent = rightAnchor.transform;
        }
        else
        {
            this.transform.parent = leftAnchor.transform;
        }
    }
}
