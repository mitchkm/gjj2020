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
    private DResponse response = null;

    [SerializeField]
    private CharacterList redirect = CharacterList.None;

    public string GetResponse () {
        if (response == null) {
            return "No Dialogue";
        }
        return response.GetResponse(redirect.ToString ());
    }

    public bool IsTriggered (DAction da) {
        return action != null && action.Equals (da);
    }

}
