using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveView : MonoBehaviour
{
    [SerializeField] CharacterController _characterController = null;
    [SerializeField] Transform _cameraTransform = null;

    public void Move(Vector3 move)
    {
        Vector3 cameraForward = Vector3.Scale(_cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        _characterController.Move(cameraForward * move.z + _cameraTransform.right * move.x);
    }
}
