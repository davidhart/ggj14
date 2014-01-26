using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour
{
	MicrophoneInput input;
	GameStateManager gameStateManager;

	void Awake()
	{
		//	Construct all systems.
		input = new MicrophoneInput();
		new DataManager();
		gameStateManager = new GameStateManager();
		new VerdictManager();
	}

	void Update()
	{
		input.UpdateTest();
		gameStateManager.Update();
	}
}
