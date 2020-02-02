using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskOptions : MonoBehaviour
{

    [SerializeField]
    private Button giveItemButton;

    [SerializeField]
    private Button askItemButton;

    [SerializeField]
    private InteractionPopup interactionPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveItemAction () {
        interactionPopup.ChangeState (InteractionPopup.State.PlayerChoices2, null, "", DAction.Action.Give);
    }

    public void AskItemAction () {
        interactionPopup.ChangeState (InteractionPopup.State.PlayerChoices2, null, "", DAction.Action.Ask);
    }

}
