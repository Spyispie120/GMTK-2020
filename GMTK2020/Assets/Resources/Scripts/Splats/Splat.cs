using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    private SpriteRenderer sr;
    Sprite[] splats;
    protected string PATH = "Sprites/Splats/";
    //private const int NUM_SPLATS = 4;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        splats = Resources.LoadAll<Sprite>(PATH);
        sr.sprite = splats[Random.Range(0, splats.Length)];

        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        transform.eulerAngles = euler;

        float newScale = Random.Range(0.2f, 0.4f);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
