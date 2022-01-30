using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start() {
        
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            gameOverScreen.SetActive(true);
        }
    }
}
