using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected Player player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void ResetAbility();
}
