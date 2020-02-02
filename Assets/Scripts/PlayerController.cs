using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public int itemCount = 0;

    private Vector3 moveDirection = Vector3.zero;

    public List<ItemSO> inventory = new List<ItemSO> ();

    public GameObject interactionWindow;

    public InteractionButton button; 

    public TMP_Text message;

    public MessageDisplay messageDisplay;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.tag == "Item") {
            //itemCount++;
            //inventory.Add (collider.gameObject.GetComponent<Item> ().so);
            //GameObject.Destroy (collider.gameObject);
            interactionWindow.SetActive (true);
            ItemSO i = collider.gameObject.GetComponent<Item> ().so;
            // message.text = i.description;
            messageDisplay.ClearMessageList ();
            messageDisplay.AddMessage (i.description);
            StartCoroutine (messageDisplay.DisplayMessage ());
            button.SetAction (() => {inventory.Add (i); 
                                     interactionWindow.SetActive (false); 
                                     GameObject.Destroy (collider.gameObject);
                                     itemCount++; });
        }
    }

    void OnTriggerExit (Collider collider) {
        interactionWindow.SetActive (false);
    }

}
