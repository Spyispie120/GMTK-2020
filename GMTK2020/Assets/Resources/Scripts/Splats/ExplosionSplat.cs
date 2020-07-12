using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSplat : Splat
{
    // Start is called before the first frame update
    protected override void Start()
    {
        PATH += "Explosions";
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
