using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolname) : base(_player, _stateMachine, _animBoolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, lookDir, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    character.ShowDialog();
                }
            }
        }

        if ((xInput != 0 && yInput == 0) || (xInput == 0 && yInput != 0))
        {
            player.anim.SetFloat("Look X", xInput);
            player.anim.SetFloat("Look Y", yInput);
            player.anim.SetFloat("Speed", Mathf.Abs(xInput == 0 ? yInput : xInput));
        }

        if (xInput == 0 && yInput == 0)
        {
            player.anim.SetFloat("Speed", 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Launch(lookDir);
                return;
            }

            if (player.audioSource.isPlaying && player.IsAudioFootsStep())
                player.StopAudioFoot();
        }

        if (xInput != 0 || yInput != 0)
        {
            player.PlayAudioFoot();
        }

        player.SetVelocity(xInput * player.moveSpeed, yInput * player.moveSpeed);
    }
}
