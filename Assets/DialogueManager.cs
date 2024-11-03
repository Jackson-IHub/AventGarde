using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
public class DialogueManager : MonoBehaviour
{
    public bool dialogueFinished = false;
    public TextAsset textFileStart;
    public TextAsset textFileEnd;

    public TextMeshPro rexTextMesh;
    public TextMeshPro stacyTextMesh;

    public GameObject rexTextBubble;
    public GameObject stacyTextBubble;

    bool rexSpeaking;

    private Queue<string> dialogue = new Queue<string>();
    private List<int> whoIsSpeaking = new List<int>();

    private int lineNumber = 0;

    string constructedLine;

    public bool finishedRound = false;

    private bool didDialogueJustStart = false;

    private bool isTyping = false;


    public void Start()
    {
        ReadTextFile();
    }

    private void PrintDialogue()
    {
        rexSpeaking = false;
        Debug.Log(dialogue.Count);

        if (dialogue.Count == 0 || dialogue.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        {
            Debug.Log("end of dialogue");
            EndDialogue();
            return;
        }

        rexTextMesh.text = "";
        stacyTextMesh.text = "";

        StartCoroutine(PrintOutText());

        if(lineNumber == 0)
        {
            stacyTextBubble.SetActive(false);
            
        }
        else
        {
            rexTextBubble.SetActive(false);
        }
    }

    private IEnumerator PrintOutText()
    {
        int numberOfCharacters = dialogue.Peek().Length;
        isTyping = true;
        
        for (int i = 0; i < numberOfCharacters; i++)
        {
            yield return new WaitForSeconds(0.075f);

            if (whoIsSpeaking[lineNumber] == 1)
            {
                rexTextMesh.text += dialogue.Peek()[i];
                rexTextBubble.SetActive(true);
                stacyTextBubble.SetActive(false);
            }
            else
            {
                stacyTextMesh.text += dialogue.Peek()[i];
                stacyTextBubble.SetActive(true);
                rexTextBubble.SetActive(false);
            }
        }
        lineNumber++;
        dialogue.Dequeue();
        isTyping = false;
        
    }


    private void EndDialogue()
    {
        rexTextMesh.text = "";
        stacyTextMesh.text = "";
        rexTextBubble.SetActive(false);
        stacyTextBubble.SetActive(false);
        dialogue.Clear();
        dialogueFinished = true;
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && dialogueFinished == false && isTyping == false)
        {
            AdvanceDialogue();
        }

    }

    private void ReadTextFile() // skip // 
    {
        didDialogueJustStart = true;
        dialogueFinished = false;
        lineNumber = 0;

        string txt;

        if (finishedRound == false)
        {
            txt = textFileStart.text;
        }
        else 
        { 
            txt = textFileEnd.text; 
        }

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
                else if(line.StartsWith("Stacy: "))
                {
                    string curr = line.Substring(line.IndexOf(':') + 1); // curr = Hello, ...
                    dialogue.Enqueue(curr);
                    whoIsSpeaking.Add(0);
                }
                else
                {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                    whoIsSpeaking.Add(whoIsSpeaking[whoIsSpeaking.Count - 1]);
                }
            }
        }

        AdvanceDialogue();

    }
    
    


}
