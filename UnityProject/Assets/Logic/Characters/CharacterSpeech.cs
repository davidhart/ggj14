using UnityEngine;
using System.Collections;
using System;

public class CharacterSpeech : MonoBehaviour
{
	const float timePerCharacter = 0.06f;
	float timerSinceLastCharacter = 0.0f;

	string toDisplay;
	int numCharactersDisplayed;

	public UILabel label;

	System.Action OnFinished;

	void Awake()
	{
	}

	public void ClearSpeech()
	{
		toDisplay = string.Empty;
	}

	bool IsFinished()
	{
		if( timerSinceLastCharacter < -0.5f )
			return true;

		return false;
	}

	void Update()
	{
		if( string.IsNullOrEmpty( toDisplay ) )
			return;

		timerSinceLastCharacter -= Time.deltaTime;

		if( toDisplay.Length == numCharactersDisplayed )
		{
			if( OnFinished == null )
				return;

			if( IsFinished() )
			{
				OnFinished();

				OnFinished = null;
			}

			return;
		}

		if( timerSinceLastCharacter > 0.0f )
			return;

		numCharactersDisplayed++;

		label.text = toDisplay.Substring( 0, numCharactersDisplayed );

		timerSinceLastCharacter = timePerCharacter;
	}

	public void Speak( string message, System.Action onFinished )
	{
		numCharactersDisplayed = 0;
		toDisplay = message;

		OnFinished = onFinished;
	}
}
