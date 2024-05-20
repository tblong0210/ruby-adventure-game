using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float moveSpeed;

    [Header("Thow cog info")]
    public float thowCooldown;
    private float thowTimer;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip audioGetDamage;
    [SerializeField] private AudioClip audioFootSteps;
    [SerializeField] private AudioClip audioThowCog;

    private bool isPlayingAudio;

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);

        currentHealth = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        thowTimer -= Time.deltaTime;
        stateMachine.currentState.Update();
    }

    public void ChangeHealth(float _amount)
    {
        if(_amount < 0)
        {
            anim.SetTrigger("Hit");
            PlaySoundOne(audioGetDamage);
        }

        currentHealth = Mathf.Clamp(currentHealth + _amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log("Current health: " + currentHealth + "/" + maxHealth);
    }

    public void Launch(Vector2 lookDirection)
    {
        if (thowTimer < 0)
        {
            PlaySoundOne(audioThowCog);

            GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300, transform);
            anim.SetTrigger("Launch");

            thowTimer = thowCooldown;
        }
        else Debug.Log("Thow is cooldown!");
    }

    public void PlayAudioFoot()
    {
        if (!isPlayingAudio)
        {
            audioSource.clip = audioFootSteps;
            audioSource.Play();
            audioSource.loop = true;
            isPlayingAudio = true;
        }
    }

    public void StopAudioFoot()
    {
        audioSource.clip = null;
        audioSource.Stop();
        isPlayingAudio = false;
        audioSource.loop = false;
    }

    public bool IsAudioFootsStep()
    {
        return audioSource.clip == audioFootSteps;
    }
}
