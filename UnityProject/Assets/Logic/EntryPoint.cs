using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour
{
	MicrophoneInput input;

	void Awake()
	{
		//	Construct all systems.
		input = new MicrophoneInput();
	}

	void Start()
	{
	
	}
	
	void Update()
	{
		input.UpdateTest();
	}
}
