using System;

public class VerdictManager
{
	public static VerdictManager Instance;

	float totalInfluence = 0.0f;

	public float Influence { get { return totalInfluence; } }

	public VerdictManager()
	{
		Instance = this;
	}

	public void Reset()
	{
		totalInfluence = 0.0f;
	}

	public void InfluenceJury( float influence )
	{
		totalInfluence += influence;
	}
}

