using UnityEngine;
using System;
using System.Collections.Generic;

public class VerdictGameState : BaseGameState
{
	const string PathToTrialScenePrefab = "Prefabs/Scenes/VerdictScene";
	
	private GameObject sceneGo;
	private VerdictScene verdictScene;

	public VerdictGameState ()
	{
	}

	public override void OnEnter ()
	{
		Debug.Log( VerdictManager.Instance.Influence );
		
		SetupScene();
	}
	
	void SetupScene()
	{
		GameObject GO = GameObject.Find( "Panel" );
		
		GameObject scenePrefab = Resources.Load(PathToTrialScenePrefab) as GameObject;
		sceneGo = GameObject.Instantiate(scenePrefab) as GameObject;
		
		sceneGo.transform.parent = GO.transform;
		sceneGo.transform.localPosition = Vector3.zero;
		sceneGo.transform.localScale = Vector3.one;
		sceneGo.transform.localRotation = Quaternion.identity;
		
		verdictScene = sceneGo.GetComponent<VerdictScene>();
		
		// TODO: get the actual dudes from the last scene
		var temp = new List<Character>();
		
		for (int i = 0; i < 10; ++i)
		{
			temp.Add(CharacterFactory.CreateRandomCharacter(sceneGo.transform));
		}
		
		verdictScene.ReParentJury(temp);
		
		int numberOfPeopleVotingGuilty = 
			Mathf.Clamp((int)(VerdictManager.Instance.Influence + temp.Count / 2.0f), 0, temp.Count);
		
		verdictScene.SetupAnimationQueue(numberOfPeopleVotingGuilty);
		
		verdictScene.BeginVerdictAnimation();
		
	}

	public override bool Update (float timeInState)
	{
		return verdictScene.IsAnimating();
	}

	public override void OnExit ()
	{
		GameObject.Destroy(sceneGo);
		sceneGo = null;
	}
}

