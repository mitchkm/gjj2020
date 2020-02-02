using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialouge", menuName = "Dialouge", order = 3)]
public class DialougeSO : ScriptableObject
{
    public string message;

    public List<string> responseOptions;

    public List<DialougeSO> nextMessages;
}
