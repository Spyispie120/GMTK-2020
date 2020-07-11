using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explode : Ability
{
    ISet<Flammable> flammables;
    ISet<GameObject> elements;

    [SerializeField] private float EXPLOSIVE_FORCE = 10f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        flammables = new HashSet<Flammable>();
        elements = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        foreach (Flammable f in flammables.ToArray())
        {
            f.Ignite();
        }

        foreach (GameObject go in elements)
        {
            Rigidbody2D gorb = go.GetComponent<Rigidbody2D>();
            if (gorb == null) continue;
            Vector2 dir = -(transform.position - go.transform.position);
            if (dir.sqrMagnitude > 0.0)
            {
                gorb.AddForce(dir.normalized * EXPLOSIVE_FORCE / dir.sqrMagnitude);
            }
        }
    }

    public override void Deactivate()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flammable f = collision.GetComponent<Flammable>();

        if (f != null)
        {
            flammables.Add(f);
        } else
        {
            elements.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Flammable f = collision.GetComponent<Flammable>();

        if (f != null)
        {
            flammables.Remove(f);
        }
        else
        {
            elements.Remove(collision.gameObject);
        }
    }
}
