using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private AudioClip audioS;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player != null)
        {
            player.ChangeHealth(1);
            Destroy(gameObject);

            player.PlaySoundOne(audioS);
        }
    }
}
