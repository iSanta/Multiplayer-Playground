using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;


// sobre el asunto del authoritative en el player
// https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform/index.html#owner-authoritative-mode
public class PlayerMovement : NetworkBehaviour
{
    public Transform cameraTransform;
    public float speed = 5.0f;
    public float rotationSpeed = 3.0f;
    public float jumpForce = 5.0f; // Fuerza del salto

    private Rigidbody rb;
    private bool isGrounded = true;
    [SerializeField] GameObject cam;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (!IsOwner || !IsClient) return;
        cam.SetActive(true);
        transform.position = new Vector3(0, 1, 0);
        
        
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }



    private void Update()
    {
        if (!IsOwner || !IsClient) return;
        // Movimiento del personaje
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        inputDirection = cameraTransform.TransformDirection(inputDirection);
        inputDirection.y = 0;
        inputDirection.Normalize();

        Vector3 movement = inputDirection * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotación de la cámara con el mouse
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0, mouseX * rotationSpeed, 0);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar si el personaje está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
