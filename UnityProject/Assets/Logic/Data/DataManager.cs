using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager
{
	public static DataManager Instance;

	public List<CaseData> Cases;

	int currentCase = -1;
	public CaseData CurrentCase { get { return Cases[ currentCase ]; } }

	public DataManager()
	{
		Instance = this;

		Load();
	}

	public void NextCase()
	{
		currentCase++;

		if( currentCase >= Cases.Count )
			currentCase = 0;
	}

	public void ToJson()
	{
		CaseData myData = new CaseData();
		myData.accusedData = new AccusedData();
		myData.accusedData.Age = 23;
		myData.accusedData.Gender = "Not Sure";
		myData.accusedData.Crime = "Eating Pie";
		myData.accusedData.FaceTexture = "";

		myData.announcements = new List<TrialAnnouncement>();
		var announcement = new TrialAnnouncement();

		announcement.Source = TrialAnnouncement.eSource.Prosecution;
		announcement.Message = "HE'S CLEARLY GUILTY, HE'S GOT A FUNNY LOOKING BEARD";
		announcement.Influence = 0.5f;

		myData.announcements.Add( announcement );
		myData.announcements.Add( announcement );

		Debug.Log ( fastJSON.JSON.Instance.ToJSON( myData ) );
	}

	public void Load()
	{
		Cases = new List<CaseData>();

		var textAssets = Resources.LoadAll( "Cases", typeof( TextAsset ) );

		var orderedTextAssets = textAssets.OrderBy( q => q.name );

		foreach( var textAsset in orderedTextAssets )
		{
			var myCase = new CaseData();
			Cases.Add( myCase );
			fastJSON.JSON.Instance.FillObject( myCase, ((TextAsset)textAsset).text );
		}

		Cases.ForEach( q => Debug.Log ( "Found Trial for: " + q.accusedData.Name ) );
	}
}
