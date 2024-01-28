using DG.Tweening;
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
    public Animator handAnimator;
    public LayerMask trapLayer;

    private bool isHandUp;
    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private bool isGrounded;
    private Vector3 velocity;
    private Vector3 moveDirection;
    float interactionRange = 2f;


    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private GameObject interactableUi;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private GameObject _labBase;


    public bool _tookKey;

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
        _startPos = gameObject.transform.position;

        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, characterController.height / 2f + .7f, groundMask);

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
        //handle anim
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isHandUp)
            {
                handAnimator.SetBool("handRaise", true);
                isHandUp = true;
                AudioSourceManager.Instance._sounds[4].Play();
            }
            else
            {
                handAnimator.SetBool("handRaise", false);
                isHandUp = false;
            }
        }


        bool hasInteractable = false; // Flag to check if any interactable object is within range

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);

        foreach (Collider col in hitColliders)
        {
            // Check if the interactable object has a specific script or component
            Interactable interactableObject = col.GetComponent<Interactable>();

            if (interactableObject != null)
            {
                // Set the flag to true if at least one interactable object is found
                hasInteractable = true;
                break; // No need to check further once an interactable is found
            }
        }

        // Activate or deactivate the UI based on the flag
        interactableUi.SetActive(hasInteractable);

    }
    private void TryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();


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


        //if (!isReversed)
        moveDirection = transform.TransformDirection(new Vector3(horizontalMove, 0f, verticalMove));
        //else
        //    moveDirection = transform.TransformDirection(new Vector3(-horizontalMove, 0f, -verticalMove));
        characterController.Move(moveDirection * speed * Time.deltaTime);


        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }


        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {

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
        if (other.gameObject.CompareTag("trap") || other.gameObject.CompareTag("yumruk"))
        {
            Debug.Log("die to trap");
            Die();
        }
        else if (other.gameObject.CompareTag("kıstırgaç"))
        {

            gameObject.transform.DOMove(_labBase.transform.position, .1f);
            Debug.Log("kıstırgaç");
            AudioSourceManager.Instance._sounds[7].Play();
        }

        else if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            _tookKey = true;
        }

        else if (other.gameObject.CompareTag("finishLine"))
        {
            if (_tookKey)
            {

                StartCoroutine(WaitForSceneLoad());
            }
        }




    }


    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(2);
    }
    #endregion

    public void Die()
    {
        DOTween.Clear(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
