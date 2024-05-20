using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private bool isDamage;
    private float damageCoolDown = 2.0f;
    private float damageTimer;

    private float damage = 1.0f;
    private Player player;

    private void Update()
    {
        damageTimer -= Time.deltaTime;

        if (damageTimer < 0 && isDamage && player != null)
        {
            player.ChangeHealth(-damage);
            player.fx.TrapDamgeFxFor(damageCoolDown);
            damageTimer = damageCoolDown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDamage = true;
        player = collision.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDamage = false;
        player = null; 
    }

    
}
