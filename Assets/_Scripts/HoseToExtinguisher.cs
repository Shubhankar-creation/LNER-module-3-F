using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseToExtinguisher : MonoBehaviour
{
    public GameObject parentExtinguisher;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(displaceChild(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator displaceChild(int x)
    {
        yield return new WaitForSeconds(x);

        int childrenCount = transform.GetChild(1).childCount;

        Transform child0 = transform.GetChild(1).GetChild(0);
        child0.transform.parent = parentExtinguisher.transform;
        child0.GetComponent<Rigidbody>().isKinematic = true;
        child0.transform.localPosition = new Vector3(0.022799f, 0.00290f, 0.293f);
        child0.transform.localRotation = Quaternion.FromToRotation(child0.localRotation.eulerAngles, new Vector3(-9.613f, 93.22f, 43.06f));


        //Debug.LogError(childrenCount);
        Transform childLast = transform.GetChild(1).GetChild(childrenCount - 2);
        childLast.transform.parent = GameManager.instance.rightAnchor.transform;
        childLast.GetComponent<Rigidbody>().isKinematic = true;
        childLast.transform.localPosition = new Vector3(0, 0, 0);
        childLast.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
