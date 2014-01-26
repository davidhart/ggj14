using System;
using UnityEngine;

public class BioGameState : BaseGameState
{
	CaseData currentCase;

	BioTemplate bioTemplate;

	public override void OnEnter()
	{
		DataManager.Instance.NextCase();

		currentCase = DataManager.Instance.CurrentCase;

		MicrophoneInput.OnHammer += OnHammer;

		var GO = GameObject.Instantiate( Resources.Load( "Prefabs/Characters/Bio" ) ) as GameObject;
		bioTemplate = GO.GetComponent<BioTemplate>();
		bioTemplate.Setup( currentCase.accusedData );
	}

	public override void OnExit()
	{
		GameObject.Destroy( bioTemplate.gameObject );

		MicrophoneInput.OnHammer -= OnHammer;

		hammered = false;
	}

	bool hammered = false;
	void OnHammer()
	{
		hammered = true;
	}

	public override bool Update (float timeInState)
	{
		return hammered;
	}
}
