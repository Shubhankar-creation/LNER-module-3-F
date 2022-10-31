using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectExtenguisher : MonoBehaviour
{
    public GameObject co2, water, wet, powder, foam;
    public GameObject ovrCamera;
    public GameObject goBackToModuleUI;
    [Header("Particles")]
    public GameObject co2Part;
    public GameObject waterPart;
    public GameObject wetPart;
    public GameObject powderPart;
    public GameObject foamPart;

    public GameObject selectExtUI;
    public GameObject handInteractions;

    public GameObject leftController, rigthController;

    public GameObject mask;


    private void Awake()
    {
        leftController.SetActive(false);
        rigthController.SetActive(false);
    }

    public void OnExtHover(GameObject go)
    {
        go.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
    }
    public void OnExtUnhover(GameObject go)
    {
        go.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void OnCO2Select()
    {
        co2.SetActive(true);
        co2Part.SetActive(true);
        GameManager.instance.extinguisherSize = 10f;
        EndHand(co2Part);
    }
    public void OnWaterSelect()
    {
        water.SetActive(true);
        waterPart.SetActive(true);
        GameManager.instance.extinguisherSize = 24f;

        EndHand(waterPart);
    }
    public void OnWetSelect()
    {
        wet.SetActive(true);
        wetPart.SetActive(true);
        GameManager.instance.extinguisherSize = 13f;
        EndHand(wetPart);
    }
    public void OnPowderSelect()
    {
        powder.SetActive(true);
        powderPart.SetActive(true);
        GameManager.instance.extinguisherSize = 20f;
        EndHand(powderPart);
    }
    public void OnFoamSelect()
    {
        foam.SetActive(true);
        foamPart.SetActive(true);
        GameManager.instance.extinguisherSize = 29f;
        EndHand(foamPart);
    }

    public void EndHand(GameObject particles)
    {
        GameManager.instance.rayVisual.SetActive(true);
        goBackToModuleUI.SetActive(true);

        GameManager.instance.particles = particles;
        handInteractions.SetActive(false);
        leftController.SetActive(true);
        rigthController.SetActive(true);
        selectExtUI.SetActive(false);
        mask.SetActive(false);
    }

}
