using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacFireManager : MonoBehaviour
{
    public GameObject tpFire1, tpFire2, tpFire3;

    private bool canEvacuate;
    private bool firstTP, secondTP, thirdTP;

    void Update()
    {
        if(firstTP)
        {
            for(int i = 0; i< tpFire1.transform.childCount; i++)
            {
                tpFire1.transform.GetChild(i).transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            }
        }
        if (secondTP)
        {
            for (int i = 0; i < tpFire2.transform.childCount; i++)
            {
                tpFire2.transform.GetChild(i).transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            }
        }
        if (thirdTP)
        {
            for (int i = 0; i < tpFire3.transform.childCount; i++)
            {
                tpFire3.transform.GetChild(i).transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            }
        }
    }

    public void OnEvacSelect()
    {
        canEvacuate = true;
    }
    public void OnFirstTp()
    {
        if(canEvacuate)
        {
            tpFire1.SetActive(true);
            firstTP = true;
        }
    }
    public void OnSecondTp()
    {
        if(canEvacuate)
        {
            tpFire1.SetActive(true);
            tpFire2.SetActive(true);

            firstTP = true;
            secondTP = true;
        }
    }
    public void OnThirdTp()
    {
        if (canEvacuate)
        {
            tpFire1.SetActive(true);
            tpFire2.SetActive(true);
            tpFire3.SetActive(true);
            firstTP = true;
            secondTP = true;
            thirdTP = true;
        }
    }
}
