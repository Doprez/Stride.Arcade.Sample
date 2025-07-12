using Arcade.AI.FiniteStateMachine;
using Arcade.Code.Utils;
using Stride.Engine;
using System;

namespace Arcade.Code.Player;
public class PlayerStateMachine : SyncScript
{

	/// <summary>
	/// Used to control the cameras anchor position.
	/// </summary>
	public SmoothFollowAndRotate CameraFollower { get; set; }

	/// <summary>
	/// This might not be needed but will start with the <see cref="PlayerFirstPersonController"/>
	/// </summary>
	public FSMState DefaultState { get; set; }

	private readonly FSM StateMachine = new();

	public override void Start()
	{
		Services.AddService(this);

		ArgumentNullException.ThrowIfNull(CameraFollower, nameof(CameraFollower));
		ArgumentNullException.ThrowIfNull(DefaultState, nameof(DefaultState));

		StateMachine.SetCurrentState(DefaultState);
	}

	public override void Update()
	{
		StateMachine.Execute();
	}
}
