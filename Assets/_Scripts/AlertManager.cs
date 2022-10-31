using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertManager : MonoBehaviour
{

    public GameObject breakGlass;
    public GameObject pressAlert;
    public GameObject phone;
    public GameObject Keypad;
    public GameObject keypadScreen;
    //public GameObject dialCorrect;
    public GameObject dialError;
    public GameObject fireLocation;
    public GameObject fireType;

    public GameObject selectExtUI;

    private Material alarmMat;

    private int len = 0;
    public void OnAlarmHover(GameObject alarm)
    {
        alarmMat = alarm.GetComponent<MeshRenderer>().material;
        alarm.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void OnAlarmUnhover(GameObject alarm)
    {
        alarm.GetComponent<MeshRenderer>().material.color = alarmMat.color;
    }
    public void OnGlassSelect()
    {
        GameManager.instance.canAlert = true;
        this.GetComponent<AudioSource>().Play();
        breakGlass.SetActive(false);
        pressAlert.SetActive(true);

        Destroy(this.gameObject.transform.GetChild(0).gameObject);

        //to-do Add Glass break animation
    }

    public void OnAlertSelect()
    {
        if (GameManager.instance.canAlert == true)
        {
            this.GetComponent<AudioSource>().Play();

            GameManager.instance.alertPressed = true;
            pressAlert.SetActive(false);

            GameManager.instance.alarmTime = GameManager.instance.totalTime;
        }
    }

    public void OnPhoneHover()
    {
        dialError.SetActive(false);
        phone.SetActive(true);
    }

    public void OnPhoneSelect()
    {
        phone.SetActive(false);
    }

    public void OnNumberSelect(string textGO)
    {
        len++;
        GameManager.instance.dialedNumber += textGO;
        keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber;
    }

    public void OnCallDialed()
    {
        if(GameManager.instance.dialedNumber == "999")
        {
            //dialCorrect.SetActive(true);
            StartCoroutine(emergencyCalled());
        }
        else
        {
            dialError.SetActive(true);
            GameManager.instance.dialedNumber = "";
        }
    }

    public void OnClearSelect()
    {
        GameManager.instance.dialedNumber = "";
        keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber;
    }

    public void OnPrevSelect()
    {
        if(len !=0 )
            --len;
        keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber.Substring(0, len);
    }
    IEnumerator emergencyCalled()
    {
        this.GetComponent<AudioSource>().clip = GameManager.instance.callRing;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(4);
        //dialCorrect.SetActive(false);
        Keypad.SetActive(false);
        dialError.SetActive(false);
        fireLocation.SetActive(true);
        this.GetComponent<AudioSource>().Stop();
    }

    public void OnLnerAcademySelect()
    {
        Debug.Log("presses");
        fireLocation.SetActive(false);
        fireType.SetActive(true);
    }

    public void PlaceSelectionError()
    {
        fireLocation.SetActive(false);
        fireType.SetActive(true);
    }
    public void OnElectricTypeSelect()
    {
        fireType.SetActive(false);
    }

    public void OnClothTpSelect()
    {
        GameManager.instance.inClothPlace = true;
    }
    public void OnFightSelect()
    {
        selectExtUI.SetActive(true);
        GameManager.instance.totalTime = 0;
    }

    public void OnEvacSelect()
    {
        GameManager.instance.totalTime = 0;
    }

}
