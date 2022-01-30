using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{
    private Animator animator;
    private float minIdleCD = 4.0f;
    private float lastIdleTime;

    // Start is called before the first frame update
    void Awake () {
        animator = GetComponent<Animator>();
        lastIdleTime = Time.time;
    }

    private void Update () {
        if (Time.time - lastIdleTime > minIdleCD && Random.Range(0f,1f) > 0.8f) {
            animator.SetTrigger("idle");
            lastIdleTime = Time.time;
        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            animator.SetTrigger("fly");
            IEnumerator flyAway = FlyAway(Random.Range(1f, 2f));
            StartCoroutine(flyAway);
        }
    }

    IEnumerator FlyAway (float tweenTime) {
        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        endPos.y += Random.Range(10f,15f);
        endPos.x += Random.Range(5f, 20f);
        while (Time.time - startTime < tweenTime) {
            yield return null;
            Vector3 newPos = startPos;
            float t = (Time.time - startTime) / tweenTime;
            newPos.x = Mathf.Lerp(startPos.x, endPos.x, t);
            newPos.y = Mathf.Lerp(startPos.y, endPos.y, t*t);
            transform.position = newPos;
        }

        transform.position = endPos;
    }
}
