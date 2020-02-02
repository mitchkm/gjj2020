using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageDisplay : MonoBehaviour
{

    [SerializeField]
    private List<string> toDisplay = new List<string> ();

    [SerializeField]
    private TMP_Text textDisplay;

    [SerializeField]
    private float textSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayMessage () {

        textDisplay.text = "";

        foreach (char letter in toDisplay [0].ToCharArray ()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds (textSpeed);
        }
        Debug.Log ("test");
        toDisplay.RemoveAt (0);

    }

    public void NextMessage () {
        if (textDisplay.text.Length < toDisplay [0].Length) {
            for (int i = textDisplay.text.Length; i < toDisplay [0].Length; i++) {
                textDisplay.text += toDisplay [0][i];
            }
            toDisplay.RemoveAt (0);
        } else {
            textDisplay.text = "";
        }
    }

    public void AddMessage (string message) {
        toDisplay.Add (message);
    }

    public void ClearMessageList () {
        toDisplay.Clear ();
    }

}
