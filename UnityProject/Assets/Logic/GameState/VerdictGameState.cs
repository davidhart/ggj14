using UnityEngine;
using System;

public class VerdictGameState : BaseGameState
{
	public VerdictGameState ()
	{
	}

	public override void OnEnter ()
	{
		Debug.Log( VerdictManager.Instance.Influence );
	}

	public override bool Update (float timeInState)
	{
		return true;
	}

	public override void OnExit ()
	{
	}
}

