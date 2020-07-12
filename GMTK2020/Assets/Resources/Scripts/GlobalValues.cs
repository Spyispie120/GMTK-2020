using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalValues : MonoBehaviour
{
    public static GlobalValues Instance;

    private IDictionary<string, bool> currentAbilities;
    private Vector3 playerSpawn;
    private Player player;
    public bool nextLevel;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            currentAbilities = new Dictionary<string, bool>();
            currentAbilities.Add("jump", false);
            currentAbilities.Add("teleport", false);
            currentAbilities.Add("lasereye", false);
            currentAbilities.Add("magnetism", false);
            currentAbilities.Add("explode", false);

            playerSpawn = Vector3.zero;
        }
        else
        {
            if(SceneManager.GetActiveScene().name == "Menu")
            {
                IDictionary<string, bool> abi = Instance.currentAbilities;
                foreach (string entry in Instance.currentAbilities.Keys)
                {
                    abi[entry] = false;
                }
            }
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player = FindObjectOfType<Player>();
        player.transform.position = playerSpawn;
        //foreach (string a in currentAbilities.Keys)
        //{
        //    player.abilityActivation.abilities[a] = currentAbilities[a];
        //}
    }

    public IDictionary<string, bool> GetAbilities()
    {
        return currentAbilities;
    }

    public Vector3 GetSpawnPoint()
    {
        return playerSpawn;
    }

    public Vector3 SetSpawnPoint(Vector3 p)
    {
        return playerSpawn = p;
    }

    public Player GetPlayer()
    {
        return player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            if (player == null || nextLevel) return;
            if (nextLevel)
            {
                nextLevel = false;
                playerSpawn = player.transform.position;
                return;
            }
            player.transform.position = new Vector3(playerSpawn.x, playerSpawn.y, player.transform.position.z);
        }
    }
}
