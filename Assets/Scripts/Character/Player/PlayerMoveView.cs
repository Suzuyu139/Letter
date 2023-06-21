using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveView : MonoBehaviour
{
    [SerializeField] CharacterController _characterController = null;
    [SerializeField] Transform _cameraTransform = null;

    public void Move(Vector3 move, out float gravity)
    {
        gravity = move.y;
        Vector3 cameraForward = Vector3.Scale(_cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 characterMove = cameraForward * move.z + _cameraTransform.right * move.x;
        if(_characterController.isGrounded)
        {
            gravity = -0.5f;
        }
        characterMove.y = move.y;

        _characterController.Move(characterMove);
    }
}
