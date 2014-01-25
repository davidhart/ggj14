using UnityEngine;
using System.Collections;
using System.Linq;

public class MicrophoneInput
{
	public MicrophoneInput()
	{
		var newGO = new GameObject( "Microphone" );
		var audioSource = newGO.AddComponent< AudioSource >();


		Debug.Log ( "MICROPHONE DEVICES: " );
		foreach( var device in Microphone.devices )
		{
			Debug.Log ( device );
		}
		audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
	}
}
