using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{

    public enum State {
        Hidden,
        ItemPickup,
        DisplayMessage,
        PlayerChoices1,
        PlayerChoices2
    }

    private readonly Dictionary<State, List<State>> map = new Dictionary<State, List<State>> () {
        {State.Hidden, new List<State> () {State.ItemPickup, State.DisplayMessage}},
        {State.ItemPickup, new List<State> () {State.Hidden}},
        {State.DisplayMessage, new List<State> () {State.Hidden, State.PlayerChoices1}},
        {State.PlayerChoices1, new List<State> () {State.Hidden, State.PlayerChoices2}},
        {State.PlayerChoices2, new List<State> () {State.Hidden, State.DisplayMessage}}
    };

    private State state = State.Hidden;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private MessageDisplay messageDisplay;

    [SerializeField]
    private InteractionButton button;

    [SerializeField]
    private AskOptions askOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState (State newState, Item item = null, string message = "", DAction.Action action = DAction.Action.Ask) {
        if (map [state].Contains (newState)) {
            state = newState;
            UpdateFromState (item, message, action);
        }
    }

    public void UpdateFromState (Item item, string message, DAction.Action action) {
        switch (state) {
            case State.Hidden:
                Hide ();
                break;
            case State.DisplayMessage:
                ShowMessage (message);
                break;
            case State.ItemPickup:
                PickupItem (item);
                break;
            case State.PlayerChoices1:
                DisplayPrimaryChoices ();
                break;
            case State.PlayerChoices2:
                DisplaySecondaryChoices (action);
                break;
            default:
                Debug.LogWarning ("Forgot to add state change");
                break;
        }
    }

    private void Hide () {
        gameObject.SetActive (false);
        button.gameObject.SetActive (false);
        askOptions.gameObject.SetActive (false);
    }

    private void ShowMessage (string message) {
        gameObject.SetActive (true);
        messageDisplay.ClearMessageList ();
        messageDisplay.AddMessage (message);
        messageDisplay.StartCoroutine (messageDisplay.DisplayMessage ());
    }

    private void PickupItem (Item item) {
        gameObject.SetActive (true);
        button.gameObject.SetActive (true);
        messageDisplay.ClearMessageList ();
        messageDisplay.AddMessage (item.so.description);
        messageDisplay.StartCoroutine (messageDisplay.DisplayMessage ());
        button.transform.GetChild (0).GetComponent<TMPro.TMP_Text> ().text = "Pick up " + item.so.itemName;
        button.SetAction (() => {player.AddToInventory (item.so);
                                 button.gameObject.SetActive (false);
                                 GameObject.Destroy (item.gameObject); });
    }

    private void DisplayPrimaryChoices () {
        askOptions.gameObject.SetActive (true);
    }

    private void DisplaySecondaryChoices (DAction.Action action) {
        
    }

}