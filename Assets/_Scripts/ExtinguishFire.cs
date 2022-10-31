using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExtinguishFire : MonoBehaviour
{
    private bool canExt;
    public GameObject Fire;
    public GameObject ReportUI;
    public GameObject wrongExtUI;

    //Alarm, phone, mask, fire, score, total 
    public GameObject reportText;
    public Image bloodImage;

    private float bloodTime;
    public void Update()
    {
        if (canExt && GameManager.instance.pinRemoved && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.8f)
        {
            if(GameManager.instance.particles.CompareTag("CO2"))
            {
                ReduceFire(0.0046f);
                GameManager.instance.extinguisherSize -= Time.deltaTime;
            }
            else if (GameManager.instance.particles.CompareTag("Dry"))
            {
                ReduceFire(0.0025f);
                GameManager.instance.extinguisherSize -= Time.deltaTime;
            }
            else
            {
                if(bloodTime < GameManager.instance.wrongExtTime)
                {
                    var newColor = new Color(1.0f, 0f, 0f, bloodImage.color.a + 0.005f);
                    bloodImage.color = newColor;
                }
                else
                {
                    DisplayReport();
                    wrongExtUI.SetActive(true);
                }
                bloodTime += Time.deltaTime;
            }
        }
    }

    void ReduceFire(float val)
    {
        for(int i =0; i<12;i++)
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

                GameManager.instance.rayVisual.SetActive(true);

                GameManager.instance.fireTime = GameManager.instance.totalTime;

                DisplayReport();
                ReportUI.SetActive(true);
                this.gameObject.GetComponent<ExtinguishFire>().enabled = false;
            }
        }
    }

    public void DisplayReport()
    {

        int alertPercent = 100 - (int)GameManager.instance.alarmTime * GameManager.instance.averageAlert / 100;
        reportText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.alarmTime / 60).ToString("00") + ":" + ((int)GameManager.instance.alarmTime % 60).ToString("00") + "/" + alertPercent.ToString() + "%";

        int callPercent = 100 -  (int)GameManager.instance.phoneTime * GameManager.instance.averageCall / 100;
        reportText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.phoneTime / 60).ToString("00") + ":" + ((int)GameManager.instance.phoneTime % 60).ToString("00") + "/" + callPercent.ToString() + "%";

        int maskPercent = 100 - (int)GameManager.instance.maskTime * GameManager.instance.averageMask / 100;
        reportText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.maskTime / 60).ToString("00") + ":" + ((int)GameManager.instance.maskTime % 60).ToString("00") + "/" + maskPercent.ToString() + "%";

        int firePercent = 100 - (int)GameManager.instance.totalTime * GameManager.instance.averageFire / 100;
        reportText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ((int)GameManager.instance.totalTime / 60).ToString("00") + ":" + ((int)GameManager.instance.totalTime % 60).ToString("00") + "/" + firePercent.ToString() + "%"; //for evac

        int totalscore =  (int)(((int)GameManager.instance.alarmTime + (int)GameManager.instance.phoneTime + (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime) / 4f);

        int percentage = 100 - totalscore * GameManager.instance.averageFight / 100;
        reportText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = percentage.ToString() + "%";

        int timeSpend = (int)GameManager.instance.maskTime + (int)GameManager.instance.totalTime;
        reportText.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (timeSpend / 60).ToString("00") + ":" + (timeSpend % 60).ToString("00");
    }
    public void ExtenguishingFire()
    {
        canExt = true;
    }

    public void OnUnhoverFire()
    {
        canExt = false;
    }
}
