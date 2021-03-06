﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 dir)
    {
        rb.AddForce(dir);
    }

    public void AddForce(Vector2 dir, ForceMode2D fm)
    {
        rb.AddForce(dir, fm);
    }

    public void Ignite()
    {
        Destroy(gameObject);
    }
}
