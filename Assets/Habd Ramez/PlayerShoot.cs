using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] ShooterScript assaultRifle;

    private void Update()
    {
        if (GameManager.Instance.LocalPlayer.PlayerStates.MoveState == PlayerStates.EMoveState.SPRINTING)
            return;

        if(GameManager.Instance.InputController.Fire1)
        {
            assaultRifle.Fire();
        }
    }
}
