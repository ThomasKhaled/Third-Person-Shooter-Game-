using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Fire2;
    public bool Reload;
    public bool isWalking;
    public bool isSprinting;
    public bool isCrouched;

    void Update()
    {
        // Saving Value for Vertical Movement
        Vertical = Input.GetAxis("Vertical");
        // Saving Value for Horizontal Movement
        Horizontal = Input.GetAxis("Horizontal");
        // Saving Mouse X,Y Values
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Reload = Input.GetKey(KeyCode.R);
        isWalking = Input.GetKey(KeyCode.LeftAlt);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isCrouched = Input.GetKey(KeyCode.C);
    }
}
