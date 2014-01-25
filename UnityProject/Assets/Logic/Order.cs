using UnityEngine;
using System.Collections;

public class Order : MonoBehaviour
{
	float timer = 0.6f;
	void Update()
	{
		timer -= Time.deltaTime;

		if( timer < 0.0f )
			GameObject.Destroy( gameObject );
	}
}
