using UnityEngine;
using System.Collections;
using System.Linq;

public enum CharacterDirection
{
	Down,
	Up,
	Left,
	Right,
}

public class Character : MonoBehaviour
{
	public CharacterSpriteSet[] CharacterSpriteSets;
	public CharacterSpeech CharacterSpeech;
	
	public void SetBodyColor(Color c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.SetBodyColor(c));
	}
	
	public void SetSkinColor(Color c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.SetSkinColor(c));
	}
	
	public void SetDirection(CharacterDirection c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.gameObject.active = false);
		
		CharacterSpriteSets[(int)c].gameObject.active = true;
	}

	public void Speak(string message)
	{
		CharacterSpeech.Speak( message );
	}
}
