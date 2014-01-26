using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public enum CharacterDirection
{
	Down,
	Up,
	Left,
	Right,
}

public class Character : MonoBehaviour
{
	public CharacterSpriteSet[] CharacterSpriteSets;
	public CharacterSpeech CharacterSpeech;
	
	public Animation BodyAnimation;
	
	public Color[] SkinColors;
	
	public Action AnimationCompletedCallback;
	
	private CharacterDirection PrevDirection;
	private CharacterDirection NewDirection;
	
	public Transform ThoughtOffset;
	
	void Awake()
	{
		SetDirectionInternal(CharacterDirection.Down);
		PrevDirection = CharacterDirection.Down;
	}
	
	public void SetBodyColor(Color c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.SetBodyColor(c));
	}
	
	public void SetSkinColor(Color c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.SetSkinColor(c));
	}
	
	public void SetDirection(CharacterDirection c)
	{
		NewDirection = c;
	}
	
	private void SetDirectionInternal(CharacterDirection c)
	{
		CharacterSpriteSets.ToList().ForEach(q=>q.gameObject.SetActiveRecursively(false));
		
		CharacterSpriteSets[(int)c].gameObject.SetActiveRecursively(true);
		
		PrevDirection = c;
	}

	public void ClearSpeech()
	{
		CharacterSpeech.ClearSpeech();
	}

	public void Speak(string message, System.Action onFinished )
	{
		CharacterSpeech.Speak( message, onFinished );
	}
	
	public void SetRandomSkinColor()
	{
		SetSkinColor(SkinColors[UnityEngine.Random.Range(0, SkinColors.Length)]);
	}
	
	public void PlayWalkAnimation()
	{
		BodyAnimation.Play("Walk");
	}
	
	public void StopWalkAnimation()
	{
		BodyAnimation.Stop("Walk");
	}
	
	public void PlayVerdictAnimation(bool guilty, bool change)
	{
		if ( guilty && change )
			animation.Play("ChangeGuilty");
		else if ( !guilty && change )
			animation.Play("ChangeNotGuilty");
		else if ( guilty && !change )
			animation.Play("ChooseGuilty");
		else if ( !guilty && !change )
			animation.Play("ChooseNotGuilty");
	}
	
	public void AnimationCompleted()
	{
		AnimationCompletedCallback();
	}

	int rabbleLoops = -1;
	public void TriggerRabble()
	{
		if( UnityEngine.Random.value < 0.5f )
			return;

		int intensity = (int)( UnityEngine.Random.value * 100 ) % 3;
		transform.parent.animation.Play( "Rabble" );

		rabbleLoops = intensity;
	}

	void Update()
	{
		if ( NewDirection != PrevDirection )
		{
			Debug.Log ("change direction");
			
			SetDirectionInternal(NewDirection);
		}
	
		if( rabbleLoops <= 0 )
			return;

		if( !transform.parent.animation.IsPlaying( "Rabble" ) )
		{
			rabbleLoops--;
			transform.parent.animation.Play( "Rabble" );
		}
		
	}

	public void TriggerThoughts( float intensity )
	{
		intensity += ( UnityEngine.Random.value ) - 0.5f;

		if( intensity > 0.0f )
		{

		}
	}

	public void FaceDown()
	{
		SetDirection(CharacterDirection.Down);
	}
	
	public void FaceUp()
	{
		SetDirection(CharacterDirection.Up);
	}
	
	public void FaceLeft()
	{
		SetDirection(CharacterDirection.Left);
	}
	
	public void FaceRight()
	{
		SetDirection(CharacterDirection.Right);
	}
	
	public void DisplayThought(bool happy)
	{
		if ( happy )
		{
			ThoughtBubble.CreateHappyThought(ThoughtOffset);
		}
		else
		{
			ThoughtBubble.CreateSadThought(ThoughtOffset);
		}
	}
}
