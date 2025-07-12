using Stride.Engine;
using System;
using System.Linq;

namespace Arcade.Code.ArcadeGames.FlappyBall;
[ComponentCategory("ArcadeGames/FlappyBall")]
public class Spawner : SyncScript
{

	public Prefab PrefabToSpawn { get; set; }
	public float SpawnInterval { get; set; } = 2;
	public float SpawnVariation { get; set; } = 10;

	private float timeSinceLastSpawn = 0;

	public override void Update()
	{
		//Spawn a new pipe every SpawnInterval seconds
		timeSinceLastSpawn += (float)Game.UpdateTime.Elapsed.TotalSeconds;

		if (timeSinceLastSpawn >= SpawnInterval)
		{
			if (timeSinceLastSpawn >= SpawnInterval)
			{
				//Spawn a new pipe
				var pipe = PrefabToSpawn.Instantiate().First();
				pipe.Transform.Position = Entity.Transform.Position;

				//Randomize the pipe's Y position
				var randomY = (float)Random.Shared.NextDouble() * SpawnVariation - SpawnVariation / 2;
				pipe.Transform.Position.Y = randomY;

				Entity.Scene.Entities.Add(pipe);

				timeSinceLastSpawn = 0;
			}
		}
	}
}
