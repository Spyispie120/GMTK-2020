using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyes : Ability
{
    private LineRenderer laserSprite;
    private bool active;

    private float FAR_AWAY = 100f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        active = false;
        laserSprite = GetComponent<LineRenderer>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.IsRight() ? Vector2.right : Vector2.left);
            laserSprite.SetPosition(0, transform.position);
            laserSprite.SetPosition(1, (hit.collider != null) ? hit.collider.transform.position : (transform.position + (player.IsRight() ? Vector3.right : Vector3.left) * FAR_AWAY));
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
