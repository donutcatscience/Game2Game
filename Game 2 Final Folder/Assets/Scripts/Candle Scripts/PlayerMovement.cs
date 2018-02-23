using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    public Text gemText;
    public Text modeText;
    public int gemCount = 0;

    private string currentMode = "None";

    void Awake()
    {
        floorMask = LayerMask.GetMask("Ground");
        playerRigidbody = GetComponent<Rigidbody>();
        gemText.text = "Gem Count: " + gemCount;
    }

    void FixedUpdate ()
    {
        gemText.text = "Gem Count: " + gemCount;
        if (!Input.GetKey(KeyCode.Space))
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Move(horizontal, vertical);
        }

        // Contuine to Turn character 
        Turning();

        //handle Mode Text
        if (Input.GetKey(KeyCode.LeftShift)) { currentMode = "Utility"; }
        else if (Input.GetKey(KeyCode.Space)) { currentMode = "Caster"; }
        else { currentMode = "None"; }
        modeText.text = "Current Mode: " + currentMode;
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * speed * Time.deltaTime * GetComponent<RunSpeedIncrease>().bonusRunSpeed;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //collects gems
        if (other.gameObject.CompareTag("Pick Up"))
        {
            Destroy(other.gameObject);
            int randomGemValue = Random.Range(10, 25);
            Debug.Log(randomGemValue);
            gemCount += randomGemValue;
        }

    }
}