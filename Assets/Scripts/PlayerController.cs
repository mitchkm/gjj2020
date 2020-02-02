using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

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

    [SerializeField]
    private List<ItemSO> inventory = new List<ItemSO> ();

    public List<ItemSO> Invetory {
        get {
            return inventory;
        }
    }

    private NPC currentNPC;

    // [SerializeField]
    // private GameObject interactionWindow;

    // [SerializeField]
    // private InteractionButton button; 

    // [SerializeField]
    // private TMP_Text message;

    // [SerializeField]
    // private MessageDisplay messageDisplay;

    // [SerializeField]
    // private TMP_Dropdown selectAction;

    // [SerializeField]
    // private TMP_Dropdown selectItemOrCharacter;

    // [SerializeField]
    // private GameObject dropDownPanel;

    [SerializeField]
    private InteractionPopup interactionPopup;

    private List<ItemSO> knownItems = new List<ItemSO> ();

    public List<ItemSO> KnownItems {
        get {
            return knownItems;
        }
    }

    // private List<GameObject> knownCharacters = new List<GameObject> ();

    // public List<GameObject> KnownCharacters {
    //     get {
    //         return knownCharacters;
    //     }
    // }

    // private List<GameObject> metCharacters = new List<GameObject> ();

    // public List<GameObject> MetCharacters {
    //     get {
    //         return metCharacters;
    //     }
    // }

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
            ItemInteraction (collider);
        } else if (collider.gameObject.tag == "NPC") {
            NPCInteraction (collider);
        }
    }

    private void ItemInteraction (Collider collider) {
        // interactionWindow.SetActive (true);
        // ItemSO i = collider.gameObject.GetComponent<Item> ().so;
        // messageDisplay.ClearMessageList ();
        // messageDisplay.AddMessage (i.description);
        // StartCoroutine (messageDisplay.DisplayMessage ());
        // button.gameObject.SetActive (true);
        // button.SetAction (() => {AddToInventory (i);
        //                          interactionWindow.SetActive (false);
        //                          button.gameObject.SetActive (false);
        //                          GameObject.Destroy (collider.gameObject); });
        interactionPopup.ChangeState (InteractionPopup.State.ItemPickup, collider.GetComponent <Item> ());
    }

    private void NPCInteraction (Collider collider) {
        currentNPC = collider.gameObject.GetComponent<NPC> ();
        interactionPopup.ChangeState (InteractionPopup.State.DisplayMessage, null, currentNPC.Interact (new DAction ()));
        // interactionWindow.SetActive (true);
        // NPC npc = collider.gameObject.GetComponent<NPC> ();
        // messageDisplay.ClearMessageList ();

        // string s = npc.DoDialogueAction (new DialogueAction (DialogueAction.Action.Greet));
        // messageDisplay.AddMessage (s);
        // StartCoroutine (messageDisplay.DisplayMessage ());
        // dropDownPanel.SetActive (true);
        // selectAction.ClearOptions ();
        // selectAction.AddOptions (Enum.GetNames (typeof (DialogueAction.Action)).ToList ());
        // OnActionDropdownChanged ();
    }

    public void OnActionDropdownChanged () {

    }

    void OnTriggerExit (Collider collider) {
        // interactionWindow.SetActive (false);
        // dropDownPanel.SetActive (false);
        interactionPopup.ChangeState (InteractionPopup.State.Hidden);
    }

    public void AddToInventory (ItemSO i) {
        inventory.Add (i);
        itemCount++;
    }

    public void AddToKnown (ItemSO i) {
        knownItems.Add (i);
    }

    // public GameObject ConvertItemToImage (ItemSO item) {
    //     GameObject go = new GameObject ();
    //     go.AddComponent<Image> ();
    //     Image i = go.GetComponent<Image> ();
    //     Sprite s = Resources.Load<Sprite> ("Sprites/Items/" + item.itemName); // Might need + ".png"
    //     i.sprite = s;

    //     return go;
    // }

}
