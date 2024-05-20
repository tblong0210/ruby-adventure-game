using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MrClockwork : Entity
{
    [Header("Move info")]
    [SerializeField] private bool isVerticalMoving;
    public float moveSpeed;
    private float moveTime = .2f;
    private float moveTimer;

    [Header("Damage info")]
    [SerializeField]private float damage;
    [SerializeField]private float damageCoolDown;

    [Header("Stun info")]
    [SerializeField] private float stunTime;
    [SerializeField] private float distanceMove;
    private float stundTimer;

    [Header("Audio info")]
    [SerializeField] private AudioClip[] audioGetDamage;

    private bool isDamage;
    private float damageTimer;

    Player player;
    [SerializeField] private GameObject smoker;

    private int direction;

    private Vector2 startPosition;

    protected override void Awake()
    {
        base.Awake();
        direction = 1;
        moveTimer = moveTime;
        smoker.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    protected override void Update()
    {
        base.Update();

        moveTimer -= Time.deltaTime;
        damageTimer -= Time.deltaTime;
        stundTimer -= Time.deltaTime;

        anim.SetBool("Stun", stundTimer > 0);

        if (stundTimer > 0)
        {
            SetZeroVelocity();
            smoker.SetActive(true);
        }
        else
        {
            smoker.SetActive(false);
            MoveController();
        }
        DamageToPlayerController();
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

    private void DamageToPlayerController()
    {
        if (damageTimer < 0 && isDamage && player != null)
        {
            player.ChangeHealth(-damage);
            damageTimer = damageCoolDown;
            player.fx.EnemyDamgeFxFor(damageCoolDown);
        }
    }

    private void MoveController()
    {
        if (CheckDistanceMove() && moveTimer < 0)
        {
            direction *= -1;
            moveTimer = moveTime;
        }

        if (!isVerticalMoving)
        {
            SetVelocity(moveSpeed * direction, 0);
            anim.SetFloat("MoveX", direction);
            anim.SetFloat("MoveY", 0);
        }
        else
        {
            SetVelocity(0, moveSpeed * direction);
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", direction);
        }
    }

    private bool CheckDistanceMove()
    {
        return startPosition != null && 
            Vector2.Distance(transform.position, startPosition) > distanceMove;
    }

    public void TakeDamage()
    {
        stundTimer = stunTime;
        PlaySoundOne(audioGetDamage[Random.Range(0, audioGetDamage.Length)]);
    }
}
