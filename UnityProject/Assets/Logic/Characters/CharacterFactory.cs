using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFactory
{
	public const string PathToCharacterPrefab = "Prefabs/Characters/Character";

	public static Character CreateCharacter( Transform rootObject )
	{
		var characterPrefab = Resources.Load( PathToCharacterPrefab ) as GameObject;
		
		var characterGo = GameObject.Instantiate(characterPrefab) as GameObject;

		characterGo.transform.parent = rootObject;
		characterGo.transform.localPosition = Vector3.zero;
		characterGo.transform.localRotation = Quaternion.identity;
		
		var characterScript = characterGo.GetComponent< Character >();

		return characterScript;
	}
	
	public static Character CreateRandomCharacter( Transform rootObject )
	{
		var c = CreateCharacter( rootObject );
		
		c.SetBodyColor(ColorUtil.RandomColor());
		c.SetSkinColor(ColorUtil.RandomColor());
		
		return c;
	}
}
