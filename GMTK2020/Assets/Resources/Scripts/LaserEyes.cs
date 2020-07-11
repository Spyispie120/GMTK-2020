using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LaserEyes : Ability
{
    private LineRenderer laserSprite;
    private bool active;

    private float FAR_AWAY = 100f;
    public LayerMask mask;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        active = false;
        laserSprite = GetComponent<LineRenderer>();
        player = GetComponentInParent<Player>();
        laserSprite.startWidth = 0.1f;
        laserSprite.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.IsRight() ? Vector2.right : Vector2.left, FAR_AWAY, mask);
            laserSprite.SetPosition(0, transform.position);
            Vector2 endposition = (hit.collider != null) ? new Vector3(hit.point.x, hit.point.y, 0) : (transform.position + (player.IsRight() ? Vector3.right : Vector3.left) * FAR_AWAY);
            //Debug.Log(endposition);
            //Debug.DrawLine(transform.position, endposition, Color.red);
            laserSprite.SetPosition(1, endposition);
            laserSprite.startColor = Color.red;
            laserSprite.endColor = Color.red;
            
            laserSprite.enabled = true;
        }
        else
        {
            laserSprite.enabled = false;
        }
    }

    public override void Activate()
    {
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
    }
}
