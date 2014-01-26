using UnityEngine;
using System.Collections;

public class BlinkFix : MonoBehaviour
{
	
	void Start()
	{
		animation["Blink"].time = Random.Range(0.0f, animation["Blink"].length);
	}

}
