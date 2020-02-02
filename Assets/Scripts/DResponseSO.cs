using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DResponse", menuName = "DResponse", order = 6)]
public class DResponse : ScriptableObject
{
    [SerializeField]
    private List<string> responses = new List<string> ();

    public string GetResponse() {
        return this.GetResponse("<instert>");
    }

    public string GetResponse (string replacement) {
        if (responses.Count > 0) {
            return String.Format(responses [0], replacement);
        }
        return "No Dialogue";
    }
}
