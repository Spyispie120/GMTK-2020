using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : Ability
{
    [SerializeField] private float MAGNETIC_FORCE = 10f;
    [SerializeField] private float MAX_SPEED = 0f;
    private bool active;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            Magnetic m = collision.gameObject.GetComponent<Magnetic>();
            if (m != null)
            {
                Vector2 dir = (transform.position - m.transform.position);
                if (dir.sqrMagnitude > 0.0)
                {
                    if (m.GetSpeed() < MAX_SPEED)
                    {
                        m.AddForce(dir.normalized * MAGNETIC_FORCE / dir.sqrMagnitude);
                    } else
                    {
                        m.Stop();
                    }
                }
            }
        }
    }
}
