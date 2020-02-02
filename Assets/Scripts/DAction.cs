using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class DAction 
{
    public enum Action {
        Give,
        Ask,
        Greet
    }

    [SerializeField]
    public Action verb;

    [SerializeField]
    public ItemSO item;

    [SerializeField]
    public CharacterList character;

    public override bool Equals (object obj) {
        if (obj == null || !(obj is DAction)) {
            return false;
        }
        
        DAction da = (DAction) obj;
        Debug.Log (da);
        return verb == da.verb && ((item == null && da.item == null) || item.Equals (da.item)) && character == da.character;

    }

    public DAction (Action verb = Action.Greet, ItemSO item = null, CharacterList character = CharacterList.None) {
        this.verb = verb;
        this.item = item;
        this.character = character;
    }

}
