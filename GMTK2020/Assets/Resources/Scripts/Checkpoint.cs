using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new Vector3(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().size.y / 2.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            GlobalValues.Instance.SetSpawnPoint(spawnPos);
            Debug.Log("checkpoint saved");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            GlobalValues.Instance.SetSpawnPoint(spawnPos);
            Debug.Log("checkpoint saved");
        }
    }
}
