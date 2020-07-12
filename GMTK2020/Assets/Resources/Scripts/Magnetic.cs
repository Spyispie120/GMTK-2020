using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    private Rigidbody2D rb;
    public float originalGravity = 1f;
    public float gravityModifier = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    public void RevertGravity()
    {
        rb.gravityScale = originalGravity;
    }

    public void MagnetizeGravity()
    {
        rb.gravityScale = originalGravity * gravityModifier;
    }

    public void AddForce(Vector2 dir)
    {
        rb.AddForce(dir);
    }

    public void AddForce(Vector2 dir, ForceMode2D fm)
    {
        rb.AddForce(dir, fm);
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    public float GetMass()
    {
        return rb.mass;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}
