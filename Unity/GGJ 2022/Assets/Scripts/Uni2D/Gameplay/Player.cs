using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private const float DEATH_TIMER = 2f;

	//public Animator animator;

	[SerializeField] private Player otherPlayer = null;
	[SerializeField] private bool _isTop = true;

	private Rigidbody2D _rigidbody;
	private BasicPlayerMovement _movement;
	private BoxCollider2D _hitbox;
	private Transform _transform;
	private Interactable _currentInteractable;

	public bool isSliding
	{
		get 
		{
			return _movement.isSliding;
		}
	}

	void Awake ()
	{
		_rigidbody = GetComponent<Rigidbody2D> ();
		_movement = GetComponent<BasicPlayerMovement> ();
		_hitbox = GetComponent<BoxCollider2D> ();
		_transform = transform;

		Debug.Assert(otherPlayer != null);

//		animator.AnimationCompleted = OnAnimationComplete;
	}

    void Start()
    {

    }

    void Update ()
	{
		CheckInteract();
	}

	public void Die ()
	{
		Respawn ();
		//SetControl(false);
		//PlayAnimation ("death");

		//StartCoroutine (DeathCountdown ());
	}

	private IEnumerator DeathCountdown ()
	{
		yield return new WaitForSeconds (DEATH_TIMER);

		LivesManager.instance.LoseLife ();

		if (LivesManager.instance.lives > 0) 
		{
			Respawn ();
		} 
	}

	public void PickupAbility (AbilityManager.Ability p_ability, string p_key)
	{
		AbilityManager.instance.AddAbility (p_ability);
		PopupManager.instance.ShowMessage (p_key);
		SoftPauseScript.softPaused = true;
		Time.timeScale = 0f;
	}

	public void Respawn ()
	{
		if (_transform.localScale.y < 0) 
		{
			_movement.FlipGravity ();
		}

		if (_rigidbody.gravityScale == 0)
		{
			_rigidbody.gravityScale = 2f;
		}

		Vector3 l_respawnPoint = _isTop ? CheckpointManager.instance.currentCheckpointTop.spawnPoint : CheckpointManager.instance.currentCheckpointBottom.spawnPoint;

		transform.position = l_respawnPoint;
		SetControl(true);
		//PlayAnimation ("idle");
		_movement.Respawn ();
	}

	public void HitGoal ()
	{
		SetControl(false);
		//PlayAnimation ("idle");
	}

	public void HitAdvance ()
	{
		SetControl (false);
		ProgressManager.instance.Win ();
	}

	public void LaunchPlayer (int p_correct, int p_given)
	{
		Vector2 l_launchDirection = new Vector2 (1f, 1f);
		_rigidbody.simulated = true;
		_hitbox.enabled = true;
		//PlayAnimation ("jump");

		Vector2 l_launchForce = Vector2.zero;

		if (p_given == p_correct) 
		{
			l_launchForce = l_launchDirection * Constants.END_LEVEL_LAUNCH_FORCE;
			_rigidbody.velocity = l_launchForce;
			return;
		}

		if (p_given > p_correct) 
		{
			l_launchForce = l_launchDirection * Constants.END_LEVEL_LAUNCH_FORCE * 1.5f;
		} 
		else 
		{
			l_launchForce = l_launchDirection * Constants.END_LEVEL_LAUNCH_FORCE / 2f;
		}

		_rigidbody.velocity = l_launchForce;
	}

	private void SetControl (bool p_control)
	{
		_movement.enabled = p_control;
		_rigidbody.simulated = p_control;
		_rigidbody.velocity = (p_control) ? Vector2.zero : _rigidbody.velocity;
		_hitbox.enabled = p_control;
	}

	public Interactable GetInteractable ()
	{
		return _currentInteractable;
	}

	public void SetInteractable (Interactable p_interactable)
	{
		_currentInteractable = p_interactable;
	}

	private void CheckInteract ()
	{
		if (InputManager.instance.GetInteract())
		{
			if (_currentInteractable == null)
			{
				return;
			}

			_currentInteractable.Interact();
		}
	}

	/*public void PlayAnimation (string p_animationName)
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName(p_animationName))
		{
			return;
		}

		animator.SetTrigger(p_animationName);
	}*/

	/*private void OnAnimationComplete(tk2dSpriteAnimator anim, tk2dSpriteAnimationClip clip)
	{
		if (animator.CurrentClip.name.Contains ("super")) 
		{
			animator.Play ("superJumpReady");
		}
	}*/
}
