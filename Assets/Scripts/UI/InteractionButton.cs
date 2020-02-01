using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionButton : MonoBehaviour
{

    public Action clickAction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction () {
        clickAction ();
    }

    public void SetAction (Action action) {
        clickAction = action;
    }

}
