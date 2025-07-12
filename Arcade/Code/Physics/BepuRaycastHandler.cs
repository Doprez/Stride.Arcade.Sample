using Stride.BepuPhysics;
using Stride.CommunityToolkit.Engine;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Collections.Generic;

namespace Arcade.Code.Physics;
[DataContract(nameof(BepuRaycastHandler))]
[ComponentCategory("Bepu")]
public class BepuRaycastHandler : StartupScript
{
	public CameraComponent Camera { get; set; }

	public float RayLength { get; set; } = 1000;
	public CollisionMask CollisionMask { get; set; } = CollisionMask.Everything;
	public int BepuSimulation { get; set; } = 0;

	private BepuConfiguration _bepuConfig;

	private readonly Vector3 _dir = -Vector3.UnitZ;

	public override void Start()
	{
		Camera ??= SceneSystem.SceneInstance.RootScene.GetCamera();
		_bepuConfig = Services.GetService<BepuConfiguration>();
	}

	/// <summary>
	/// outputs a ray cast hit. Using the center of the screen.
	/// </summary>
	/// <param name="hit"></param>
	public void ScreenRayCast(out HitInfo hit)
	{
		Entity.Transform.GetWorldTransformation(out var _, out var rotation, out var _);
		var worldDir = -Vector3.UnitZ;
		rotation.Rotate(ref worldDir);

		// Perform raycast
		_bepuConfig.BepuSimulations[BepuSimulation].RayCast(Camera.Entity.WorldPosition(), worldDir, RayLength, out hit, CollisionMask);
	}

	/// <summary>
	/// outputs a ray cast hit. Using the center of the screen.
	/// </summary>
	/// <param name="hit"></param>
	public bool ScreenRayCastPenetrating(out List<HitInfo> hits)
	{
		hits = [];

		Entity.Transform.GetWorldTransformation(out var _, out var rotation, out var _);
		var worldDir = -Vector3.UnitZ;
		rotation.Rotate(ref worldDir);

		Span<HitInfoStack> buffer = stackalloc HitInfoStack[16];
		int j = 0;
		foreach (var hitInfo in _bepuConfig.BepuSimulations[BepuSimulation].RayCastPenetrating(Camera.Entity.WorldPosition(), worldDir, RayLength, buffer, CollisionMask))
		{
			hits.Add(hitInfo);
		}
		if (j == 0)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// outputs a ray cast hit. Using the mouse position.
	/// </summary>
	/// <param name="hit"></param>
	public void MouseRayCast(out HitInfo hit)
	{
		var ray = Camera.GetPickRay(Input.MousePosition);

		// Perform raycast
		_bepuConfig.BepuSimulations[BepuSimulation].RayCast(Camera.Entity.WorldPosition(), ray.Direction, RayLength, out hit, CollisionMask);
	}

	/// <summary>
	/// outputs a ray cast hit. Using the mouse position.
	/// </summary>
	/// <param name="hit"></param>
	public void MouseRayCast(CollisionMask collisionMask, out HitInfo hit)
	{
		var ray = Camera.GetPickRay(Input.MousePosition);

		// Perform raycast
		_bepuConfig.BepuSimulations[BepuSimulation].RayCast(Camera.Entity.WorldPosition(), ray.Direction, RayLength, out hit, collisionMask);
	}

	/// <summary>
	/// outputs a ray cast hit. Using the mouse position.
	/// </summary>
	/// <param name="hit"></param>
	public bool MouseRayCastPenetrating(out List<HitInfo> hits)
	{
		var ray = Camera.GetPickRay(Input.MousePosition);
		hits = [];

		Span<HitInfoStack> buffer = stackalloc HitInfoStack[16];
		int j = 0;
		foreach (var hitInfo in _bepuConfig.BepuSimulations[BepuSimulation].RayCastPenetrating(Camera.Entity.WorldPosition(), ray.Direction, RayLength, buffer, CollisionMask))
		{
			hits.Add(hitInfo);
		}
		if (j == 0)
		{
			return false;
		}
		return true;
	}
}
