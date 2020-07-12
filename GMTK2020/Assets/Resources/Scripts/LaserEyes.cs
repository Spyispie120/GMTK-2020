using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LaserEyes : Ability
{
    private LineRenderer laserSprite;
    private ParticleSystem spark;
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
        laserSprite.startWidth = 0.2f;
        laserSprite.endWidth = 0.2f;
        spark = GetComponentInChildren<ParticleSystem>();
        spark.Stop();
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
            spark.transform.position = endposition;
            //laserSprite.startColor = Color.red;
            //laserSprite.endColor = Color.red;
            
            laserSprite.enabled = true;
            if (!spark.isPlaying) spark.Play();
        }
        else
        {
            laserSprite.enabled = false;
            if (spark.isPlaying) spark.Stop();
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

    public override void ResetAbility()
    {
        throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "lasereye";
    }

}
