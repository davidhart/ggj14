using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFactory
{
	List<Character> Characters = new List<Character>();

	public static CharacterFactory Instance;

	public void CreateCharacter( string pathToCharacterPrefab )
	{
		var characterGO = Resources.Load( pathToCharacterPrefab ) as GameObject;
		var characterScript = characterGO.GetComponent< Character >();

		Characters.Add( characterScript );
	}
}
