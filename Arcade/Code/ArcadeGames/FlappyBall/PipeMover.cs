using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace Arcade.Code.ArcadeGames.FlappyBall;
[ComponentCategory("ArcadeGames/FlappyBall")]
public class PipeMover : SyncScript
{
	public float Speed { get; set; } = -5;
	public float LifeTime { get; set; } = 5;

	public RigidbodyComponent Rigidbody { get; set; }

	public override void Start()
	{
		Rigidbody ??= Entity.Get<RigidbodyComponent>();
	}

	public override void Update()
	{
		// Move the pipe to the left
		Rigidbody.LinearVelocity = new Vector3(Speed, 0, 0);

		// Destroy the pipe after a certain amount of time
		LifeTime -= (float)Game.UpdateTime.Elapsed.TotalSeconds;
		if (LifeTime <= 0)
		{
			Entity.Scene = null;
		}
	}
}
