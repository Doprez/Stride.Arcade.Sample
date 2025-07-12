using Stride.Core.Serialization;
using Stride.Engine;
using Stride.Input;

namespace Arcade.Code.Arcade;
/// <summary>
/// A basic component to load any scene and display it on the arcade screen.
/// </summary>
public class TestCabinet : ArcadeCabinet
{

	public UrlReference<Scene> ChildSceneUrl { get; set; }
	public SpriteComponent ArcadeScreen { get; set; }

	private Scene _scene;

	public override void EnterState()
	{
		base.EnterState();
		ArcadeScreen.Enabled = true;

		_scene = Content.Load(ChildSceneUrl);

		Entity.Scene.Children.Add(_scene);
	}

	public override void ExecuteState()
	{
		if(Input.IsKeyPressed(Keys.Escape))
		{
			StateMachine.SetCurrentState(PlayerStateMachine.DefaultState);
		}
	}

	public override void ExitState()
	{
		base.ExitState();
		ArcadeScreen.Enabled = false;

		Entity.Scene.Children.Remove(_scene);
		_scene.Parent = null;
		Content.Unload(_scene);
	}
}
