using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerdictTransition
{
	public bool Guilty;
	public bool Change;
	public Character Character;
}

public class VerdictScene : MonoBehaviour
{
	public UIAnchor AnchorTop;
	public UIAnchor AnchorBottom;
	public UIAnchor AnchorRight;
	public UIAnchor AnchorLeft;
	
	public Transform WeFindTheDefendantParent;
	public Transform VerdictTextParent;
	
	public UISprite Background;
	
	public Transform JuryStartNode;
	public float JurySeparation;
	
	public GameObject GuiltyText;
	public GameObject NotGuiltyText;
	public GameObject AndTheVerdictIsText;
	
	private bool IsDone = false;
	private bool IsGuilty = false;
	
	public List<Character> Jury = new List<Character>();
	
	private Queue<VerdictTransition> TransitionQueue = new Queue<VerdictTransition>();
	
	void Awake()
	{
		var size = new Vector3( AnchorRight.transform.localPosition.x - AnchorLeft.transform.localPosition.x, 
	    	                    AnchorTop.transform.localPosition.y - AnchorBottom.transform.localPosition.y,
	        	                1 );
		Background.transform.localScale = size;
	}
	
	public void ReParentJury(List<Character> jury)
	{
		Jury.AddRange(jury);
		
		int i = 0;
		
		foreach(Character j in Jury)
		{
			GameObject goParent = new GameObject("JuryParent");
			
			goParent.transform.parent = JuryStartNode;
			goParent.transform.localPosition = new Vector3(i++ * JurySeparation - JurySeparation * Jury.Count * 0.5f, 0, 0);
			goParent.transform.localScale = Vector3.one;
			goParent.transform.localRotation = Quaternion.identity;
		
			j.transform.parent = goParent.transform;
			j.transform.localPosition = Vector3.zero;
			j.transform.localScale = Vector3.one;
			j.transform.localRotation = Quaternion.identity;	
		}
	}
	
	public void SetupAnimationQueue(int numJuryGuilty)
	{
		IsGuilty = numJuryGuilty >= Jury.Count / 2;
	
		var isInFavor = new List<bool>();
		var willChangeMind = new List<bool>();
		
		for (int i = 0; i < Jury.Count; i++)
		{
			isInFavor.Add(false);
			willChangeMind.Add(false);
		}
		
		for (int i = 0; i < numJuryGuilty; ++i)
		{
			int index = 0;
			
			while (true)
			{
				index = Random.Range(0, Jury.Count);
				
				Debug.Log(index);
				
				if (!isInFavor[index])
				{
					isInFavor[index] = true;
					
					if ( Random.value > 0.2f && index != 0 && index != Jury.Count - 1 )
					{
						willChangeMind[index] = true;
					}
					
					break;
				}
			}
		}
		
		for (int i = 0; i < Jury.Count; ++i)
		{
			bool willGoGuilty = isInFavor[i];
			
			if ( willChangeMind[i] )
				willGoGuilty = !willGoGuilty;
		
			var v = new VerdictTransition()
			{
				Guilty = willGoGuilty,
				Change = false,
				Character = Jury[i]	
			};
		
			TransitionQueue.Enqueue(v);
			
			if ( i != 0 && willChangeMind[i - 1] )
			{
				v = new VerdictTransition();
			
				v.Guilty = isInFavor[i - 1];
				v.Character = Jury[i - 1];
				v.Change = true;
				
				 TransitionQueue.Enqueue(v); 
			}
		}
		
		
	}
	
	public void BeginVerdictAnimation()
	{
		PopVerdictAnim();
	}
	
	public void PopVerdictAnim()
	{
		Debug.Log("PopVerdictAnim");
	
		if ( TransitionQueue.Count == 0 )
		{
			StartCoroutine("DelayCoroutine");
			
			return;
		}
		
		var v = TransitionQueue.Dequeue();
		
		v.Character.AnimationCompletedCallback = PopVerdictAnim;
		v.Character.PlayVerdictAnimation(v.Guilty, v.Change);
	}
	
	public bool IsAnimating()
	{
		return IsDone;
	}
	
	private IEnumerator DelayCoroutine()
	{
		Debug.Log("DELAY ROUTINE");
	
		yield return new WaitForSeconds(1.5f);
		
		ZoomText text1 = ZoomText.CreateZoomText("We find the defendant..", WeFindTheDefendantParent);
		text1.BeginAnimation();
		text1.Text.transform.localScale = new Vector3(40, 40, 1);
		
		yield return new WaitForSeconds(0.8f);
		
		ZoomText text2 = ZoomText.CreateZoomText(IsGuilty ? "GUILTY!" : "NOT GUILTY!", VerdictTextParent);
		text2.OnAnimationCompleted = CaseCompleteTextCompleted;
		text2.BeginAnimation();
	}
	
	private void CaseCompleteTextCompleted()
	{
		Debug.Log( "VERDICT COMPLETE" );
		
		IsDone = true;
	}
	
}
