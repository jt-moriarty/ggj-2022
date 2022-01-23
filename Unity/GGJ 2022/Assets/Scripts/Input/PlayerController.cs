using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    Player myPlayer;

    [SerializeField]
    bool isUpsideDown;

    void Awake()
    {
        myPlayer = GetComponent<Player>();
    }

    public void Jump()
    {
        Debug.Log("Jump");
    }

    public void Movement(Vector2 direction)
    {
        Debug.Log(direction);
    }

}
