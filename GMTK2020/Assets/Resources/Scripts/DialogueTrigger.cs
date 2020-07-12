using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] dialogue;
    private int dialoguePointer;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            text.text = dialogue[dialoguePointer];
            dialoguePointer++;
            if (dialoguePointer > dialogue.Length - 1)
            {
                dialoguePointer = 0;
            }
        }
    }
}
