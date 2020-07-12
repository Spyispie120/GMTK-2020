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
    private bool entered;
    private Collider2D dialogueTriggerBox;

    private void Start()
    {
        dialogueTriggerBox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entered)
        {
            text.text = dialogue[dialoguePointer];
            dialoguePointer++;
            if (dialoguePointer > dialogue.Length - 1)
            {
                dialoguePointer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            entered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            entered = false;
    }
}
