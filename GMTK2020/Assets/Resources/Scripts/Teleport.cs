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
    private float FAR_AWAY = 100f;

    [SerializeField]
    private GameObject ghostTrail;

    public LayerMask mask;
    public const int NUM_TELEPORT_FALLBACKS = 50;

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
        if (cdTimer > 0 || player.isTalking) return;
        cdTimer = COOLDOWN;
        //if (!canUse) return;
        player.anim.SetTrigger("Teleport");
        CreateGhostTrail();
        Vector3 pos = player.transform.position;
        Vector3 newPos = pos;
        Collider2D collider = null;
        for (int i = NUM_TELEPORT_FALLBACKS; i > 0; i--)
        {
            float scale = 1.0f * i / NUM_TELEPORT_FALLBACKS; // IE 10/10, 9/10, 8/10...

            newPos = new Vector3(pos.x + scale * teleportDistance.x * (player.IsRight() ? 1 : -1),
                                                pos.y + scale * teleportDistance.y,
                                                pos.z);
            collider = Physics2D.OverlapCircle(newPos, 0.1f, mask);
            if (collider == null) break; // This position is safe, just warp to it then
        }
        if (collider != null) // If we never found a safe collider
        {
            RaycastHit2D hit = Physics2D.Raycast(pos,
                                                 player.IsRight() ? Vector2.right : Vector2.left,
                                                 teleportDistance.magnitude,
                                                 mask);
            newPos = (hit.collider != null) ? (Vector3) hit.point : pos; // The second case should never happen lol
        }
        player.transform.position = newPos;
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

    public override string GetName()
    {
        return "teleport";
    }
}
