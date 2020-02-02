using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 4)]
public class QuestSO : ScriptableObject
{

    [SerializeField]
    private ItemSO reward = null;

    private bool started = false;
    private bool completed = false;
    private int numAttempts = 0;

    [SerializeField]
    private DARPSO redeem; // Redeem quest
    [SerializeField]
    private DARPSO redeemUnexpected; // Unexpectedly redeem quest
    [SerializeField]
    private DARPSO[] give; // Give quest

    public bool IsCompleted() {
        return completed;
    }

    public string DoAction(DAction playerAction, PlayerController player) {
        numAttempts %= give.Length;
        if (completed) {
            return null;
        }
        if (started && redeem.IsTriggered(playerAction)) {
            this.GiveReward(player);
            return redeem.GetResponse(reward.ToString());
        } else if (redeemUnexpected.IsTriggered(playerAction)) {
            this.GiveReward(player);
            return redeemUnexpected.GetResponse(reward.ToString());
        } else if (give[numAttempts].IsTriggered(playerAction)) {
            if (!started) {
                started = true;
                // Teach player about this item / person / whatever
                player.AddToKnown (redeem.RequiredItem);
            }
            return give[numAttempts++].GetResponse(reward.ToString());
        } else {
            return null;
        }
    }

    private void GiveReward(PlayerController player) {
        completed = true;
        player.AddToInventory(reward);
    }
}
