using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class DialougeAction 
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
        if (obj == null || !(obj is DialougeAction)) {
            return false;
        }
        
        DialougeAction ap = (DialougeAction) obj;

        return verb == ap.verb && item.Equals (ap.item) && character == ap.character;

    }

}
