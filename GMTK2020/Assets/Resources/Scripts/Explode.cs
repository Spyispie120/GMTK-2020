using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        foreach (Flammable f in flammables)
        {
            f.Ignite();
        }

        foreach (GameObject go in elements)
        {
            Vector2 dir = -(transform.position - go.transform.position);
            if (dir.sqrMagnitude > 0.0)
            {
                go.GetComponent<Rigidbody2D>().AddForce(dir.normalized * EXPLOSIVE_FORCE / dir.sqrMagnitude);
            }
        }
    }

    public override void Deactivate()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;
        Flammable f = go.GetComponent<Flammable>();

        if (f != null)
        {
            flammables.Add(f);
        } else if (go != null)
        {
            elements.Add(go);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;
        Flammable f = go.GetComponent<Flammable>();

        if (f != null)
        {
            flammables.Remove(f);
        }
        else if (go != null)
        {
            elements.Remove(go);
        }
    }
}
