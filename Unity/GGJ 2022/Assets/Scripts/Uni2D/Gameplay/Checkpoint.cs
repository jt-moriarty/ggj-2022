using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private Collider2D _collider;
    private CheckpointFX _fx;

	private Vector3 _spawnPoint;
	public Vector3 spawnPoint
	{
		get 
		{
			return _spawnPoint;
		}

		private set 
		{
			_spawnPoint = value;
		}
	}

	void Awake ()
	{
		_collider = GetComponentInChildren<Collider2D>();
        _fx = GetComponentInChildren<CheckpointFX>();
        spawnPoint = transform.position;
	}

	public void RemoveCheckpoint ()
	{
        Debug.Log("RemoveCheckpoint", this.gameObject);
        _fx.StartFX();
        _collider.enabled = false;
	}

	public void RestoreCheckpoint ()
	{
        _collider.enabled = true;
	}
}
