using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField]
    private List<QuestSO> quests = new List<QuestSO> ();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string DoDialogueAction (DialogueAction da) {
        for (int i = 0; i < quests.Count; i++) { // Hits the first matching quest found
            if (quests [i].CheckDialougeAction (da)) {
                return quests [i].GetResponse ();
            }
        }
        return "No dialouge found.";
    }

}
