using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetPlayerState : MonoBehaviour 
{
	private Transform myTransform;
	PlayerStateManager myStateManager;

	void Awake()
	{
		myTransform = transform;
		myStateManager = GetComponent<PlayerStateManager>();
	}

	void OnEnable()
	{
		Initialize();
		SoftPauseScript.instance.AddToHandler (Enums.UpdateType.SoftUpdate, SoftUpdate);
	}

	public void Initialize() 
	{
		// Set up event listeners for state changes
		myStateManager.ChangeGroundState += ChangeGroundState;
		myStateManager.ChangeAttackState += ChangeAttackState;
		myStateManager.ChangeStunnedState += ChangeStunnedState;
		myStateManager.ChangeMoving += ChangeMoving;

		//myAnimator.AnimationCompleted = OnAnimationComplete;
	}

	void OnDisable()
	{
		//myAnimator.AnimationCompleted = OnAnimationComplete;

		myStateManager.ChangeGroundState -= ChangeGroundState;
		myStateManager.ChangeAttackState -= ChangeAttackState;
		myStateManager.ChangeStunnedState -= ChangeStunnedState;
		myStateManager.ChangeMoving -= ChangeMoving;
	}

	void SoftUpdate(GameObject dispatcher)
	{
		// We will probably need to check some things here
	}

	private void ChangeGroundState(GameObject dispatcher)
	{
		switch (myStateManager.currentGroundState)
		{
			case Enums.PlayerGroundState.OnGround:
			{
				if (myStateManager.isMoving)
				{
					Player.instance.PlayAnimation("run");
				}
				else if (myStateManager.currentAttackState == Enums.PlayerAttackState.None)
				{
					Player.instance.PlayAnimation("idle");
				}
				break;
			}
			case Enums.PlayerGroundState.Rising:
			{
				Player.instance.PlayAnimation("jump");
				break;
			}
			case Enums.PlayerGroundState.Falling:
			{
				Player.instance.PlayAnimation("fall");
				break;
			}
			case Enums.PlayerGroundState.Landing:
			{
				if (myStateManager.currentAttackState == Enums.PlayerAttackState.None)
				{
					Player.instance.PlayAnimation("land");
				}
				break;
			}
		}
	}

	private void ChangeAttackState(GameObject dispatcher)
	{
		/*if (myStateManager.currentGroundState == Enums.PlayerGroundState.Rising || 
		    myStateManager.currentGroundState == Enums.PlayerGroundState.Falling)
		{
			switch (myStateManager.currentAttackState)
			{
				// Do air attacks
			}
		}
		else
		{
			switch (myStateManager.currentAttackState)
			{
				// Do ground attacks
			}
		}*/
	}

	private void ChangeStunnedState(GameObject dispatcher)
	{
		switch (myStateManager.currentStunnedState)
		{
			case Enums.PlayerStunnedState.Hit:
			{
				Player.instance.PlayAnimation("hit");
				break;
			}
		}
	}

	private void ChangeMoving(GameObject dispatcher)
	{
		if (Player.instance.isSliding) 
		{
			return;
		}

		if (myStateManager.isMoving)
		{
			if (myStateManager.currentGroundState == Enums.PlayerGroundState.OnGround)
			{
				Player.instance.PlayAnimation("run");
			}
		}
		else
		{
			if (myStateManager.currentGroundState == Enums.PlayerGroundState.OnGround)
			{
				Player.instance.PlayAnimation("idle");
			}
		}
	}

	public void OnAnimationComplete()
	{
		// Take care of animation end
	}
}