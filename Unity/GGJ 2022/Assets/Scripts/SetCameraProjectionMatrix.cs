using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class SetCameraProjectionMatrix : MonoBehaviour {

    Camera cam;
    public bool flipHorizontal;
    
    void Awake () {
        cam = GetComponent<Camera>();
    }

    void OnPreCull () {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreRender () {
        GL.invertCulling = flipHorizontal;
    }

    void OnPostRender () {
        GL.invertCulling = false;
    }
}
