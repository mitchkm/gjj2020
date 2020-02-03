using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : NPC {
    // Reference to game controller to end the game

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override string Interact(DAction playerAction) {
        string response = base.Interact(playerAction);
        if (base.AllQuestsCompleted()) {
            // Tell game controller to end the game
        }
        return response;
    }
}