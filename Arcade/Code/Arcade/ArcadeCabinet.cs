using Arcade.AI.FiniteStateMachine;
using Arcade.Code.Player;
using Stride.Engine;

namespace Arcade.Code.Arcade;
[ComponentCategory("Arcade")]
public abstract class ArcadeCabinet : FSMState
{
	public Entity CameraAnchorPosition { get; set; }

	protected PlayerStateMachine PlayerStateMachine => Services.GetService<PlayerStateMachine>();

	public override void EnterState()
	{
		PlayerStateMachine.CameraFollower.EntityToFollow = CameraAnchorPosition;
	}

	public override void ExecuteState()
	{

	}

	public override void ExitState()
	{
	}
}
