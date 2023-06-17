using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewpointView : MonoBehaviour
{
    [SerializeField] Transform _camera = null;

    Transform _charaTransform = null;
    Camera _mainCamera = null;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _charaTransform = this.transform;

        _mainCamera.transform.SetParent(_camera);
        _mainCamera.transform.localPosition = Vector3.zero;
        _mainCamera.transform.localRotation = Quaternion.identity;
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
