using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasController : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private OVRCameraRig _cameraRig;
    [SerializeField]
    public Transform _unmovableCanvasTransform;
    [SerializeField]
    private float _movableCanvasOffset = 5f;
    [SerializeField]
    private float _minDistance = 1f;
    [SerializeField]
    private float smoothTime = 1f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Vector3 pos = _targetTransform.position;
        pos.z += _movableCanvasOffset;
        _targetTransform.position = pos;
    }

    private void FixedUpdate()
    {
        UnmovableCanvasFollow();
    }

    private void UnmovableCanvasFollow()
    {
        //Unmovable Canvas
        //Rotation
        Vector3 lookPos = _unmovableCanvasTransform.position - _cameraRig.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        _unmovableCanvasTransform.rotation = Quaternion.Slerp(_unmovableCanvasTransform.rotation, rotation, smoothTime);

        //Position
        float distance = Vector3.Distance(_targetTransform.position, _unmovableCanvasTransform.position);
        if (distance > _minDistance)
        {
            float time = smoothTime / distance;
            Vector3 cameraPos = _targetTransform.position;
            cameraPos.y = _unmovableCanvasTransform.transform.position.y;
            _unmovableCanvasTransform.position = Vector3.SmoothDamp(_unmovableCanvasTransform.position, cameraPos, ref velocity, time);
        }
    }
}