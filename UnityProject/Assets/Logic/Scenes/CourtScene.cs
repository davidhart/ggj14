using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourtScene : MonoBehaviour
{
	public float HorizontalCharacterSpacing;
	public float VerticalCharacterSpacing;
	public int JuryPerRow;
	public int RabblePerRow;

	public Transform AccusedNode;
	public Transform DefendantNode;
	public Transform ProsecutionNode;
	public Transform[] JuryNodes;
	public Transform[] RabbleNodes;
	
	public UIAnchor AnchorTop;
	public UIAnchor AnchorBottom;
	public UIAnchor AnchorRight;
	public UIAnchor AnchorLeft;
	
	public UITiledSprite Background;
	
	private Character Accused;
	private Character Defendant;
	private Character Prosecution;
	private List<Character> Jury = new List<Character>();
	private List<Character> Rabble = new List<Character>();

	// Use this for initialization
	void Start ()
	{
	
		var size = new Vector3( AnchorRight.transform.localPosition.x - AnchorLeft.transform.localPosition.x, 
								AnchorTop.transform.localPosition.y - AnchorBottom.transform.localPosition.y,
								1 );
		Background.transform.localScale = size;
		
		Accused = PopulateCharacter(AccusedNode);
		Defendant = PopulateCharacter(DefendantNode);
		Prosecution = PopulateCharacter(ProsecutionNode);
		
		foreach (Transform t in JuryNodes)
		{
			Jury.AddRange(PopulateCharactersMulti(t, JuryPerRow, false));
		}
		
		foreach (Transform t in RabbleNodes)
		{
			Rabble.AddRange(PopulateCharactersMulti(t, RabblePerRow, true));
		}
	}
	
	Character PopulateCharacter(Transform parent)
	{
		var c = CharacterFactory.CreateRandomCharacter();
		
		c.transform.parent = parent;
		c.transform.localPosition = Vector3.zero;
		c.transform.localScale = Vector3.one;
		
		return c;
	}
	
	List<Character> PopulateCharactersMulti(Transform parent, int count, bool horizontal)
	{
		var characters = new List<Character>();
	
		for (int i = 0; i < count; ++i)
		{
			var c = PopulateCharacter(parent);
			
			float offsetX = horizontal ? HorizontalCharacterSpacing * i - HorizontalCharacterSpacing * (count / 2) : 0;
			float offsetY = !horizontal ? VerticalCharacterSpacing * i - VerticalCharacterSpacing * (count / 2) : 0;
			
			c.transform.parent = parent;
			c.transform.localPosition = new Vector3(offsetX, offsetY, 0);
			
			characters.Add(c);
		}
		
		return characters;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
