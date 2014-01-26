using System;

public class MainMenuGameState : BaseGameState
{
	bool hammerHit = false;

	public MainMenuGameState()
	{
	}

	public override void OnEnter()
	{
		MicrophoneInput.OnHammer += OnHammer;
	}

	public override void OnExit()
	{
		MicrophoneInput.OnHammer -= OnHammer;

		hammerHit = false;
	}

	void OnHammer()
	{
		hammerHit = true;
	}

	public override bool Update (float timeInState)
	{
		if( hammerHit )
		{
			return true;
		}

		return false;
	}
}
