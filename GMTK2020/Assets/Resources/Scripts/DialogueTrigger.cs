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
        {
            entered = true;
            float viewportHeight = mainCam.pixelRect.height / heightScale;
            Debug.Log(viewportHeight);
            dialougeRect.position = mainCam.WorldToScreenPoint(parent.position) + new Vector3(0, viewportHeight, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            entered = false;
            dialougeRect.position = new Vector3(1000, 1000, 1000);
        }
    }
}
