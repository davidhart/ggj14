using UnityEngine;
using System.Collections;
using System;

public class CharacterSpeech : MonoBehaviour
{
	const float timePerCharacter = 0.05f;
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
		label.text = string.Empty;
	}

	bool IsFinished()
	{
		if( timerSinceLastCharacter < -2.5f )
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
				System.Action del = OnFinished;
				OnFinished = null;
				del();
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

		Debug.Log (message );

		OnFinished = onFinished;

		int numberOfLines = label.font.WrapText( message, label.lineWidth / label.transform.localScale.x, 10 ).Split( '\n' ).Length;
		label.transform.localPosition = new Vector3( 0.0f, 35.0f + ( 25.0f * numberOfLines ), -10.0f );
	}
}
