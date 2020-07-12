using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddlerFlip : MonoBehaviour
{
    public Transform player;
    private Vector3 lookRight = new Vector3(-1, 1, 1);
    private Vector3 lookLeft = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > player.position.x)
        {
            transform.localScale = lookRight;
        } else if (transform.position.x < player.position.x)
        {
            transform.localScale = lookLeft;
        }
    }
}
