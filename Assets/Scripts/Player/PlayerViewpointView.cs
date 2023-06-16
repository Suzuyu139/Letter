using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewpointView : MonoBehaviour
{
    [SerializeField] Transform _camera = null;

    Transform _charaTransform = null;

    private void Awake()
    {
        _charaTransform = this.transform;
    }

    public void UpdateRotation(Quaternion cameraRot, Quaternion charaRot)
    {
        _camera.localRotation = cameraRot;
        _charaTransform.Rotate(charaRot.eulerAngles);
    }

    public Quaternion GetCameraRotation()
    {
        return _camera.localRotation;
    }

    public Quaternion GetCharaRotation()
    {
        return _charaTransform.localRotation;
    }
}
