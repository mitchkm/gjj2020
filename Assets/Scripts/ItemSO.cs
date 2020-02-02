using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class ItemSO : ScriptableObject
{

    public string itemName;

    public string description;

    public Sprite sprite;

    public override bool Equals(object obj) {
        if (obj == null || !(obj is ItemSO)) {
            return false;
        }

        ItemSO i = (ItemSO) obj;

        return itemName == i.itemName;

    }

}
