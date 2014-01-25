using System;
using UnityEngine;

public class TrialGameState : BaseGameState
{
	Character dave;
	public override void OnEnter()
	{
		var GO = new GameObject( "CharacterRoot" );
		GO.transform.localPosition = Vector3.zero;
		GO.transform.localRotation = Quaternion.identity;
		GO.transform.localScale = Vector3.one;

		GO = GameObject.Find( "Panel" );

		var rand = new System.Random();

		for( int nIndex = 0; nIndex < 1; nIndex++ )
		{
			var character = CharacterFactory.CreateRandomCharacter( GO.transform );

			float x = ( Screen.width * (float)rand.NextDouble() ) - ( Screen.width / 2.0f );
			float y = ( Screen.height * (float)rand.NextDouble() ) - ( Screen.height / 2.0f );

			//character.gameObject.transform.localPosition = new UnityEngine.Vector3( x, y, 0.0f );
			character.gameObject.transform.localPosition = new UnityEngine.Vector3( 0.0f, 0.0f, 0.0f );

			dave = character;
		}

		MicrophoneInput.OnHammer += OnHammer;
	}

	public override void OnExit ()
	{
		MicrophoneInput.OnHammer -= OnHammer;
	}

	void OnHammer()
	{
		dave.Speak( "This is a test message." );
	}

	public override bool Update (float timeInState)
	{
		return false;
	}
}

