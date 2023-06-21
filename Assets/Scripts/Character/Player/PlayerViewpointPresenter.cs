using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using VContainer;

public class PlayerViewpointPresenter : MonoBehaviour
{
    PlayerModel _model;
    PlayerViewpointView _viewpointView;

    Quaternion _cameraRot = new Quaternion();
    Quaternion _charaRot = new Quaternion();

    [Inject]
    void Construct(PlayerModel model, PlayerViewpointView viewpoint)
    {
        _model = model;
        _viewpointView = viewpoint;
    }

    private void Start()
    {
        _cameraRot = _viewpointView.GetCameraRotation();
        _charaRot = _viewpointView.GetCharaRotation();

        this.LateUpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);
    }

    void OnUpdate(Unit unit)
    {
        var data = _model.InputData.Value;
        _cameraRot *= Quaternion.Euler(-data.ViewpointY * Time.deltaTime * _model.SensiVertical, 0.0f, 0.0f);
        _charaRot = Quaternion.Euler(0.0f, data.ViewpointX * Time.deltaTime * _model.SensiHorizontal, 0.0f);
        _cameraRot = ClampRotation(_cameraRot);

        _viewpointView.UpdateRotation(_cameraRot, _charaRot);
    }

    Quaternion ClampRotation(Quaternion rot)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        rot.x /= rot.w;
        rot.y /= rot.w;
        rot.z /= rot.w;
        rot.w = 1f;

        float angleX = Mathf.Atan(rot.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, _model.ViewpointVerticalMin, _model.ViewpointVerticalMax);

        rot.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return rot;
    }
}
