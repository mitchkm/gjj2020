using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 4)]
public class QuestSO : ScriptableObject
{

    [SerializeField]
    private DialougeAction action;

    [SerializeField]
    private List<string> response = new List<string> ();

    [SerializeField]
    private ItemSO reward;

    [SerializeField]
    private CharacterList redirect;

    [SerializeField]
    private bool useRedirect = true;

    public string GetResponse () {
        if (reward == null) {
            useRedirect = true;
        }
        if (response.Count > 0) {
            return String.Format(response [0], useRedirect ? redirect.ToString () : reward.itemName);
        }
        return "No Dialouge";
    }

    public bool CheckDialougeAction (DialougeAction da) {
        return action.Equals (da);
    }

}
