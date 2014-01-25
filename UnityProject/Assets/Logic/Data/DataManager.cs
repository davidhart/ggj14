using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager
{
	public static DataManager Instance;

	public List<CaseData> Cases;

	public DataManager()
	{
		Instance = this;

		//ToJson();
		Load();
	}

	public void ToJson()
	{
		CaseData myData = new CaseData();
		myData.accusedData = new AccusedData();
		myData.accusedData.Age = 23;
		myData.accusedData.Gender = "Not Sure";
		myData.accusedData.Crime = "Rape";
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

		foreach( var textAsset in textAssets )
		{
			var myCase = new CaseData();
			Cases.Add( myCase );
			fastJSON.JSON.Instance.FillObject( myCase, ((TextAsset)textAsset).text );
		}

		Cases.ForEach( q => Debug.Log ( "Found Trial for: " + q.accusedData.Name ) );
	}
}
