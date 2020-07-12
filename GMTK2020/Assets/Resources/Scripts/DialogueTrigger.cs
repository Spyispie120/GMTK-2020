using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] dialogue;
    private int dialoguePointer;
    public RectTransform dialougeRect;
    public TextMeshProUGUI text;
    private bool canStart, inDialogue;
    private Player player;


    private bool oldJump, oldTeleport;
    private bool entered;
    private Collider2D dialogueTriggerBox;
    private Transform parent;
    private Camera mainCam;
    public float heightScale;

    private void Start()
    {
        dialogueTriggerBox = GetComponent<Collider2D>();
        parent = this.transform.parent;
        mainCam = Camera.main;


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
            {   // In this case clean up our dialogue
                player.isTalking = false;
                if (oldJump) ablities.EnableAbility("jump");
                if (oldTeleport) ablities.EnableAbility("teleport");
                inDialogue = false;
                dialoguePointer = 0;
                text.SetText(""); // Clear out the dialogue lol
                return;
            }
            player.isTalking = true;
            oldTeleport = ablities.IsEnable("teleport");
            oldJump = ablities.IsEnable("jump");
            ablities.DisableAbility("teleport");
            ablities.DisableAbility("jump");
            text.text = dialogue[dialoguePointer];
            dialoguePointer++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            entered = true;
            float viewportHeight = mainCam.pixelRect.height / heightScale;
            Debug.Log(viewportHeight);
            dialougeRect.position = mainCam.WorldToScreenPoint(parent.position) + new Vector3(0, viewportHeight, 0);
            Player found = collision.gameObject.GetComponent<Player>();
            if (found != null)
            {
                player = found;
                canStart = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            entered = false;
            dialougeRect.position = new Vector3(1000, 1000, 1000);
        }
        canStart = false;
    }
}
