using UnityEngine;
using System.Collections;
using System;

public class ThoughtBubble : MonoBehaviour
{	
	public void BeginAnimation()
	{
		animation.Play();
	}
	
	private void AnimationFinished()
	{
		GameObject.Destroy(gameObject);
	}
	
	private const string PrefabHappy = "Prefabs/Characters/HappyThought";
	private const string PrefabSad = "Prefabs/Characters/SadThought";
	
	public static ThoughtBubble CreateHappyThought(Transform parent)
	{
		return CreateThought(PrefabHappy, parent);
	}
	
	public static ThoughtBubble CreateSadThought(Transform parent)
	{
		return CreateThought(PrefabSad, parent);
	}
	
	public static ThoughtBubble CreateThought(string prefabPath, Transform parent)
	{
		GameObject prefab = Resources.Load(prefabPath) as GameObject;
		
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		
		go.transform.parent = parent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		
		ThoughtBubble bubble = go.GetComponent<ThoughtBubble>();
		
		return bubble;
	}
}
