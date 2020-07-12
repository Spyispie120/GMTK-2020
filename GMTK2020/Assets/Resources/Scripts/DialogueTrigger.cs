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
    private bool canStart, inDialogue;
    private Player player;

    private void Start()
    {
        text.SetText("");
        canStart = false;
        inDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (canStart || inDialogue))
        {
            inDialogue = true;
            AbilityActivation ablities = player.GetComponent<AbilityActivation>();
            if (dialoguePointer == dialogue.Length)
            {
                player.isTalking = false;
                ablities.EnableAbility("teleport");
                inDialogue = false;
                dialoguePointer = 0;
                text.SetText(""); // Clear out the dialogue lol
                return;
            }
            player.isTalking = true;
            ablities.DisableAbility("teleport");
            text.text = dialogue[dialoguePointer];
            dialoguePointer++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player found = collision.gameObject.GetComponent<Player>();
        if (found != null)
        {
            player = found;
            canStart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            canStart = false;
    }
}
