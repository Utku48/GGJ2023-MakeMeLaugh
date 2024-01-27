using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = 14f;
    public LayerMask groundMask;
    public bool isReversed;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private bool isGrounded;
    private Vector3 velocity;
    private Vector3 moveDirection;

    [SerializeField] private ParticleSystem _dust;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, characterController.height / 2f + .5f, groundMask);

        // Handle player movement
        HandleMovement();

        // Handle jumping
        HandleJump();

        // Handle player rotation
        HandleRotation();

        // Lock and unlock cursor
        HandleCursorLock();

        //handle interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }
    private void TryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // Cast a ray forward from the player's position
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            // Check if the hit object has an IInteractable component
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            // If it does, call the Interact method
            if (interactable != null)
            {
                interactable.interact();
            }
        }
    }

    void HandleMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");


        if(!isReversed)
            moveDirection = transform.TransformDirection(new Vector3(horizontalMove, 0f, verticalMove));
        else
            moveDirection = transform.TransformDirection(new Vector3(verticalMove , 0f, horizontalMove));
        characterController.Move(moveDirection * speed * Time.deltaTime);

        // Apply gravity to the velocity
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        // Move the character based on the velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Reset y-velocity to prevent floating
            velocity.y = Mathf.Sqrt(2f * jumpForce * gravity);
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #region Çarpışmalar
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            PlayerController.Instance.Die();
        }
    }

        #endregion

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
