using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour 
{
	public GameObject titleScreen;

    public void Awake () {
        //SoftPauseScript.softPaused = true;
    }

    public void StartGame () {
		titleScreen.SetActive (false);
		LoadGame();
	}

    public void Update () {
        if (Keyboard.current.anyKey.wasPressedThisFrame) {
            LoadGame();
        }
    }

    public void LoadGame () {
        titleScreen.SetActive(false);
        //SoftPauseScript.softPaused = false;
	}
}
