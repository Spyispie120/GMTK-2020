using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivation : MonoBehaviour
{
    private Player player;
    private ISet<Ability> press;
    private ISet<Ability> hold;
    private ISet<Ability> release;

    public Dictionary<string, bool> abilities;

    [SerializeField] private bool enableJump;
    [SerializeField] private bool enableTeleport;
    [SerializeField] private bool enableLaser;
    [SerializeField] private bool enableMagnet;
    [SerializeField] private bool enableExplode;

    // Start is called before the first frame update
    void Awake()
    {
        player = this.GetComponent<Player>();
        press = new HashSet<Ability>();
        hold = new HashSet<Ability>();
        release = new HashSet<Ability>();

        abilities = new Dictionary<string, bool>();

        press.Add(GetComponentInChildren<Explode>());
        hold.Add(GetComponentInChildren<Magnetism>());
        hold.Add(GetComponentInChildren<LaserEyes>());
        release.Add(GetComponent<Teleport>());

        
    }

    private void Start()
    {
        abilities["explode"] = false || enableExplode || GlobalValues.Instance.GetAbilities()["explode"];
        abilities["magnetism"] = false || enableMagnet || GlobalValues.Instance.GetAbilities()["magnetism"];
        abilities["lasereye"] = false || enableLaser || GlobalValues.Instance.GetAbilities()["lasereye"];
        abilities["teleport"] = false || enableTeleport || GlobalValues.Instance.GetAbilities()["teleport"];
        abilities["jump"] = false || enableJump || GlobalValues.Instance.GetAbilities()["jump"];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Ability a in press)
            {
                if(abilities[a.GetName()]) a.Activate();
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                if (abilities[a.GetName()]) a.Activate();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Ability a in hold)
            {
                if (abilities[a.GetName()]) a.Deactivate();
            }

            foreach (Ability a in release)
            {
                if (abilities[a.GetName()]) a.Activate();
            }
        }
    }

    public bool IsEnable(string abilityName)
    {
        return abilities[abilityName];
    }
    public void EnableAbility(string abilityName)
    {
        if (abilities.ContainsKey(abilityName)) abilities[abilityName] = true;
    }
    public void DisableAbility(string abilityName)
    {
        if (abilities.ContainsKey(abilityName)) abilities[abilityName] = false;
    }

    public void DisableAllAbilities()
    {
        foreach (KeyValuePair<string, bool> entry in abilities)
        {
            // do something with entry.Value or entry.Key
            abilities[entry.Key] = false;
        }
    }
}
