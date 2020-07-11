using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{
    private Player player;

    [SerializeField]
    private Vector2 teleportDistance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        Vector3 pos = player.transform.position;
        player.transform.position = new Vector3(pos.x + teleportDistance.x, pos.y + teleportDistance.y, pos.z);
    }

}
