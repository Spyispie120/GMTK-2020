using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : Ability
{
    [SerializeField] private float MAGNETIC_FORCE = 10f;
    [SerializeField] private float MAX_SPEED = 0f;
    [SerializeField] private ParticleSystem magneticParticle;

    private Rigidbody2D rb;
    private bool active;
        
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponentInParent<Rigidbody2D>();
        active = false;
        player = this.transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        if (player.isTalking) return;
        active = true;
        if(!magneticParticle.isPlaying) magneticParticle.Play();
    }

    public override void Deactivate()
    {
        active = false;
        magneticParticle.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            Magnetic m = collision.gameObject.GetComponent<Magnetic>();
            if (m != null)
            {
                m.MagnetizeGravity();
                Vector2 dir = (transform.position - m.transform.position);
                if (dir.sqrMagnitude > 0.0)
                {
                    if (m.GetSpeed() < MAX_SPEED)
                    {
                        m.AddForce(dir.normalized * rb.mass * m.GetMass() * MAGNETIC_FORCE / dir.sqrMagnitude);
                    }
                    else
                    {
                        m.Stop();
                    }
                }
            }
        }
        else
        {
            Magnetic m = collision.gameObject.GetComponent<Magnetic>();
            if (m != null)
            {
                m.RevertGravity();
            }
        }
    }

    public override void ResetAbility()
    {
        throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "magnetism";
    }
}
