using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour 
{
	public static CheckpointManager instance;

	public Checkpoint startingCheckpointTop;
	public Checkpoint startingCheckpointBottom;
	public Checkpoint currentCheckpointTop;
	public Checkpoint currentCheckpointBottom;

	private Checkpoint[] _checkpoints;

	void Awake ()
	{
		instance = this;

		_checkpoints = GameObject.FindObjectsOfType<Checkpoint> ();
	}

	public void GetCheckpoint (Checkpoint p_checkpoint)
	{
		if (p_checkpoint.isTop)
		{
			currentCheckpointTop = p_checkpoint;
		} else 
		{
			currentCheckpointBottom = p_checkpoint;
		}
		
		p_checkpoint.RemoveCheckpoint ();
    }

	public void ResetCheckpoints ()
	{
		for (int i = 0; i < _checkpoints.Length; i++) 
		{
			_checkpoints [i].RestoreCheckpoint ();
		}
		currentCheckpointTop = startingCheckpointTop;
		currentCheckpointBottom = startingCheckpointBottom;
	}
}
