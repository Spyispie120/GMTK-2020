using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    private Teleport player;
    private Image img;
    public float heightScale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Teleport>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float viewportHeight = Camera.main.pixelRect.height / heightScale;
        gameObject.transform.position = Camera.main.WorldToScreenPoint(player.transform.position) +
                                        new Vector3(0, viewportHeight, 0);
        img.fillAmount = (Mathf.Abs(player.GetCooldownRatio()) < 0.001f) ? 0 : 1.0f - player.GetCooldownRatio();
    }
}
