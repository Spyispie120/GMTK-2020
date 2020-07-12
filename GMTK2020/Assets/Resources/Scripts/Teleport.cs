﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{

    [SerializeField]
    private Vector2 teleportDistance;
    private bool canUse;
    protected override void Start()
    {
        base.Start();
        canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Activate()
    {
        if (!canUse) return;
        player.anim.SetTrigger("Teleport");
        Vector3 pos = player.transform.position;
        player.transform.position = new Vector3(pos.x + teleportDistance.x * (player.IsRight() ? 1 : -1),
                                                pos.y + teleportDistance.y, 
                                                pos.z);
        canUse = false;
    }

    public override void Deactivate()
    {
        return;
    }

    public override void ResetAbility()
    {
        canUse = true;
    }
}
