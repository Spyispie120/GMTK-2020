using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MonoBehaviour
{
    GameObject particles;
    GameObject mark;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        particles = Resources.Load("Particles/Explosion") as GameObject;
        mark = Resources.Load("Prefabs/Splats/BloodSplat") as GameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Vector3 splatPos = new Vector3(transform.position.x, transform.position.y - sr.sprite.bounds.size.y / 2, transform.position.z);
        Instantiate(particles, transform.position, Quaternion.identity);
        Instantiate(mark, splatPos, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
