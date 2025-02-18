using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 200f;
    public Transform cameraTransform;

    public AudioManager audioManager;
    public float footstepInterval = 0.912f;

    private float xRotation = 0f;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * v + right * h).normalized;
        bool isMoving = (movement.magnitude > 0.1f);

        if (isMoving)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            if (!audioManager.footstepSource.isPlaying)
            {
                audioManager.footstepSource.Play();
            }
        }
        else
        {
            if (audioManager.footstepSource.isPlaying)
            {
                audioManager.footstepSource.Stop();
            }
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
