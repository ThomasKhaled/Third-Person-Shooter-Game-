using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D image;
    [SerializeField] int size;

    private void OnGUI()
    {
        if (GameManager.Instance.LocalPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMING ||
           GameManager.Instance.LocalPlayer.PlayerStates.WeaponState == PlayerStates.EWeaponState.AIMEDFIRING)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x -size/2, screenPosition.y-size/2, size, size), image);
        }
    }
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
       
    }
}
