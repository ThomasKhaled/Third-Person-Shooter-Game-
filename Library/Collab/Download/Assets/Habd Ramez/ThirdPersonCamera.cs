using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;
        public float Damping;
        public float CrouchHeight;
    }

    [SerializeField] CameraRig defaultCamera;
    [SerializeField] CameraRig aimCamera;

    Transform cameraLookTarget;
    PlayerScript localPlayer;



    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    void HandleOnLocalPlayerJoined(PlayerScript player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("AimingPivot");
        if (cameraLookTarget == null)
            cameraLookTarget = localPlayer.transform;
    }

    void Update()
    {
        if (localPlayer == null)
            return;

        CameraRig cameraRig = defaultCamera;

        if(localPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMING ||
            localPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMEDFIRING)
        {
            cameraRig = aimCamera;
        }

        float targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerStates.MoveState == PlayerStates.EMoveState.CROUCHING? cameraRig.CrouchHeight: 0);
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
        localPlayer.transform.up * targetHeight +
        localPlayer.transform.right * cameraRig.CameraOffset.x;

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookTarget.rotation, cameraRig.Damping * Time.deltaTime);
    }
}
