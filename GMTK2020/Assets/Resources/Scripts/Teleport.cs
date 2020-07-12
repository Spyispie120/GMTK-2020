using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{

    [SerializeField]
    private Vector2 teleportDistance;
    private bool canUse;

    private SpriteRenderer sr;
    private Material blueMat;

    [SerializeField] private float COOLDOWN = 0.3f;
    [SerializeField] private float cdTimer;

    [SerializeField]
    private GameObject ghostTrail;

    protected override void Start()
    {
        base.Start();
        canUse = true;
        cdTimer = 0;
        sr = GetComponent<SpriteRenderer>();
        blueMat = Resources.Load("Materials/BlueFlash", typeof(Material)) as Material;
        //Debug.Log(blueMat);
        //sr.material = blueMat;
        //StartCoroutine("Flash");
    }

    private void Update()
    {
        if (cdTimer > 0)
        {
            cdTimer -= Time.deltaTime;
            if (cdTimer <= 0)
            {
                StartCoroutine(Flash());
            }
        }
    }

    IEnumerator Flash()
    {
        Material m = sr.material;
        sr.material = blueMat;
        yield return new WaitForSeconds(0.1f);
        sr.material = m;
    }

    public float GetCooldownRatio()
    {
        return Mathf.Max(0, cdTimer) / COOLDOWN;
    }

    public override void Activate()
    {
        if (cdTimer > 0) return;
        cdTimer = COOLDOWN;
        //if (!canUse) return;
        player.anim.SetTrigger("Teleport");
        CreateGhostTrail();
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

    public void CreateGhostTrail()
    {
        GameObject go = Instantiate(ghostTrail, this.transform.position, Quaternion.identity);
        go.transform.rotation = player.transform.rotation;
        go.transform.localScale = player.transform.localScale;
        Destroy(go, 1f);
    }
}
