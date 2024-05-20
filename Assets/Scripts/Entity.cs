using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public AudioSource audioSource { get; private set; }

    [Header("Stat info")]
    [SerializeField] protected  float maxHealth;
    protected float currentHealth;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update() { 

    }

    public void SetZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public void PlaySoundOne(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
