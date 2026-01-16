using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;

    public PlayerState(PlayerController player)
    {
        this.player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}

public class GroundState : PlayerState
{
    public GroundState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entered Ground State");
        player.movementSpeed = player.groundMovementSpeed;
    }

    public override void Update()
    {
        if (player.IsOnIce)
        {
            player.ChangeState(new IceNormalState(player));
        }

        player.transform.position = Vector2.MoveTowards(player.transform.position, player.targetPosition, Time.deltaTime * player.movementSpeed);
    }
}

public class IceNormalState : PlayerState
{
    public IceNormalState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entered Ice Normal State");
        player.movementSpeed = player.iceNormalMovementSpeed;
    }

    public override void Update()
    {
        if (!player.IsOnIce)
        {
            player.ChangeState(new GroundState(player));
        }

        player.transform.position += (Vector3)(player.bodyTransform.right * player.movementSpeed * Time.deltaTime);

    }
}
