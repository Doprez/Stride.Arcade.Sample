using Stride.Core.Collections;
using Stride.Core;
using Stride.Engine;
using Stride.Physics;
using System.Collections.Specialized;
using Stride.Input;
using Stride.Core.Mathematics;

namespace Arcade.Code.ArcadeGames.FlappyBall;
[ComponentCategory("ArcadeGames/FlappyBall")]
public class FlappyBallPlayer : SyncScript
{
	[DataMemberIgnore]
	public int Score { get; set; } = 0;

	public RigidbodyComponent Rigidbody { get; set; }
	public StaticColliderComponent GoalCollider { get; set; }

	public float JumpForce { get; set; } = 5;

	public override void Start()
	{
		Rigidbody ??= Entity.Get<RigidbodyComponent>();

		GoalCollider.Collisions.CollectionChanged += GoalCollectionChanged;
	}

	private void GoalCollectionChanged(object sender, TrackingCollectionChangedEventArgs e)
	{
		if (e.Action == NotifyCollectionChangedAction.Add)
		{
			// shouldnt need to check since only the pipes move.
			//var collision = (Collision)e.Item;
			//do something
			Score++;
		}
	}

	public override void Update()
	{
		DebugText.Print($"Score: {Score}", new Int2(10, 10));

		if (Input.IsKeyPressed(Keys.Space))
		{
			Jump();
		}
	}

	public void Jump()
	{
		// reset velocity to zero before applying impulse to remove slow rise effect
		Rigidbody.LinearVelocity = Vector3.Zero;
		Rigidbody.ApplyImpulse(Vector3.UnitY * JumpForce);
	}
}
