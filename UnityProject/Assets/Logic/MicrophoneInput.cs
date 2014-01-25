using UnityEngine;
using System.Collections;
using System.Linq;

public class MicrophoneInput
{
	AudioSource audioSource;
	string deviceName;

	public MicrophoneInput()
	{
		var newGO = new GameObject( "Microphone" );
		audioSource = newGO.AddComponent< AudioSource >();

		if( Microphone.devices.Length == 0 )
		{
			Debug.LogError( "No Microphone Devices Detected" );
			deviceName = string.Empty;
		}

		deviceName = Microphone.devices[0];
		Debug.Log( "Using Microphone Device: " + deviceName );

		audioSource.clip = Microphone.Start(deviceName, true, 3, 44100);
	}

	float timer = 3.0f;

	public void UpdateTest()
	{
		if( string.IsNullOrEmpty( deviceName ) )
			return;

		timer -= Time.deltaTime;

		if( timer > 0.0f )
			return;

		Debug.Log ( "Playing" );

		timer = 5.0f;

		Microphone.End( deviceName );

		audioSource.Play();
	}

	public void TestForBang()
	{
		var samples = new float[audioSource.clip.samples * audioSource.clip.channels];

		//AudioSource

		//for( int nIndex = 0; nIndex < samples.
	}
}
