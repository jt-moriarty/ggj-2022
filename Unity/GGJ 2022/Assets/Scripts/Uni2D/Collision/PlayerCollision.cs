using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour 
{
	private Rigidbody2D myRigidbody;
	
	void Awake() 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void OnCollisionEnter2D(Collision2D p_collision)
	{
		// Check collision stuff
	}

	void OnTriggerEnter2D(Collider2D p_other)
	{
		if (p_other.CompareTag ("Hazard")) 
		{
			Player.instance.Die ();
		}

		if (p_other.CompareTag ("Checkpoint")) 
		{
			CheckpointManager.instance.GetCheckpoint(p_other.GetComponent<Checkpoint>());
		}

		/*if (p_other.CompareTag ("Goal")) 
		{
			ProgressManager.instance.HitGoal();
		}

		if (p_other.CompareTag ("Advance")) 
		{
			Player.instance.HitAdvance();
		}

		if (p_other.CompareTag ("Ability")) 
		{
			AbilityPickup l_pickup = p_other.GetComponent<AbilityPickup> ();
			AbilityManager.Ability l_ability = l_pickup.ability;
			string l_key = l_pickup.messageKey;
			Player.instance.PickupAbility(l_ability, l_key);
			p_other.gameObject.SetActive (false);
		}*/

		if (p_other.CompareTag ("Interactable")) 
		{
			Debug.Log("interactable enter");
			Interactable l_newInteractable = p_other.GetComponentInChildren<Interactable>();

			if (l_newInteractable == null)
			{
				Debug.LogWarning("WARNING: object set with tag Interactable but not Interactable component was found. Check Inspector: ", p_other);
				return;
			}

			l_newInteractable.OnPlayerEnter();

			Player.instance.SetInteractable(l_newInteractable);
		}
	}

	void OnTriggerExit2D(Collider2D p_other)
	{
		if (p_other.CompareTag ("Interactable")) 
		{
			Debug.Log("interactable exit");
			Interactable l_newInteractable = p_other.GetComponentInChildren<Interactable>();

			if (l_newInteractable == null)
			{
				Debug.LogWarning("WARNING: object set with tag Interactable but not Interactable component was found. Check Inspector: ", p_other);
				return;
			}

			l_newInteractable.OnPlayerExit();

			if (Player.instance.GetInteractable() != l_newInteractable)
			{
				return;
			}

			Player.instance.SetInteractable(null);
		}
	}
}
