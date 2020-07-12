using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivation : MonoBehaviour
{
    private Player player;
    private ISet<Ability> press;
    private ISet<Ability> hold;
    private ISet<Ability> release;

    public Dictionary<string, bool> abilites;

    // Start is called before the first frame update
    void Awake()
    {
        player = this.GetComponent<Player>();
        press = new HashSet<Ability>();
        hold = new HashSet<Ability>();
        release = new HashSet<Ability>();

        abilites = new Dictionary<string, bool>();

        press.Add(GetComponentInChildren<Explode>());
        hold.Add(GetComponentInChildren<Magnetism>());
        hold.Add(GetComponentInChildren<LaserEyes>());
        release.Add(GetComponent<Teleport>());

        abilites["explode"] = false;
        abilites["magnetism"] = false;
        abilites["lasereye"] = false;
        abilites["teleport"] = false;
        abilites["jump"] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Ability a in press)
            {
                if(abilites[a.GetName()]) a.Activate();
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                if (abilites[a.GetName()]) a.Activate();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                if (abilites[a.GetName()]) a.Deactivate();
            }

            foreach (Ability a in release)
            {
                if (abilites[a.GetName()]) a.Activate();
            }
        }
    }

    public bool IsEnable(string abilityName)
    {
        return abilites[abilityName];
    }
    public void EnableAbility(string abilityName)
    {
        if (abilites.ContainsKey(abilityName)) abilites[abilityName] = true;
    }
}
