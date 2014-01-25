using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialGameState : BaseGameState
{
	CaseData CurrentCase;
	int currentAnnouncement;

	List<Character> Jury;
	Character Defense;
	Character Prosecution;
	Character Accused;

	Dictionary<TrialAnnouncement.eSource, Character> characterLookup;

	public override void OnEnter()
	{
		CurrentCase = DataManager.Instance.Cases[0];
		Jury = new List<Character>();
		characterLookup =  new Dictionary<TrialAnnouncement.eSource, Character>();
		currentAnnouncement = -1;

		MicrophoneInput.OnHammer += OnHammer;

		SetupScene();

		NextAnnouncement();
	}

	void SetupScene()
	{
		GameObject GO = GameObject.Find( "Panel" );
		
		//	Jury
		for( int nIndex = 0; nIndex < 12; nIndex++ )
		{
			var jurer = CharacterFactory.CreateRandomCharacter( GO.transform );
			Jury.Add( jurer );
			
			jurer.gameObject.transform.localPosition = new Vector3( ( nIndex - 6 ) * 40, 250, 0 );
		}
		
		//	Defense
		Defense = CharacterFactory.CreateRandomCharacter( GO.transform );
		Defense.gameObject.transform.localPosition = new Vector3( 0, 100, 0 );
		
		//	Prosecution
		Prosecution = CharacterFactory.CreateRandomCharacter( GO.transform );
		Prosecution.gameObject.transform.localPosition = new Vector3( 0, -100, 0 );
		
		//	Accused
		Accused = CharacterFactory.CreateRandomCharacter( GO.transform );
		Accused.gameObject.transform.localPosition = new Vector3( 0, -300, 0 );

		characterLookup.Add( TrialAnnouncement.eSource.Accused, Accused );
		characterLookup.Add( TrialAnnouncement.eSource.Defense, Defense );
		characterLookup.Add( TrialAnnouncement.eSource.Prosecution, Prosecution );
	}

	public override void OnExit()
	{
		MicrophoneInput.OnHammer -= OnHammer;
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

		if( currentAnnouncement >= CurrentCase.announcements.Count )
			return true;

		var announcement = CurrentCase.announcements[currentAnnouncement];

		Character teller = characterLookup[announcement.Source];
		teller.Speak( announcement.Message, OnSpeechComplete );

		currentSpeaker = teller;

		return false;
	}

	void OnHammer()
	{
		//Accused.Speak( "I didn't do it...", OnSpeechComplete );

		var GO = GameObject.Instantiate( Resources.Load( "Prefabs/Order" ) ) as GameObject;
		GO.transform.parent = GameObject.Find ( "Panel" ).transform;
	}

	public override bool Update (float timeInState)
	{
		return false;
	}
}

