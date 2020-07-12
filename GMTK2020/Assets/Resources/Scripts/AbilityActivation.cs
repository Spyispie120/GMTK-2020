using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivation : MonoBehaviour
{
    private Player player;
    private ISet<Ability> press;
    private ISet<Ability> hold;
    private ISet<Ability> release;

    // Start is called before the first frame update
    void Awake()
    {
        player = this.GetComponent<Player>();
        press = new HashSet<Ability>();
        hold = new HashSet<Ability>();
        release = new HashSet<Ability>();

        press.Add(GetComponentInChildren<Explode>());

        hold.Add(GetComponentInChildren<Magnetism>());
        hold.Add(GetComponentInChildren<LaserEyes>());

        release.Add(GetComponent<Teleport>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Ability a in press)
            {
                a.Activate();
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                a.Activate();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                a.Deactivate();
            }

            foreach (Ability a in release)
            {
                a.Activate();
            }
        }
    }
}
