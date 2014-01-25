using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour
{
	void Awake()
	{
		//	Construct all systems.
		new CharacterFactory();
		new MicrophoneInput();
	}

	void Start()
	{
	
	}
	
	void Update()
	{
	}
}
