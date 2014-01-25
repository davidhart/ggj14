using System;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager
{
	int currentGameState = -1;
	List<BaseGameState> gameStates;

	public BaseGameState CurrentGameState { get { return gameStates[ currentGameState ]; } }

	float timeInState = 0.0f;

	public GameStateManager()
	{
		gameStates = new List<BaseGameState>();

		gameStates.Add( new MainMenuGameState() );
		gameStates.Add( new TrialGameState() );
		gameStates.Add( new VerdictGameState() );

		NextGameState();
	}

	void NextGameState()
	{
		if( currentGameState >= 0 )
		{
			CurrentGameState.OnExit();
		}

		currentGameState++;

		timeInState = 0.0f;

		if( currentGameState == gameStates.Count )
			currentGameState = 0;

		CurrentGameState.OnEnter();

		UnityEngine.Debug.Log ( "[GameStateManager] Change State to: " + CurrentGameState );
	}

	public void Update()
	{
		bool stateFinished = CurrentGameState.Update( timeInState );

		timeInState += UnityEngine.Time.deltaTime;

		if( stateFinished )
			NextGameState();
	}
}

