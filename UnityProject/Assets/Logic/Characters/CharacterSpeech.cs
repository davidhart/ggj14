using UnityEngine;
using System.Collections;

public class CharacterSpeech : MonoBehaviour
{
	const float timePerCharacter = 0.1f;
	float timerSinceLastCharacter = 0.0f;

	string toDisplay;
	int numCharactersDisplayed;

	public UILabel label;

	void Awake()
	{
	}

	void Update()
	{
		if( string.IsNullOrEmpty( toDisplay ) )
			return;

		if( toDisplay.Length == numCharactersDisplayed )
			return;

		timerSinceLastCharacter -= Time.deltaTime;

		if( timerSinceLastCharacter > 0.0f )
			return;

		numCharactersDisplayed++;

		label.text = toDisplay.Substring( 0, numCharactersDisplayed );

		timerSinceLastCharacter = timePerCharacter;
	}

	public void Speak( string message )
	{
		numCharactersDisplayed = 0;
		toDisplay = message;
	}
}
