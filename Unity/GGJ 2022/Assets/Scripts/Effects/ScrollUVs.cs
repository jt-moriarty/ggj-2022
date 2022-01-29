using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUVs : MonoBehaviour
{
    // Scroll the UVs of a texture over time

    [SerializeField] private string textureName = "_MainTex";
    [SerializeField] private float scrollSpeedX = 0.5f;
    [SerializeField] private float scrollSpeedY = 0f;

    private Renderer myRenderer;

    private float offsetX = 0f;
    private float offsetY = 0f;

    void Awake()
    {
        myRenderer = GetComponent<Renderer> ();
    }

    void Update()
    {
        offsetX += scrollSpeedX * Time.deltaTime;
        offsetY += scrollSpeedY * Time.deltaTime;
        myRenderer.material.SetTextureOffset(textureName, new Vector2(offsetX, offsetY));
    }
}
