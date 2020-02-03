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

    [SerializeField]
    private Transform selectOptions;

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
        selectOptions.gameObject.SetActive (false);
    }

    private void ShowMessage (string message) {
        gameObject.SetActive (true);
        selectOptions.gameObject.SetActive (false);
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
                                 GameObject.Destroy (item.gameObject); 
                                 ChangeState (State.Hidden); });
    }

    private void DisplayPrimaryChoices () {
        askOptions.gameObject.SetActive (true);
    }

    private void DisplaySecondaryChoices (DAction.Action action) {
        Debug.Log ("choices2");
        askOptions.gameObject.SetActive (false);
        selectOptions.gameObject.SetActive (true);
        List<GameObject> buttons;
        if (action == DAction.Action.Ask) {
            buttons = player.GenerateButtons (player.KnownItems, action);
        } else {
            buttons = player.GenerateButtons (player.Inventory, action);
        }

        float availableSize = 600f / buttons.Count;
        float buttonWidth = availableSize * (2f/3f);
        if (buttonWidth > 50) {
            buttonWidth = 50;
        }

        float pos = 0;
        for (int i = 0; i < buttons.Count; i++) {
            buttons [i].transform.SetParent (selectOptions);
            ((RectTransform)buttons [i].transform).sizeDelta = new Vector2 (buttonWidth, buttonWidth);
            pos = -300 + (i * availableSize) + (0.5f * availableSize);
            buttons [i].transform.localPosition = new Vector3 (pos, 0, 0);
        }

    }

    public void GetRidOfButtons () {
        foreach (Transform t in selectOptions) {
            GameObject.Destroy (t.gameObject);
        }
    }

}