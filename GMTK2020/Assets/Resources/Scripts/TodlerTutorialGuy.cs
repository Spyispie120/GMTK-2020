using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodlerTutorialGuy : MonoBehaviour
{
    public List<string> abilityList;
    public List<GameObject> positions;

    private int index;

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
            if (index < abilityList.Count)
            {
                player.abilityActivation.EnableAbility(abilityList[index++]);
            }
            //if(index < positions.Count)
            //{
            //    this.transform.position = positions[index].transform.position;
            //}
        }
    }


}
