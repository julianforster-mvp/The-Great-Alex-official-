using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class alexcontroller : MonoBehaviour
{

    private Rigidbody rb;

    private int count;
    // Movement along X and Y axes.
    private float movementX;
    private float movementY;
    // Speed at which the player moves.
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Initial speed
    public float movementSpeed = 5f;
    // Amount to increase speed on collision
    public float speedIncreaseAmount = 2f;

    public float jumpAmount = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);
        }
    }
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        if (count >= 4) ;
        {
            winTextObject.SetActive(true);
        }

        countText.text = "Count: " + count.ToString();
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }
    

    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);

        //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();

            


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            movementSpeed += speedIncreaseAmount; // Increase speed
            Debug.Log("Speed increased! New speed: " + movementSpeed);
        }
    }


}
