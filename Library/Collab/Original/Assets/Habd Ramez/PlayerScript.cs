using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class PlayerScript : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool lockMouse;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] MouseInput MouseControl;

    private MoveController moveController;
    public MoveController MoveController
    {
        get
        {
            if (moveController == null)
                moveController = GetComponent<MoveController>();
            return moveController;
        }
    }

    private Crosshair crosshair;
    private Crosshair Crosshair
    {
        get
        {
            if (crosshair == null)
                crosshair = GetComponentInChildren<Crosshair>();
            return crosshair;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;

      if(MouseControl.lockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        LookAround();
    }

    void Move()
    {
        float moveSpeed = runSpeed;

        if (playerInput.isWalking)
            moveSpeed = walkSpeed;

        if (playerInput.isSprinting)
            moveSpeed = sprintSpeed;

        if (playerInput.isCrouched)
            moveSpeed = crouchSpeed;

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        MoveController.Move(direction);
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);


        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
    }

}
