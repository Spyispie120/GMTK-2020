using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{

    [SerializeField]
    private Vector2 teleportDistance;

    void Start()
    {
        player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Activate()
    {
        Vector3 pos = player.transform.position;
        player.transform.position = new Vector3(pos.x + teleportDistance.x * (player.IsRight() ? 1 : -1),
                                                pos.y + teleportDistance.y, 
                                                pos.z);
    }

    public override void Deactivate()
    {
        return;
    }

}
