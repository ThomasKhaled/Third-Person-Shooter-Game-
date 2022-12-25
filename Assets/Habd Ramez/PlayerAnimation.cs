using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private PlayerAim playerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if (playerAim == null)
                playerAim = GameManager.Instance.LocalPlayer.playerAim;
            return playerAim;
        }
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);
        animator.SetBool("isWalking", GameManager.Instance.InputController.isWalking);
        animator.SetBool("isSprinting", GameManager.Instance.InputController.isSprinting);
        animator.SetBool("isCrouched", GameManager.Instance.InputController.isCrouched);

        animator.SetFloat("AimAngle", PlayerAim.GetAngle());
        //animator.SetBool("isAiming",
        //    GameManager.Instance.LocalPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMING ||
        //    GameManager.Instance.LocalPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMEDFIRING);

    }
}
