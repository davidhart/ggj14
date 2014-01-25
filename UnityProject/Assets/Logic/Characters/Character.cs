using UnityEngine;
using System.Collections;
using System.Linq;
using System;

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
	
	public Color[] SkinColors;
	
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

	public void ClearSpeech()
	{
		CharacterSpeech.ClearSpeech();
	}

	public void Speak(string message, System.Action onFinished )
	{
		CharacterSpeech.Speak( message, onFinished );
	}
	
	public void SetRandomSkinColor()
	{
		SetSkinColor(SkinColors[UnityEngine.Random.Range(0, SkinColors.Length)]);
	}
}
