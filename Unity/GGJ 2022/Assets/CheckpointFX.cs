using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFX : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake () {
        rigid = GetComponentInChildren<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        //Debug.Log("rigid " + rigid.gameObject.name);
        //Debug.Log("sprite " + sprite.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFX () {
        StartCoroutine(Fall());
    }

    IEnumerator Fall () {
        float startY = rigid.transform.position.y;
        rigid.simulated = true;
        Vector2 forceDir = new Vector2(1f, 1f);
        rigid.AddForce(forceDir*3f, ForceMode2D.Impulse);
        rigid.AddTorque(500f, ForceMode2D.Impulse);
        while (startY - rigid.transform.position.y < 20f) {
            yield return null;
        }
        sprite.enabled = false;
    }
}
