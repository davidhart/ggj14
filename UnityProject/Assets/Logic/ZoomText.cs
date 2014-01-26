using UnityEngine;
using System.Collections;
using System;

public class ZoomText : MonoBehaviour
{
	public Action OnAnimationCompleted;
	public UILabel Text;
	
	public void SetText(string text)
	{
		Text.text = text;
	}
	
	public void BeginAnimation()
	{
		animation.Play();
	}
	
	private void AnimationFinished()
	{
		if (OnAnimationCompleted != null)
			OnAnimationCompleted();
	}
	
	private const string ZoomTextPrefab = "Prefabs/ZoomText";
	
	public static ZoomText CreateZoomText(string text, Transform parent)
	{
		GameObject prefab = Resources.Load(ZoomTextPrefab) as GameObject;
		
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		
		go.transform.parent = parent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		
		ZoomText zoomText = go.GetComponent<ZoomText>();
		zoomText.SetText(text);
		
		return zoomText;
	}
}
