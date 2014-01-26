using System;
using UnityEngine;

public class MainMenuGameState : BaseGameState
{
	float hammerTimer = -1.0f;
	
	GameObject characterGO;
	Character character;

	public MainMenuGameState()
	{
	}

	public override void OnEnter()
	{
		characterGO = new GameObject( "Characters" );

		characterGO.transform.parent = GameObject.Find( "Panel" ).transform;
		characterGO.transform.localScale = Vector3.one;

		character = CharacterFactory.CreateRandomCharacter( characterGO.transform );
		character.Speak( "Smash Hammer to Interrupt People", OnSpeakFinish );

		MicrophoneInput.OnHammer += OnHammer;

		hammerTimer = -1.0f;
	}

	void OnSpeakFinish()
	{
		character.Speak( "Interrupt Me to Continue", OnSpeakFinish );
	}

	public override void OnExit()
	{
		MicrophoneInput.OnHammer -= OnHammer;

		GameObject.Destroy( characterGO );
	}

	void OnHammer()
	{
		var deniedGO = GameObject.Instantiate( Resources.Load( "Prefabs/Denied" ) ) as GameObject;
		deniedGO.transform.parent = GameObject.Find ( "Panel" ).transform;

		//var GO = GameObject.Instantiate( Resources.Load( "Prefabs/Order" ) ) as GameObject;
		//GO.transform.parent = GameObject.Find ( "Panel" ).transform;

		hammerTimer = 1.0f;
	}

	public override bool Update (float timeInState)
	{
		if( hammerTimer > 0.0f )
		{
			hammerTimer -= Time.deltaTime;

			if( hammerTimer <= 0.0f )
				return true;
		}

		return false;
	}
}
