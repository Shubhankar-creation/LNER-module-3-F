using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInteration : MonoBehaviour
{
    [Header("Select Type")]

    [SerializeField]
    private InteractionType _interactionType = InteractionType._NONE;

    [Header("Door Interaction")]
    [SerializeField]
    private List<Transform> _doorTransform = new List<Transform>();
    [SerializeField]
    private List<Quaternion> _targetRotation = new List<Quaternion>();

    [Header("Window Interation")]
    [SerializeField]
    private List<Transform> _windowTransform = new List<Transform>();
    [SerializeField]
    private List<Vector3> _targetPosition = new List<Vector3>();

    [Header("Other Settings")]
    [SerializeField]
    private float _movementSpeed = 1f;

    private float timer = 0;

    private void Update()
    {
        if (_interactionType == InteractionType._NONE)
            return;

        else
        {
            if(timer < _movementSpeed)
            {
                timer += _movementSpeed * Time.deltaTime;
            }
            else
            {
                GetComponent<SelectInteration>().enabled = false;
            }
            if(_interactionType == InteractionType._DOOR)
            {
                for (int i = 0; i < _doorTransform.Count; i++)
                {
                    _doorTransform[i].localRotation = Quaternion.Lerp(_doorTransform[i].localRotation, _targetRotation[i], timer);
                }
            }
            else if(_interactionType == InteractionType._WINDOW)
            {
                for (int i = 0; i < _windowTransform.Count; i++)
                {
                    _windowTransform[i].localPosition = Vector3.Lerp(_windowTransform[i].localPosition, _targetPosition[i], timer);
                }
            }
        }
    }


}

enum InteractionType
{
    _NONE,
    _DOOR,
    _WINDOW
}
