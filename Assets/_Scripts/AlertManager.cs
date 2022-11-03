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
    public GameObject useCloth;

    public GameObject selectExtUI;

    private Material alarmMat;

    private int len = 0;
    public void OnAlarmHover(bool val)
    {
        if (GameManager.instance.onAlarmTP)
            breakGlass.SetActive(val);
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

            GameManager.instance.alertDone = true;

            pressAlert.SetActive(false);

            GameManager.instance.alarmTime = GameManager.instance.totalTime;
        }
    }

    public void OnPhoneHover( bool val)
    {
        if (GameManager.instance.onPhoneTP)
            phone.SetActive(val);
    }
    public void OnPhoneSelect()
    {
        phone.SetActive(false);
    }
    
    public void OnClothHoverUnhover(bool val)
    {
        if (GameManager.instance.onClothTP)
            useCloth.SetActive(val);
    }
    public void OnNumberSelect(string textGO)
    {
        len++;
        if(len<=3)
        {
            GameManager.instance.dialedNumber += textGO;
            keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber;
        }
        
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
        }
    }

    public void OnClearSelect()
    {
        GameManager.instance.dialedNumber = "";
        keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber;
        len = 0;
    }

    public void OnPrevSelect()
    {
        if(len !=0 )
        {
            --len;
            GameManager.instance.dialedNumber = GameManager.instance.dialedNumber.Substring(0, len);
            keypadScreen.GetComponent<TextMeshProUGUI>().text = GameManager.instance.dialedNumber;
        }
            
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
        GameManager.instance.phoneDone = true;
        fireType.SetActive(false);
    }

    public void OnClothTpSelect()
    {
        GameManager.instance.onClothTP = true;
        GameManager.instance.onAlarmTP = false;
        GameManager.instance.onPhoneTP = false;
    }
    public void OnAlarmTpSelect()
    {
        GameManager.instance.onClothTP = false;
        GameManager.instance.onAlarmTP = true;
        GameManager.instance.onPhoneTP = false;
    }
    public void OnPhoneTpSelect()
    {
        GameManager.instance.onClothTP = false;
        GameManager.instance.onAlarmTP = false;
        GameManager.instance.onPhoneTP = true;
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

    int windowCount = 0;
    public void OnWindowSelect()
    {
        windowCount++;
        if (windowCount == 2)
        {
            GameManager.instance.windowsDone = true;
            GameManager.instance.windowTime = GameManager.instance.maskTime + GameManager.instance.totalTime;
        }
    }

    public void OnDoorSelect()
    {
        GameManager.instance.doorDone = true;
        GameManager.instance.windowTime = GameManager.instance.maskTime + GameManager.instance.totalTime;
    }
}
