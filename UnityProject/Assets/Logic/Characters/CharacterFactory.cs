using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFactory
{
	public const string PathToCharacterPrefab = "Prefabs/Characters/Character";

	public static Character CreateCharacter()
	{
		var characterPrefab = Resources.Load( PathToCharacterPrefab ) as GameObject;
		
		var characterGo = GameObject.Instantiate(characterPrefab) as GameObject;
		
		var characterScript = characterGo.GetComponent< Character >();

		return characterScript;
	}
	
	public static Character CreateRandomCharacter()
	{
		var c = CreateCharacter();
		
		c.SetBodyColor(ColorUtil.RandomColor());
		c.SetSkinColor(ColorUtil.RandomColor());
		
		return c;
	}
}
