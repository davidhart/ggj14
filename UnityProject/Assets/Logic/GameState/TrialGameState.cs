using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialGameState : BaseGameState
{
	CaseData currentCase;
	int currentAnnouncement;

	List<Character> Jury;
	Character Defense;
	Character Prosecution;
	Character Accused;
	
	float announcementDelay = 1.0f;
	float verdictDelay = -1.0f;

	GameObject verdictGO;
	GameObject sceneGo;

	Dictionary<TrialAnnouncement.eSource, Character> characterLookup;
	
	const string PathToTrialScenePrefab = "Prefabs/Scenes/CourtScene";

	public override void OnEnter()
	{
		currentCase = DataManager.Instance.CurrentCase;
		Jury = new List<Character>();
		characterLookup =  new Dictionary<TrialAnnouncement.eSource, Character>();
		currentAnnouncement = -1;

		MicrophoneInput.OnHammer += OnHammer;

		announcementDelay = 1.0f;
		verdictDelay = -1.0f;

		SetupScene();

		NextAnnouncement();
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
		
		CourtScene scene = sceneGo.GetComponent<CourtScene>();
		
		Defense = scene.Defendant;
		Prosecution = scene.Prosecution;
		Accused = scene.Accused;

		Jury.AddRange(scene.Jury);

		characterLookup.Clear();
		characterLookup.Add( TrialAnnouncement.eSource.Accused, Accused );
		characterLookup.Add( TrialAnnouncement.eSource.Defense, Defense );
		characterLookup.Add( TrialAnnouncement.eSource.Prosecution, Prosecution );
	}

	public override void OnExit()
	{
		MicrophoneInput.OnHammer -= OnHammer;

		GameObject.Destroy( sceneGo );
		GameObject.Destroy( verdictGO );
	}

	public void OnSpeechComplete()
	{
		NextAnnouncement();
	}

	Character currentSpeaker;

	bool NextAnnouncement()
	{
		currentAnnouncement++;

		Debug.Log ( "Showing Ann " + currentAnnouncement );

		if( currentSpeaker )
			currentSpeaker.ClearSpeech();

		if( currentAnnouncement >= currentCase.announcements.Count )
			return true;

		var announcement = currentCase.announcements[currentAnnouncement];

		Character teller = characterLookup[announcement.Source];
		teller.Speak( announcement.Message, OnSpeechComplete );

		currentSpeaker = teller;

		return false;
	}
	
	void OnHammer()
	{
		var GO = GameObject.Instantiate( Resources.Load( "Prefabs/Order" ) ) as GameObject;
		GO.transform.parent = GameObject.Find ( "Panel" ).transform;

		GO = GameObject.Instantiate( Resources.Load( "Prefabs/Denied" ) ) as GameObject;
		GO.transform.parent = GameObject.Find ( "Panel" ).transform;

		if( currentSpeaker )
		{
			GO.transform.position = currentSpeaker.transform.position;
		}

		announcementDelay = 0.6f;

		if( currentSpeaker != null )
			currentSpeaker.ClearSpeech();
	}

	public override bool Update (float timeInState)
	{
		if( announcementDelay > 0.0f )
		{
			announcementDelay -= Time.deltaTime;

			if( announcementDelay <= 0.0f )
			{
				NextAnnouncement();
			}
		}

		if( currentAnnouncement >= currentCase.announcements.Count )
		{
			if( verdictGO == null )
			{
				verdictGO = GameObject.Instantiate( Resources.Load( "Prefabs/Verdict" ) ) as GameObject;
				verdictGO.transform.parent = GameObject.Find ( "Panel" ).transform;
			}

			verdictDelay -= Time.deltaTime;

			if( verdictDelay < 0.0f )
			{
				return true;
			}
		}

		return false;
	}
}

