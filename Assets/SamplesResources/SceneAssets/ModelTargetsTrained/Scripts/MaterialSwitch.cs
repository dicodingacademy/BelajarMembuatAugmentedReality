/*==============================================================================
Copyright (c) 2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/
using System.Collections;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    #region PUBLIC_MEMBERS

    public Material[] materials;

    #endregion // PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS

    private int materialIndex = -1;

    #endregion // PRIVATE_MEMBERS


    #region PUBLIC_METHODS

    public void SwitchMaterial()
    {
        if (this.materials == null || this.materials.Length == 0)
        {
            return;
        }

        // Increase the material index
        this.materialIndex++;
        if (this.materialIndex >= this.materials.Length)
        {
            this.materialIndex = 0;
        }

        // Set the material
        var renderers = this.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = this.materials[materialIndex];
            if (renderer.material.HasProperty("_ScanTime"))
            {
                renderer.material.SetFloat("_ScanTime", 0.0f);
                StartCoroutine(UpdateScanTime(renderer.material, 1.0f));
            }
        }
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    IEnumerator UpdateScanTime(Material mat, float duration)
    {
        float time = 0;
        float deltaTime = 0.05f;
        var wait = new WaitForSeconds(deltaTime);
        while (time < duration)
        {
            if (mat.HasProperty("_ScanTime"))
            {
                mat.SetFloat("_ScanTime", time);
            }

            time += deltaTime;
            yield return wait;
        }
    }

    #endregion // PRIVATE_METHODS
}
