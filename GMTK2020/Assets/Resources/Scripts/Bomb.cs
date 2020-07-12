using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = Resources.Load("Particles/Explosion2") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(PlayerDeath());
        }
    }

    public void Die()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator PlayerDeath()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        GlobalValues.Instance.GetPlayer().Die();
    }
}
