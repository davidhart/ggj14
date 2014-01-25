using UnityEngine;
using System.Collections;

public class CharacterSpriteSet : MonoBehaviour 
{
	public UISprite Eyes;
	public UISprite Head;
	public UISprite Body;
	
	public void SetSkinColor(Color c)
	{
		Head.color = c;
	}
	
	public void SetBodyColor(Color c)
	{
		Body.color = c;
	}
}
