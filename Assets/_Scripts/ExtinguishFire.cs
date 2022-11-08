using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExtinguishFire : MonoBehaviour
{
    public GameObject Fire;
    public GameObject ReportUI;
    public GameObject wrongExtUI;

    //Alarm, phone, mask, fire, score, total 
    public GameObject reportText;
    public Image bloodImage;

    private float bloodTime;

    private float averageFireTime = 0f;
    public void Update()
    {

        if (GameManager.instance.pinRemoved && averageFireTime <= 60f)
        {
            averageFireTime += Time.deltaTime;
        }
        else if (averageFireTime > 60f)
        {
            GameManager.instance.pinRemoved = false;
            DisplayReport();
            ReportUI.SetActive(true);
            GameManager.instance.canExt = false;
            this.gameObject.GetComponent<ExtinguishFire>().enabled = false;
        }

        if (GameManager.instance.canExt && GameManager.instance.pinRemoved && OVRInput.Get(OVRInput.Button.One))
        {
            if(GameManager.instance.particles.CompareTag("CO2") || GameManager.instance.particles.CompareTag("Dry"))
            {
                ReduceFire(0.0075f);
            }
            else
            {
                bloodTime += Time.deltaTime;
                if (bloodTime < GameManager.instance.wrongExtTime)
                {
                    var newColor = new Color(1.0f, 0f, 0f, bloodImage.color.a + 0.01f);
                    bloodImage.color = newColor;
                }
                else
                {
                    DisplayReport();
                    wrongExtUI.SetActive(true);
                    GameManager.instance.canExt = false;
                }
            }
        }
        /*else if(GameManager.instance.extinguisherSize <=0)
        {
            DisplayReport();
            ReportUI.SetActive(true);
            GameManager.instance.canExt = false;
            this.gameObject.GetComponent<ExtinguishFire>().enabled = false;
        }*/
    }

    void ReduceFire(float val)
    {
        GameManager.instance.extinguisherSize -= Time.deltaTime;

        for (int i =0; i<12;i++)
        {
            if(Fire.transform.GetChild(i).localScale.x > 0)
            {
                Fire.transform.GetChild(i).localScale -= new Vector3(val, val, val);
            }
            else
            {
                //add report UI change
/*                successfulExt.SetActive(true);
*/
                Fire.GetComponent<AudioSource>().Stop();

                GameManager.instance.rightRayVisual.SetActive(true);

                GameManager.instance.fireTime = GameManager.instance.totalTime;

                GameManager.instance.fireDone = true;

                DisplayReport();
                ReportUI.SetActive(true);
                GameManager.instance.canExt = false;
                this.gameObject.GetComponent<ExtinguishFire>().enabled = false;
            }
        }
    }

    public void DisplayReport()
    {
        int alertPercent = 0, callPercent = 0, maskPercent = 0, firePercent = 0;

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
            GameManager.instance.fireReportUIs.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.fireReportUIs.transform.GetChild(1).gameObject.SetActive(true);
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
            GameManager.instance.fireReportUIs.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.fireReportUIs.transform.GetChild(3).gameObject.SetActive(true);
            reportText.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (GameManager.instance.maskDone)
        {
            if(GameManager.instance.averageMask >= (int)GameManager.instance.maskTime)
            {
                maskPercent = 100;
            }    
            else
            {
                maskPercent = 100 - ((int)GameManager.instance.maskTime - GameManager.instance.averageMask) / GameManager.instance.averageMask;
            }
            reportText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.maskTime / 60).ToString("00") + ":" + ((int)GameManager.instance.maskTime % 60).ToString("00") + "/" + maskPercent.ToString() + "%";
            GameManager.instance.fireReportUIs.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.fireReportUIs.transform.GetChild(5).gameObject.SetActive(true);
            reportText.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (GameManager.instance.fireDone)
        {
            if(GameManager.instance.averageFire >= (int)GameManager.instance.totalTime)
            {
                firePercent = 100;
            }
            else
            {
                firePercent = 100 - ((int)GameManager.instance.totalTime - GameManager.instance.averageFire) / GameManager.instance.averageFire;
            }
            reportText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.totalTime / 60).ToString("00") + ":" + ((int)GameManager.instance.totalTime % 60).ToString("00") + "/" + firePercent.ToString() + "%"; //for evac
            GameManager.instance.fireReportUIs.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.fireReportUIs.transform.GetChild(7).gameObject.SetActive(true);
            reportText.transform.GetChild(3).gameObject.SetActive(false);
        }

        //total Score
        int totalScore = (alertPercent + callPercent + maskPercent + firePercent) / 4;
        reportText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = totalScore.ToString() + "%";

        //total Time
        int timeSpend = (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime;
        reportText.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (timeSpend / 60).ToString("00") + ":" + (timeSpend % 60).ToString("00");
    }
    public void ExtenguishingFire()
    {
        GameManager.instance.canExt = true;
    }

    public void OnUnhoverFire()
    {
        GameManager.instance.canExt = false;
    }
}
