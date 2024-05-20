using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    protected string animBoolName;

    protected float stateTimer;

    protected Vector2 lookDir; 

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolname)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolname;
    }

    public virtual void Enter()
    {
        rb = player.rb;
        lookDir.Set(1, 0);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        LookDirectionPlayer();
    }

    private void LookDirectionPlayer()
    {
        if (!Mathf.Approximately(xInput, 0.0f) || !Mathf.Approximately(yInput, 0.0f))
        {
            lookDir.Set(xInput, yInput);
            lookDir.Normalize();
        }
    }

    public virtual void Exit()
    {
    }
}
