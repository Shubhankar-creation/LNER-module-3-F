using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components"),Space(10)]

    [SerializeField] 
    private List<Transform> _waypointsToOutside = new List<Transform>();
    [SerializeField]
    private int _doorWaypointCount = 2;
    [SerializeField, Space(5)]
    private float _playerMoveSpeed = 5f;
    [SerializeField, Space(5)]
    private float _minDistanceToSwitch = 0.01f;

    private Animator _animator;

    private bool _hasShouted = false;
    private bool _doorOpened = false;
    private bool _doRun = false;
    private int waypointIndex = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_hasShouted)
        {
            if (Vector3.Distance(transform.position, _waypointsToOutside[waypointIndex].transform.position) < _minDistanceToSwitch)
            {
                if (waypointIndex + 1 < _waypointsToOutside.Count)
                {
                    transform.position = _waypointsToOutside[waypointIndex].transform.position;
                    waypointIndex++;
                    if (waypointIndex == _doorWaypointCount && !_doorOpened)
                    {
                        StopRun();
                    }
                }
                else
                {
                    StopRun();
                }
            }
            else if (_doRun)
            {
                Vector3 direction = (_waypointsToOutside[waypointIndex].transform.position - transform.position).normalized;
                Quaternion playerRotation = Quaternion.LookRotation(direction, transform.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, 100f);
                transform.Translate(Vector3.forward * _playerMoveSpeed * Time.deltaTime);
            }
        }
    }

    private void DoRun()
    {
        _doRun = true;
        _animator.SetBool("DoRun", true);
    }

    private void StopRun()
    {
        _doRun = false;
        _animator.SetBool("DoRun", false);
    }

    public void DoorOpened()
    {
        _doorOpened = true;
        DoRun();
    }

    public void Shouted()
    {
        _hasShouted = true;
        DoRun();
    }
}
