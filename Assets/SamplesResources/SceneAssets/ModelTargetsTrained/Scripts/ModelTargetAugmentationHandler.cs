/*==============================================================================
Copyright (c) 2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface,
/// and triggers animations on the detected model target.
/// </summary>
public class ModelTargetAugmentationHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PUBLIC_MEMBER_VARIABLES

    public Material animationMaterial;

    #endregion PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    Dictionary<MeshRenderer, Material> savedMaterials;
    bool alreadyDetectedOnce;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        this.savedMaterials = new Dictionary<MeshRenderer, Material>();

        ModelTargetBehaviour modelTargetBehaviour = GetComponent<ModelTargetBehaviour>();

        if (modelTargetBehaviour)
        {
            modelTargetBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // if first time detection, trigger animations
            if (!this.alreadyDetectedOnce)
            {
                this.alreadyDetectedOnce = true;

                // Trigger the various animations and FX
                Invoke("SwitchMaterial", 0.0f);
                Invoke("SwitchMaterial", 1.25f);
                Invoke("PlayParticleSystems", 1.5f);
                Invoke("PlayAnimations", 1.5f);
            }
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS

    void SwitchMaterial()
    {
        var materialSwitchers = GetComponentsInChildren<MaterialSwitch>();

        foreach (var ms in materialSwitchers)
        {
            ms.SwitchMaterial();
        }
    }

    void PlayParticleSystems()
    {
        var particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (var ps in particleSystems)
        {
            ps.Play();
        }
    }

    void StopParticleSystems()
    {
        var particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (var ps in particleSystems)
        {
            ps.Stop();
        }
    }

    void PlayAnimations()
    {
        var animations = GetComponentsInChildren<SimpleAnimation>();

        if (animations == null || animations.Length == 0)
        {
            return;
        }

        int animCount = animations.Length;
        int animCompleteCount = 0;
        foreach (var anim in animations)
        {
            // Setup the animation callbacks
            anim.OnStartEvent += () =>
            {
                // Save the original material of the animated game object
                // and its children
                SaveMaterials(anim.gameObject);

                // Apply animation material
                if (this.animationMaterial)
                {
                    SetMaterial(anim.gameObject, this.animationMaterial);
                }
            };

            anim.OnCompleteEvent += () =>
            {
                // We are done with this animation,
                // so we unregister the event handlers.
                anim.ClearEventHandlers();

                // Restore original materials of the animated game object
                // and its children
                RestoreMaterials(anim.gameObject);

                // Increase animation completed count
                animCompleteCount++;

                // Check if all animations associated to this target,
                // have completed
                if (animCompleteCount >= animCount)
                {
                    // When ALL the animations are over,
                    // stop the particle systems.
                    StopParticleSystems();

                    // Then schedule a "replay" some time later
                    Invoke("RewindAnimations", 4.5f);
                    Invoke("PlayAnimations", 5.0f);
                    Invoke("PlayParticleSystems", 5.0f);
                }
            };

            // Start playing the animation
            anim.Play();
        }
    }

    void RewindAnimations()
    {
        var animations = GetComponentsInChildren<SimpleAnimation>();

        if (animations == null || animations.Length == 0)
        {
            return;
        }

        foreach (var anim in animations)
        {
            anim.Rewind();
        }
    }

    void SaveMaterials(GameObject go)
    {
        if (!go)
        {
            return;
        }

        var mrs = go.GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in mrs)
        {
            this.savedMaterials[mr] = mr.material;
        }
    }

    void RestoreMaterials(GameObject go)
    {
        if (!go)
        {
            return;
        }

        var renderers = go.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
        {
            if (this.savedMaterials.ContainsKey(renderer))
            {
                renderer.material = this.savedMaterials[renderer];
            }
        }
    }

    void SetMaterial(GameObject go, Material material)
    {
        if (!go)
        {
            return;
        }

        var renderers = go.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }

    #endregion // PRIVATE_METHODS
}
