using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public UICanvasController cameraFollow;

    public GameObject followUI;
    public GameObject welcomeWindow;
    public GameObject pinchTutorial;
    public GameObject directTutorial;
    public GameObject teleportTutorial;
    public GameObject StartLevel;

    public GameObject calledForSafety;
    public GameObject selectCloth;
    public GameObject GetBackToFire;
    public GameObject Precautions;

    public GameObject fireScene;

    public GameObject pickTheExt;
    public GameObject grabPin;

    private AudioSource canvasPopSound;

    private bool timeStarted;

    void Start()
    {
/*        cameraFollow._unmovableCanvasTransform = welcomeWindow.transform;
*/        canvasPopSound = GetComponent<AudioSource>();
        StartCoroutine(setWelcomeWindow());
    }

    private void Update()
    {
        if(timeStarted)
        {
            GameManager.instance.totalTime += Time.deltaTime;
        }
        
    }
    IEnumerator setWelcomeWindow()
    {
        welcomeWindow.SetActive(true);
        canvasPopSound.Play();
        yield return new WaitForSeconds(5f);
        welcomeWindow.SetActive(false);
/*        cameraFollow._unmovableCanvasTransform = pinchTutorial.transform;
*/        pinchTutorial.SetActive(true);
        canvasPopSound.Play();
    }


    public void OnPinchSelect()
    {
        Debug.Log("Pinch pressed");
        pinchTutorial.SetActive(false);
/*        cameraFollow._unmovableCanvasTransform = directTutorial.transform;
*/
        directTutorial.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnDirectSelect()
    {
        directTutorial.SetActive(false);
/*        cameraFollow._unmovableCanvasTransform = teleportTutorial.transform;
*/
        teleportTutorial.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnTeleportSelect()
    {
        teleportTutorial.SetActive(false);
/*        cameraFollow._unmovableCanvasTransform = StartLevel.transform;
*/
        StartLevel.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnStartSelect()
    {
        StartLevel.SetActive(false);
        followUI.SetActive(false);
        fireScene.SetActive(true);
        timeStarted = true;
    }

    public void OnElectricFireSelect()
    {
        calledForSafety.SetActive(true);

        GameManager.instance.phoneTime = GameManager.instance.totalTime;

        StartCoroutine(hideUI(calledForSafety));
    }
    public void OnClothSelect()
    {
        selectCloth.SetActive(false);
        GetBackToFire.SetActive(true);

        GameManager.instance.maskDone = true;

        GameManager.instance.maskTime = GameManager.instance.totalTime;

        StartCoroutine(hideUI(GetBackToFire));
    }

    public void OnFireLocation()
    {
        Precautions.SetActive(true);
    }
    IEnumerator hideUI(GameObject UI)
    {
        yield return new WaitForSeconds(5f);
        UI.SetActive(false);
    }

    public void DisplayEvacReport(GameObject reportText)
    {
        int alertPercent = 0, callPercent = 0, maskPercent = 0, firePercent = 0, windowPercent = 0, doorPercent = 0, shoutPercent = 0;

        if (GameManager.instance.alertDone)
        {
            if (GameManager.instance.averageAlert >= (int)GameManager.instance.alarmTime)
            {
                alertPercent = 100;
            }
            else
            {
                alertPercent = 100 - ((int)GameManager.instance.alarmTime - GameManager.instance.averageAlert) / GameManager.instance.averageAlert;
            }

            reportText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.alarmTime / 60).ToString("00") + ":" + ((int)GameManager.instance.alarmTime % 60).ToString("00") + "/" + alertPercent.ToString() + "%";
            GameManager.instance.evacReportUIs.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(1).gameObject.SetActive(true);
            reportText.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (GameManager.instance.phoneDone)
        {
            if (GameManager.instance.averageCall >= (int)GameManager.instance.phoneTime)
            {
                callPercent = 100;
            }
            else
            {
                callPercent = 100 - ((int)GameManager.instance.phoneTime - GameManager.instance.averageCall) / GameManager.instance.averageCall;
            }
            reportText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.phoneTime / 60).ToString("00") + ":" + ((int)GameManager.instance.phoneTime % 60).ToString("00") + "/" + callPercent.ToString() + "%";
            GameManager.instance.evacReportUIs.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(3).gameObject.SetActive(true);
            reportText.transform.GetChild(1).gameObject.SetActive(false);
        }

        //respiratory
        if (GameManager.instance.maskDone)
        {
            if (GameManager.instance.averageMask >= (int)GameManager.instance.maskTime)
            {
                maskPercent = 100;
            }
            else
            {
                maskPercent = 100 - ((int)GameManager.instance.maskTime - GameManager.instance.averageMask) / GameManager.instance.averageMask;
            }
            reportText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.maskTime / 60).ToString("00") + ":" + ((int)GameManager.instance.maskTime % 60).ToString("00") + "/" + maskPercent.ToString() + "%";
            GameManager.instance.evacReportUIs.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(5).gameObject.SetActive(true);
            reportText.transform.GetChild(2).gameObject.SetActive(false);
        }

        //window
        if(GameManager.instance.windowsDone)
        {
            if(GameManager.instance.averageWindow >= GameManager.instance.windowTime)
            {
                windowPercent = 100;
            }
            else
            {
                windowPercent = 100 - ((int)GameManager.instance.windowTime - GameManager.instance.averageWindow) / GameManager.instance.averageWindow;
            }
            reportText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.windowTime / 60).ToString("00") + ":" + ((int)GameManager.instance.windowTime % 60).ToString("00") + "/" + windowPercent.ToString() + "%";
            GameManager.instance.evacReportUIs.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(7).gameObject.SetActive(true);
            reportText.transform.GetChild(3).gameObject.SetActive(false);
        }

        //door
        if (GameManager.instance.doorDone)
        {
            if (GameManager.instance.averageDoor >= GameManager.instance.doorTime)
            {
                doorPercent = 100;
            }
            else
            {
                doorPercent = 100 - ((int)GameManager.instance.doorTime - GameManager.instance.averageDoor) / GameManager.instance.averageDoor;
            }
            reportText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.doorTime / 60).ToString("00") + ":" + ((int)GameManager.instance.doorTime % 60).ToString("00") + "/" + doorPercent.ToString() + "%";
            GameManager.instance.evacReportUIs.transform.GetChild(8).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(9).gameObject.SetActive(true);
            reportText.transform.GetChild(4).gameObject.SetActive(false);
        }

        //alert surrounding
        if (GameManager.instance.shoutDone)
        {
            if (GameManager.instance.averageShout >= GameManager.instance.shoutTime)
            {
                shoutPercent = 100;
            }
            else
            {
                shoutPercent = 100 - ((int)GameManager.instance.shoutTime - GameManager.instance.averageShout) / GameManager.instance.averageShout;
            }
            reportText.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.shoutTime / 60).ToString("00") + ":" + ((int)GameManager.instance.shoutTime % 60).ToString("00") + "/" + shoutPercent.ToString() + "%"; //for evac
            GameManager.instance.evacReportUIs.transform.GetChild(10).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.evacReportUIs.transform.GetChild(11).gameObject.SetActive(true);
            reportText.transform.GetChild(5).gameObject.SetActive(false);
        }

        //total score        
        int totalScore = (alertPercent + callPercent + maskPercent  + windowPercent + doorPercent + shoutPercent) / 6;

        reportText.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = totalScore.ToString() + "%";

        //total Time
        int timeSpend = (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime;
        reportText.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = (timeSpend / 60).ToString("00") + ":" + (timeSpend % 60).ToString("00");
    }

    public void OnPassOff()
    {
        for (int i = 0; i < GameManager.instance.ropeMesh.transform.childCount; i++)
        {
            GameManager.instance.ropeMesh.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }

        pickTheExt.SetActive(false);
        GameManager.instance.canRemovePin = true;
        if (!GameManager.instance.pinRemoved)
            grabPin.SetActive(true);
        GameManager.instance.leftRayVisual.SetActive(false);
        GameManager.instance.rightRayVisual.SetActive(false);
        GameManager.instance.ovrManager.isInsightPassthroughEnabled = false;
    }
    public void OnResetClick()
    {
        GameManager.instance.totalTime = 0;
        GameManager.instance.alarmTime = 0;
        GameManager.instance.phoneTime = 0;
        GameManager.instance.maskTime = 0;
        GameManager.instance.windowTime = 0;
        GameManager.instance.doorTime = 0;
        GameManager.instance.shoutTime = 0;
        GameManager.instance.fireTime = 0;
        GameManager.instance.evacTime = 0;

        SceneManager.LoadScene(0);
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
