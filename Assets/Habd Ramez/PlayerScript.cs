using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(PlayerStates))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] Audiocontroller footSteps;
    [SerializeField] float minSteps;
    [SerializeField] int HP;
    [SerializeField] Text HPText;
    Vector3 startingPosition;
    float initimer = 1f;
    bool iniframes;
    Vector3 previousPos;
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

    public PlayerAim playerAim;

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

    private PlayerStates playerState;
    public PlayerStates PlayerStates
    {
        get
        {
            if (playerState == null)
                playerState = GetComponentInChildren<PlayerStates>();
            return playerState;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
        if (MouseControl.lockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        HPText.text = HP.ToString();
        iniframes = false;
        startingPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        LookAround();

        if (iniframes)
        {
            initimer -= Time.deltaTime;
            if (initimer <= 0)
            {
                initimer = 1f;
                iniframes = false;
            }
        }
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

        if (Vector3.Distance(transform.position, previousPos) > minSteps)
            footSteps.Play();
        previousPos = transform.position;
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);


        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {

            if (!iniframes)
            {
                iniframes = true;
                HP -= 10;
                HPText.text = HP.ToString();
            }
        }
        if (HP == 0)
        {
            transform.position = startingPosition;
            HP = 100;
            HPText.text = HP.ToString();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {

            if (!iniframes)
            {
                iniframes = true;
                HP -= 10;
                HPText.text = HP.ToString();
            }
        }
        if (HP == 0)
        {
            transform.position = startingPosition;
            HP = 100;
            HPText.text = HP.ToString();
        }
    }

}