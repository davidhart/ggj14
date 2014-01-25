using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MicrophoneInput
{
	AudioSource audioSource;
	string deviceName;

	//	Use me if we have to... Hardcoding a noise for now.
//	Queue<float> movingAverageBackgroundNoise = new Queue<float>();

	const float noiseLevel = 0.5f;
	const float lockOutTime = 0.5f;

	float timeSinceLastLockOut = 0.0f;

	public static event System.Action OnHammer = delegate {};

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

		audioSource.clip = Microphone.Start(deviceName, true, 60*60, 44100);
		audioSource.Play();
	}

	public void UpdateTest()
	{
		if( string.IsNullOrEmpty( deviceName ) )
			return;

		timeSinceLastLockOut -= Time.deltaTime;

		if( timeSinceLastLockOut > 0.0f )
			return;

		TestForBang();
	}

	public void TestForBang()
	{
		const int numSamples = 1024;
		var samples = new float[numSamples];

		audioSource.GetOutputData( samples, 0 );

		float maxSample = -1.0f;
		float x = 0.0f;
		foreach( var sample in samples )
		{		
			x += 0.001f;

			var start = new Vector3( x, 0.0f, 0.0f );
			var end = new Vector3( x, sample, 0.0f );
			Debug.DrawLine( start, end );
			
			if( sample > maxSample )
				maxSample = sample;
		}

		if( maxSample > noiseLevel )
		{
			timeSinceLastLockOut = lockOutTime;
			Debug.Log ( "ORDER!!!" );

			OnHammer();
		}
	}
}
