using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private List<QuestSO> quests = new List<QuestSO> ();
    [SerializeField]
    private List<DARPSO> responses = new List<DARPSO>();

    [SerializeField]
    private DResponseSO giveFailure;
    [SerializeField]
    private DResponseSO askFailure;

    [SerializeField]
    private DResponseSO greetings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual string Interact(DAction playerAction) {
        string reply;
        foreach (QuestSO quest in quests) {
            reply = quest.DoAction(playerAction, player);
            if (reply != null) return reply;
        }
        foreach (DARPSO response in responses) {
            if (response.IsTriggered(playerAction)) {
                return response.GetResponse();
            }
        }
        switch (playerAction.verb) {
            case DAction.Action.Ask:
                if (askFailure) return askFailure.GetResponse();
                break;
            case DAction.Action.Give:
                if (giveFailure) return giveFailure.GetResponse();
                break;
            case DAction.Action.Greet:
                if (greetings) return greetings.GetResponse();
                break;
            default:
                return "听不懂";
        }
        return "Something has gone wrong. Get out now before it's too late.";
    }

    public bool AllQuestsCompleted() {
        foreach (QuestSO quest in quests) {
            if (!quest.IsCompleted()) return false;
        }
        return true;
    }
}
