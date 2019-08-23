/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;

public class MT360TrackableEventHandler : DefaultTrackableEventHandler
{
    #region PRIVATE_MEMBERS
    
    private ModelTargetsManager modelTargetsManager;
    
    #endregion // PRIVATE_MEMBERS
    
    
    #region MONOBEHAVIOUR_METHODS
    
    protected override void Start()
    {
        base.Start();

        this.modelTargetsManager = FindObjectOfType<ModelTargetsManager>();
    }
    
    #endregion // MONOBEHAVIOUR_METHODS

    
    #region PROTECTED_METHODS
    
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        
        // We are tracking a target, so we hide the symbolic targets UI
        if (this.modelTargetsManager)
        {
            this.modelTargetsManager.EnableSymbolicTargetsUI(false);
        }
    }
    
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        // Target tracking lost, so we show the symbolic targets UI
        if (this.modelTargetsManager)
        {
            this.modelTargetsManager.EnableSymbolicTargetsUI(true);
        }
    }
    
    #endregion // PROTECTED_METHODS
}
