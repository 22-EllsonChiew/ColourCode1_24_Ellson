using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isOnGround;
    float speed = 10.0f;
    float planeAabsLimits = 10.0f;
    float planeBabsLimits = 5.0f;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float zLimit = 10.0f;
    float xLimit = 10.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;


    // Start is called before the first frame update
    void Start()
    {

        isOnGround = true;
        
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //Player Jump
        PlayerJump();

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[0].color;
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[1].color;
        }
        if (transform.position.x < -zLimit)
        {
            transform.position = new Vector3(-zLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.x > zLimit)
        {
            transform.position = new Vector3(zLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[3].color;
        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Plane"))
        {
            isOnGround = true;


            playerRdr.material.color = playerMtrs[4].color;
        }




    }


    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;


            playerRdr.material.color = playerMtrs[5].color;
        }
    }
}
