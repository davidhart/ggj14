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
		CharacterSpriteSets.ToList().ForEach(q=>q.gameObject.active = false);
		
		CharacterSpriteSets[(int)c].gameObject.active = true;
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
		if( rabbleLoops <= 0 )
			return;

		if( !transform.parent.animation.IsPlaying( "Rabble" ) )
		{
			rabbleLoops--;
			transform.parent.animation.Play( "Rabble" );
		}
	}
}
