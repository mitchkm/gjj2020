using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]
    private string name;

    public string Name {
        get {
            return name;
        }
    }

    public ItemSO so;

}
