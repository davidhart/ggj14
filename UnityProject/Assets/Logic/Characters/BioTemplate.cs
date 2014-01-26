using UnityEngine;
using System.Collections;

public class BioTemplate : MonoBehaviour
{
	public UISprite faceSprite;
	public UITexture faceTexture;

	public UILabel Name;
	public UILabel Age;
	public UILabel Crime;

	public void Setup( AccusedData accusedData )
	{
		var face = Resources.Load( "Cases/Faces/" + accusedData.FaceTexture ) as Texture2D;

		faceTexture.mainTexture = face;
		faceTexture.transform.localScale = new Vector3( 1.5f, 1.5f, 1 );
		faceTexture.transform.localPosition = new Vector3( -0.5f, 0.0f, 0.0f );

		Name.text = "Name: " + accusedData.Name;
		Age.text = "Age: " + accusedData.Age;

		Crime.text = "Crime: " + accusedData.Crime;
	}
}
