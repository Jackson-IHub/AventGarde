using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class DialogueManager : MonoBehaviour
{
    public TextAsset textFile;

    public TextMeshPro rexTextMesh;
    public TextMeshPro stacyTextMesh;

    bool rexSpeaking;

    private Queue<string> dialogue = new Queue<string>();
    private List<int> whoIsSpeaking = new List<int>();


    private void Start()
    {
        ReadTextFile();

    }

    private void PrintDialogue()
    {

        rexSpeaking = false;

        if (dialogue.Count == 0 || dialogue.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        {
            dialogue.Dequeue(); // Clear Queue
            EndDialogue();
        }

        if (dialogue.Peek().Contains("Rex: ") )
        {
            Debug.Log("test");
            rexSpeaking = true;
            PrintDialogue(); // print the rest of this line
        }
        if(dialogue.Peek().Contains("Stacy: ") )
        {
            rexSpeaking=false;
        }
        else
        {
            rexTextMesh.text = dialogue.Dequeue();
        }

        if(whoIsSpeaking)





    }
    private void EndDialogue()
    {
        rexTextMesh.text = "";
        dialogue.Clear();
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AdvanceDialogue();
        }

    }

    private void ReadTextFile() // skip // 
    {
        string txt = textFile.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        foreach (string line in lines) // for every line of dialogue
        {
            if (!string.IsNullOrEmpty(line))// ignore empty lines of dialogue
            {
                if (line.StartsWith("Rex: ")) // e.g [NAME=Michael] Hello, my name is Michael
                {
                    string curr = line.Substring(line.IndexOf(':') + 1); // curr = Hello, ...
                    dialogue.Enqueue(curr);
                    whoIsSpeaking.Add(1);
                }

                if(line.StartsWith("Stacy: "))
                {
                    string curr = line.Substring(line.IndexOf(':') + 1); // curr = Hello, ...
                    dialogue.Enqueue(curr);
                    whoIsSpeaking.Add(0);
                }
                else
                {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                    whoIsSpeaking.Add(whoIsSpeaking[whoIsSpeaking.Count]);
                }
            }
        }
        dialogue.Enqueue("EndQueue");
    }

}
