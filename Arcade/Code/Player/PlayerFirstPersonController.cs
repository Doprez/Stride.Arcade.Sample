using Arcade.AI.FiniteStateMachine;
using Arcade.Code.Arcade;
using Arcade.Code.Physics;
using Stride.BepuPhysics;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;

namespace Arcade.Code.Player;
[ComponentCategory("States")]
public class PlayerFirstPersonController : FSMState
{
	public Entity CameraPivot { get; set; }
	public float MinCameraAngle { get; set; } = -90;
	public float MaxCameraAngle { get; set; } = 90;

	public Entity CameraAnchorPosition { get; set; }
	public CharacterComponent CharacterComponent { get; set; }
	public BepuRaycastHandler RaycastHandler { get; set; }

	private Vector3 _cameraAngle;

	public override void EnterState()
	{
		Input.LockMousePosition(true);

		Services.GetService<PlayerStateMachine>().CameraFollower.EntityToFollow = CameraAnchorPosition;
	}

	public override void ExecuteState()
	{
		if (Input.Mouse.IsPositionLocked)
		{
			Interact();
			Move();
			Rotate();
			Jump();
		}

		if(Input.IsKeyPressed(Keys.Escape))
		{
			Input.UnlockMousePosition();
		}
	}

	private void Interact()
	{
		if (Input.IsKeyPressed(Keys.E))
		{
			RaycastHandler.ScreenRayCastPenetrating(out var hits);
			if (hits.Count == 0)
				return;

			var hit = hits[0];

			// Hits are NOT always ordered by distance so filtering will be required for consistancy.
			if (hit.Collidable.Entity == CharacterComponent.Entity)
			{
				if (hits.Count < 2)
					return;
				hit = hits[1];
			}

			var arcade = hit.Collidable.Entity.Get<ArcadeCabinet>();

			if (arcade is not null)
			{
				StateMachine.SetCurrentState(arcade);
			}
		}
	}

	public override void ExitState()
	{
		Input.UnlockMousePosition();
	}

	private void Move()
	{
		// Keyboard movement
		var moveDirection = Vector2.Zero;
		if (Input.IsKeyDown(Keys.W))
			moveDirection.Y += 1;
		if (Input.IsKeyDown(Keys.S))
			moveDirection.Y -= 1;
		if (Input.IsKeyDown(Keys.A))
			moveDirection.X -= 1;
		if (Input.IsKeyDown(Keys.D))
			moveDirection.X += 1;

		var velocity = new Vector3(moveDirection.X, 0, -moveDirection.Y);
		velocity.Normalize();

		velocity = Vector3.Transform(velocity, Entity.Transform.Rotation);

		if (Input.IsKeyDown(Keys.LeftShift))
			velocity *= 2f;

		CharacterComponent.Move(velocity);
	}

	private void Rotate()
	{
		var delta = Input.Mouse.Delta;

		_cameraAngle.X -= delta.Y;
		_cameraAngle.Y -= delta.X;
		_cameraAngle.X = MathUtil.Clamp(_cameraAngle.X, MinCameraAngle, MaxCameraAngle);

		CharacterComponent.Orientation = Quaternion.RotationY(_cameraAngle.Y);
		if (CameraPivot != null)
		{
			CameraPivot.Transform.Rotation = Quaternion.RotationX(_cameraAngle.X);
		}
	}

	private void Jump()
	{
		if (Input.IsKeyPressed(Keys.Space))
		{
			CharacterComponent.TryJump();
		}
	}

}
