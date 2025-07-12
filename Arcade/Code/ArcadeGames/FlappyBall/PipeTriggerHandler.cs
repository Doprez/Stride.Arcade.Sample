using Stride.Engine;
using Stride.Physics;
using System.Threading.Tasks;

namespace Arcade.Code.ArcadeGames.FlappyBall;
[ComponentCategory("ArcadeGames/FlappyBall")]
public class PipeTriggerHandler : AsyncScript
{

	public RigidbodyComponent Rigidbody { get; set; }

	public override async Task Execute()
	{
		Rigidbody ??= Entity.Get<RigidbodyComponent>();

		while (Game.IsRunning)
		{
			var firstCollision = await Rigidbody.NewCollision();

			var otherCollider = Rigidbody == firstCollision.ColliderA
				? firstCollision.ColliderB
				: firstCollision.ColliderA;

			if (otherCollider.Entity.Get<FlappyBallPlayer>() is FlappyBallPlayer player)
			{
				player.Entity.Scene = null;
			}

			await Script.NextFrame();
		}
	}
}
