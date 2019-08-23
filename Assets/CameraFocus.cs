using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraFocus : MonoBehaviour {
    void Start() {    
        var vuforia = VuforiaARController.Instance;    
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);    
        vuforia.RegisterOnPauseCallback(OnPaused);
    }  

    private void OnVuforiaStarted() {    
        CameraDevice.Instance.SetFocusMode(
        CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused) {    
        if (!paused) {
            CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);    
        }
    }
}