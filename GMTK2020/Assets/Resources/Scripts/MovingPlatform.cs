using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start, end;
    public float speed;

    private Rigidbody2D body;
    private bool movingToDest;
    private Vector3 startPos, endPos;

    private const float DELTA = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        startPos = start.position;
        endPos = end.position;

        gameObject.transform.position = startPos;
        movingToDest = true;
        body.velocity = calcVelocity();
    }

    private Vector3 calcVelocity() {
        return (movingToDest ? (endPos - startPos) : (startPos - endPos)).normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingToDest && Vector2.Distance(gameObject.transform.position, endPos) < DELTA)
        {
            movingToDest = false;
            body.velocity = calcVelocity();

            print("reached dest");
        }
        else if (!movingToDest && Vector2.Distance(gameObject.transform.position, startPos) < DELTA)
        {
            movingToDest = true;
            body.velocity = calcVelocity();
        }
    }
}
