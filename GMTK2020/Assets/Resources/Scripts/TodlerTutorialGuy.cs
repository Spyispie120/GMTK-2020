using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodlerTutorialGuy : MonoBehaviour
{
    [SerializeField] private Abilities abilities;

    void Start()
    {
        //abilityList = new List<string>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.abilityActivation.EnableAbility(abilities.ToString());
        }
    }


}
