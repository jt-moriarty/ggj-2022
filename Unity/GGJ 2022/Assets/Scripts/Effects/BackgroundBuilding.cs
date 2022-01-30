using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBuilding : MonoBehaviour
{
    [SerializeField] private WindowSection[] windowSections;

    private Renderer myRenderer;

    void Awake ()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void OnEnable ()
    {
        RandomizeWindows();
    }

    public void RandomizeWindows ()
    {
        int randomIndex = Random.Range(0, windowSections.Length);
        WindowSection newWindow = windowSections[randomIndex];

        myRenderer.material.SetTexture("_EmissionMap", newWindow.texture);
        myRenderer.material.SetColor("_EmissionColor", newWindow.emissionColor);
        myRenderer.material.EnableKeyword("_EMISSION");

        //Debug.Log("material color: " + myRenderer.material.GetColor("_EmissionColor"));
    }
}

[System.Serializable]
public class WindowSection 
{
    public Texture texture;
    [ColorUsageAttribute(true, true)]
    public Color emissionColor;
}