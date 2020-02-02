using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DiaResPair", menuName = "DiaResPair", order = 5)]
public class DARPSO : ScriptableObject
{

    [SerializeField]
    private DAction action = null;

    [SerializeField]
    private DResponseSO response = null;

    [SerializeField]
    private CharacterList redirect = CharacterList.None;

    public string GetResponse () {
        return this.GetResponse(redirect.ToString ());
    }

    public string GetResponse(string replacement) {
        if (response == null) {
            return "No Dialogue";
        }
        return response.GetResponse(replacement);
    }

    public bool IsTriggered (DAction da) {
        return action != null && action.Equals (da);
    }

    public ItemSO RequiredItem {
        get {
            return action.item;
        }
    }

}
