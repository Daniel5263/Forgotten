using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Camera settings
    public Camera playerCam;

    // Movement settings
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpPower = 0f;
    public float gravity = 10f;

    // Look settings
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    // Zoom settings
    public int ZoomFOV = 35;
    public float initialFOV;
    public float cameraZoomSmooth = 1f;
    private bool isZoomed = false;
    public AudioSource cameraZoomSound;

    // Footstep sound settings
    public AudioManager audioManager;
    public float footstepInterval = 0.5f;
    private float footstepTimer = 0f;

    // Internal variables
    private Vector3 moveDirection;
    private float rotationX;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (initialFOV == 0)
            initialFOV = playerCam.fieldOfView;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleZoom();
    }

    void HandleMovement()
    {
        // Get input for movement
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 horizontalMove = (forward * inputVertical + right * inputHorizontal)
                                 .normalized * (isRunning ? runSpeed : walkSpeed);

        float verticalVelocity = moveDirection.y;
        moveDirection = horizontalMove;
        moveDirection.y = verticalVelocity;

        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpPower;

        // Apply gravity if not grounded
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        else if (moveDirection.y < 0)
            moveDirection.y = -2f;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Footstep sound logic: only when moving and grounded
        if (horizontalMove.magnitude > 0.1f && characterController.isGrounded)
        {
            // Footsteps play faster when running.
            float currentFootstepInterval = isRunning ? footstepInterval * 0.5f : footstepInterval;
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                if (audioManager && audioManager.playerFootsteps)
                    audioManager.footstepSource.PlayOneShot(audioManager.playerFootsteps);
                footstepTimer = currentFootstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
            if (audioManager && audioManager.footstepSource.isPlaying)
                audioManager.footstepSource.Stop();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX - mouseY, -lookXLimit, lookXLimit);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(0f, mouseX, 0f);
    }

    void HandleZoom()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = true;
            if (cameraZoomSound) cameraZoomSound.Play();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = false;
            if (cameraZoomSound) cameraZoomSound.Play();
        }

        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, isZoomed ? ZoomFOV : initialFOV, Time.deltaTime * cameraZoomSmooth);
    }
}
