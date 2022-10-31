using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingFire : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireOne;
    [SerializeField]
    private GameObject _fireTwo;
    private bool evacuateFire = false;

    public void Evacuate() => evacuateFire = true;

    public void FireOne()
    {
        if(evacuateFire)
        {
            _fireOne.SetActive(true);
        }
    }

    public void FireTwo()
    {
        if(evacuateFire)
        {
            _fireTwo.SetActive(true);
        }
    }
}
