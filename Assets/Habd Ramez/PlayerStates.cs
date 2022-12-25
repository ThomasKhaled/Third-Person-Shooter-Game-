using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum EMoveState
    {
        WALKING,
        RUNING,
        CROUCHING,
        SPRINTING
    }
    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }
    public EMoveState MoveState;
    public EWeaponState WeaponState;

    private InputController ic;
    public InputController InputController
    {
        get
        {
            if (ic == null)
                ic = GameManager.Instance.InputController;
            return ic;
        }
    }

    private void Update()
    {
        SetMoveState();
        SetWeaponState();
    }

    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDLE;
        if (InputController.Fire1)
            WeaponState = EWeaponState.FIRING;
        if (InputController.Fire2)
            WeaponState = EWeaponState.AIMING;
        if (InputController.Fire1 && InputController.Fire2)
            WeaponState = EWeaponState.AIMEDFIRING;
    }

    void SetMoveState()
    {
        MoveState = EMoveState.RUNING;
        if (InputController.isSprinting)
            MoveState = EMoveState.SPRINTING;
        if (InputController.isWalking)
            MoveState = EMoveState.WALKING;
        if (InputController.isCrouched)
            MoveState = EMoveState.CROUCHING;
    }

}
