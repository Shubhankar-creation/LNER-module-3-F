using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public UICanvasController cameraFollow;

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
        cameraFollow._unmovableCanvasTransform = welcomeWindow.transform;
        canvasPopSound = GetComponent<AudioSource>();
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
        cameraFollow._unmovableCanvasTransform = pinchTutorial.transform;
        pinchTutorial.SetActive(true);
        canvasPopSound.Play();
    }


    public void OnPinchSelect()
    {
        Debug.Log("Pinch pressed");
        pinchTutorial.SetActive(false);
        cameraFollow._unmovableCanvasTransform = directTutorial.transform;

        directTutorial.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnDirectSelect()
    {
        directTutorial.SetActive(false);
        cameraFollow._unmovableCanvasTransform = teleportTutorial.transform;

        teleportTutorial.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnTeleportSelect()
    {
        teleportTutorial.SetActive(false);
        cameraFollow._unmovableCanvasTransform = StartLevel.transform;

        StartLevel.SetActive(true);
        canvasPopSound.Play();
    }

    public void OnStartSelect()
    {
        StartLevel.SetActive(false);
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

        GameManager.instance.maskTime = GameManager.instance.totalTime;

        StartCoroutine(hideUI(GetBackToFire));
    }

    public void OnFireLocation()
    {
        if(GameManager.instance.inClothPlace == true)
            Precautions.SetActive(true);
    }
    IEnumerator hideUI(GameObject UI)
    {
        yield return new WaitForSeconds(5f);
        UI.SetActive(false);
    }

    public void DisplayReport(GameObject reportText)
    {
        int alertPercent = 100 - (int)GameManager.instance.alarmTime * GameManager.instance.averageAlert / 100;
        reportText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.alarmTime / 60).ToString("00") + ":" + ((int)GameManager.instance.alarmTime % 60).ToString("00") + "/" + alertPercent.ToString() + "%";

        int callPercent = 100 - (int)GameManager.instance.phoneTime * GameManager.instance.averageCall / 100;
        reportText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.phoneTime / 60).ToString("00") + ":" + ((int)GameManager.instance.phoneTime % 60).ToString("00") + "/" + callPercent.ToString() + "%";

        int maskPercent = 100 - (int)GameManager.instance.maskTime * GameManager.instance.averageMask / 100;
        reportText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.maskTime / 60).ToString("00") + ":" + ((int)GameManager.instance.maskTime % 60).ToString("00") + "/" + maskPercent.ToString() + "%";

        int shoutPercent = 100 - (int)GameManager.instance.totalTime * GameManager.instance.averageShout / 100;
        reportText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.totalTime / 60).ToString("00") + ":" + ((int)GameManager.instance.totalTime % 60).ToString("00") + "/" + shoutPercent.ToString() + "%"; //for evac

        int totalscore = (int)(((int)GameManager.instance.alarmTime + (int)GameManager.instance.phoneTime + (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime) / 4f);

        int percentage = 100 - totalscore * GameManager.instance.averageEvac / 100;
        reportText.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = percentage.ToString() + "%";

        int timeSpend = (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime;
        reportText.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (timeSpend / 60).ToString("00") + ":" + (timeSpend % 60).ToString("00");
    }

    public void OnPassOff()
    {
        pickTheExt.SetActive(false);
        grabPin.SetActive(true);
    }
    public void OnResetClick()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
