/*==============================================================================
Copyright (c) 2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    #region PUBLIC_MEMBERS

    public delegate void OnAnimationStarted();
    public delegate void OnAnimationCompleted();
    public event OnAnimationStarted OnStartEvent;
    public event OnAnimationCompleted OnCompleteEvent;

    public Vector3 startPos = Vector3.zero;
    public Vector3 endPos = Vector3.zero;
    public Vector3 rotationAxis = Vector3.up;
    public Transform rotationCenter;
    public float rotationAngle;
    public float startDelay;
    public float duration = 1;
    public bool loop;
    public bool playOnAwake;

    #endregion // PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS

    Vector3 originalPos = Vector3.zero;
    Quaternion originalRot = Quaternion.identity;
    float startTime;
    bool playing;
    bool startTimeReached;

    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    void Awake()
    {
        if (this.playOnAwake)
        {
            Play();
        }
    }

    void Update()
    {
        if (this.playing)
        {
            float t = Time.realtimeSinceStartup - this.startTime;

            if (t < this.startDelay)
            {
                return;
            }

            if (!this.startTimeReached)
            {
                this.startTimeReached = true;

                if (OnStartEvent != null)
                {
                    OnStartEvent();
                }
            }

            float u = Mathf.Clamp01((t - this.startDelay) / this.duration);
            u = Mathf.SmoothStep(0, 1, u);

            // If the rotation center has been set,
            // this means we want to rotate around a center,
            // which means the rotation also has a translation component
            // In that case, we don't translate.
            float deltaAngle = this.rotationAngle * (Time.deltaTime / this.duration);

            if (this.rotationCenter)
            {
                this.transform.RotateAround(this.rotationCenter.position, this.rotationAxis, deltaAngle);
            }
            else
            {
                this.transform.localPosition = Vector3.Lerp(this.startPos, this.endPos, u);
                this.transform.Rotate(this.rotationAxis, deltaAngle);
            }

            if (u >= 0.9999f)
            {
                this.playing = false;

                if (OnCompleteEvent != null)
                {
                    OnCompleteEvent();
                }

                if (this.loop)
                {
                    Play();
                }
            }
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Play()
    {
        if (this.playing)
        {
            return;// already playing
        }

        this.startTime = Time.realtimeSinceStartup;
        this.originalPos = this.transform.localPosition;
        this.originalRot = this.transform.localRotation;

        this.playing = true;
        this.startTimeReached = false;
    }

    public void Rewind()
    {
        this.playing = false;
        this.transform.localPosition = this.originalPos;
        this.transform.localRotation = this.originalRot;

        if (this.loop)
        {
            Play();
        }
    }

    public void ClearEventHandlers()
    {
        this.OnStartEvent = null;
        this.OnCompleteEvent = null;
    }

    #endregion // PUBLIC_METHODS
}
