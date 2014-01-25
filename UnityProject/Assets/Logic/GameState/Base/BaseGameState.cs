using System;

public abstract class BaseGameState
{
	public BaseGameState()
	{
	}

	public virtual void OnEnter() {}
	public virtual bool Update( float timeInState ) { return false; }
	public virtual void OnExit() {}
}

