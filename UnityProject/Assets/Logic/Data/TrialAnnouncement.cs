using System;


public class TrialAnnouncement
{
	public enum eSource
	{
		Defense,
		Prosecution,
		Accused
	};

	public eSource Source;
	public string Message;

	public float Influence;
}
