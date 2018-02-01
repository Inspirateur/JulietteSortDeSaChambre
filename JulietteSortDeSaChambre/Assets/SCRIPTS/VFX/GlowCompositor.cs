using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowCompositor : MonoBehaviour {
    public Material glowMaterial;
    public RenderTexture highlight;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        glowMaterial.SetTexture("_Highlight", highlight);
        Graphics.Blit(source, destination, glowMaterial);
    }
}
