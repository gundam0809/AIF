using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float speed = 3f;
    public float turnSpeed = 1f;
    public Rigidbody rb;
    public float currentYRotation;
    Vector3 currentRotation;
    public float horizontatInput;
    public Transform orientation;
    Vector3 moveDirection;
    public LayerMask seaweed;
    public bool reward = true;

    public GameManager gameManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {

        
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                horizontatInput = Input.GetAxisRaw("Horizontal");
                moveDirection = orientation.forward * horizontatInput;
                rb.AddForce(moveDirection.normalized * moveSpeed * -5, ForceMode.Force);

                //clamp the velocity

                Debug.Log("Crabs games are good");
            }
        
            if (Input.GetKeyDown(KeyCode.W)) {
            
                transform.Rotate(0, 5 * turnSpeed, 0);
            }
        if (Input.GetKeyDown(KeyCode.S))
        {
                transform.Rotate(0, -5 * turnSpeed, 0);
            }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Green"))
        {
            Debug.Log("Unicorn gundam is better then 00");
            gameManager.AddGreen();
            Debug.Log("Mr Knight");
        }
        if (collision.gameObject.CompareTag("Net"))
        {

        }

    }
}
