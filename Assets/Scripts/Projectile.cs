using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    Transform initPosition;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (initPosition != null
           && Vector2.Distance(initPosition.position, transform.position) > 8)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force, Transform _playerPosition)
    {
        rb.AddForce(direction * force);
        initPosition = _playerPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_MrClockwork enemy = collision.GetComponent<Enemy_MrClockwork>();

        if (enemy != null)
        {
            enemy.TakeDamage();
        }
    }
}
