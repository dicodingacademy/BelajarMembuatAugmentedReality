/*===============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections.Generic;

public class AboutScreenInfo
{
    #region PRIVATE_MEMBERS

    readonly Dictionary<string, string> titles;
    readonly Dictionary<string, string> descriptions;

    #endregion // PRIVATE_MEMBERS


    #region PUBLIC_METHODS

    public string GetTitle(string titleKey)
    {
        return GetValuefromDictionary(this.titles, titleKey);
    }

    public string GetDescription(string descriptionKey)
    {
        return GetValuefromDictionary(this.descriptions, descriptionKey);
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    string GetValuefromDictionary(Dictionary<string, string> dictionary, string key)
    {
        if (dictionary.ContainsKey(key))
        {
            string value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        return "Key not found.";
    }

    #endregion // PRIVATE_METHODS


    #region CONSTRUCTOR

    public AboutScreenInfo()
    {

        // Init our Title Strings

        this.titles = new Dictionary<string, string>()
        {
            { "ImageTargets", "Image Targets" },
            { "VuMarks", "VuMarks" },
            { "CylinderTargets", "Cylinder Targets" },
            { "MultiTargets", "Multi Targets" },
            { "UserDefinedTargets", "User Defined Targets" },
            { "ObjectReco", "Object Reco" },
            { "CloudReco", "Cloud Reco" },
            { "VirtualButtons", "Virtual Buttons" },
            { "ModelTargets", "Model Targets" },
            { "GroundPlane", "Ground Plane" },
            { "BackgroundTextureAccess", "Background Texture Access" },
            { "OcclusionManagement", "Occlusion Management" },
            { "Books", "Books" },
            { "ARVR", "ARVR" }
        };

        // Init our Common Cache Strings

        string vuforiaVersion = Vuforia.VuforiaUnity.GetVuforiaLibraryVersion();
        string unityVersion = UnityEngine.Application.unityVersion;
        UnityEngine.Debug.Log("Vuforia Engine " + vuforiaVersion + "\nUnity " + unityVersion);

        string vuforia = Vuforia.VuforiaRuntime.Instance.InitializationState != Vuforia.VuforiaRuntime.InitState.NOT_INITIALIZED
                                ? "<#23B200>Yes</color>"
                                : "<color=red>No</color>";

        string description = "\n<size=26>Description:</size>";
        string keyFunctionality = "<size=26>Key Functionality:</size>";
        string targets = "<size=26>Targets:</size>";
        string instructions = "<size=26>Instructions:</size>";
        string footer =
            "<size=26>Build Version Info:</size>" +
            "\n• Vuforia Engine " + vuforiaVersion +
            "\n• Unity " + unityVersion +
            "\n" +
            "\n<size=26>Project Settings Info:</size>" +
            "\n• Vuforia Engine Enabled: " + vuforia +
            "\n" +
            "\n<size=26>Links:</size>" +
            "\n• <link=https://developer.vuforia.com/legal/vuforia-developer-agreement><color=blue><u>Developer Agreement</u></color></link>" +
            "\n• <link=https://developer.vuforia.com/legal/privacy><color=blue><u>Privacy Policy</u></color></link>" +
            "\n• <link=https://developer.vuforia.com/legal/EULA><color=blue><u>Terms of Use</u></color></link>" +
            "\n• <link=https://developer.vuforia.com/legal/statistics><color=blue><u>Statistics</u></color></link>" +
            "\n\n" +
            "© 2019 PTC Inc. All Rights Reserved." +
            "\n";
        string targetPDFsURL = "<link=https://library.vuforia.com/content/vuforia-library/en/articles/Solution/sample-apps-target-pdfs.html>";

        // Init our Description Strings

        this.descriptions = new Dictionary<string, string>();

        // Image Targets

        this.descriptions.Add(
            "ImageTargets",
            description +
            "\nThe Image Targets sample shows how to detect an image " +
            "target and render a simple 3D object on top of it." +
            "\n\n" +
            keyFunctionality +
            "\n• Simultaneous detection and tracking of multiple targets" +
            "\n• Load and activate multiple device databases" +
            "\n• Activate Extended Tracking" +
            "\n• Manage camera functions: flash and continuous autofocus" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // VuMark

        this.descriptions.Add(
            "VuMarks",
            description +
            "\nThe VuMarks sample shows how to detect and track VuMarks." +
            "\n\n" +
            keyFunctionality +
            "\n• Simultaneous detection and tracking of multiple VuMarks" +
            "\n• Load and activate a VuMark target" +
            "\n• Activate Extended Tracking" +
            "\n• Render an outline when a VuMark is detected" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point device at VuMark" +
            "\n• Double tap to focus" +
            "\n• Tap window showing VuMark ID to dismiss" +
            "\n\n" +
            footer + "\n");


        // Cylinder Targets

        this.descriptions.Add(
            "CylinderTargets",
            description +
            "\nThe Cylinder Targets sample shows how to detect a cylindrical " +
            "target and animate a 3D object around the circumference of the cylinder." +
            "\n\n" +
            keyFunctionality +
            "\n• Detection and tracking of a cylinder target" +
            "\n• Occlusion handling" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            "Print target and wrap around a standard soda can." +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Multi Targets

        this.descriptions.Add(
            "MultiTargets",
            description +
            "\nThe Multi Targets sample shows how to detect a simple cuboid 3D shape " +
            "and animate a 3D object around the shape." +
            "\n\n" +
            keyFunctionality +
            "\n• Detection and tracking of cuboid 3D shape" +
            "\n• Occlusion handling" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // User Defined Targets

        this.descriptions.Add(
            "UserDefinedTargets",
            description +
            "\nThe User Defined Targets sample shows how to capture and create an " +
            "image target at runtime from user-selected camera video frames." +
            "\n\n" +
            keyFunctionality +
            "\n• Create and manage user defined image target" +
            "\n• Activate Extended Tracking" +
            "\n\n" +
            instructions +
            "\n• Hold device parallel to feature rich target and tap camera button" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Object Reco

        this.descriptions.Add(
            "ObjectReco",
            description +
            "\nThe Object Recognition sample shows how to recognize and track an object." +
            "\n\n" +
            keyFunctionality +
            "\n• Recognize and track up to 2 objects simultaneously" +
            "\n• Activate Extended Tracking" +
            "\n• Manage camera functions: flash" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Cloud Reco

        this.descriptions.Add(
            "CloudReco",
            description +
            "\nThe Cloud Reco sample shows how to use the cloud recognition service to " +
            "recognize targets located in a cloud database." +
            "\n\n" +
            keyFunctionality +
            "\n• Manage detection and tracking of cloud based image targets" +
            "\n• Activate Extended Tracking" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Virtual Buttons

        this.descriptions.Add(
            "VirtualButtons",
            description +
            "\nThe Virtual Buttons sample shows how the developer can define rectangular " +
            "regions on image targets that trigger an event when touched or occluded in " +
            "the camera view. The sample renders a 3D object that changes color when " +
            "one of the virtual buttons is triggered." +
            "\n\n" +
            keyFunctionality +
            "\n• Button occlusion event handling" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>Target PDFs</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Model Targets

        this.descriptions.Add(
            "ModelTargets",
            description +
            "\nThe Model Targets Sample shows how to detect a 3D object and " +
            "render a simple 3D representation over it. The sample demonstrates " +
            "how Standard, Advanced, and 360 DataSets work." +
            "\n\n" +
            keyFunctionality +
            "\n• Standard Model Target: Loads a Model Target with multiple Guide Views " +
            "that can be cycled with the click of a button and used to pick which view " +
            "to detect of the physical model" +
            "\n• Advanced Model Target: Loads two Model Targets that have automatic " +
            "detection and snapping of Guide Views to the physical models" +
            "\n• Model Target 360: Loads a Model Target that can be detected from arbitrary views" +
            "\n• Automatic 3D object tracking after successful detection" +
            "\n• Extended Tracking when target is not visible in the camera view" +
            "\n\n" +
            targets +
            "\n• Model Target: 3D Printed Model (Mars Lander)" +
            "\n• Model Target: Toy Model (Bike)" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n• Switch to access a different dataset" +
            "\n• Mars Lander is in all three datasets" +
            "\n• Motorcycle is in the Advanced dataset" +
            "\n• Change the Detection Position if needed" +
            "\n\n" +
            footer + "\n");


        // Ground Plane

        this.descriptions.Add(
            "GroundPlane",
            description +
            "\nThe Ground Plane sample demonstrates how to place " +
            "content on surfaces and in mid-air using anchor points." +
            "\n\n" +
            keyFunctionality +
            "\n• Hit testing places the Chair or the Astronaut on an intersecting plane in " +
            "the environment. Try this mode by pressing the Chair or Astronaut buttons." +
            "\n• Mid-Air anchoring places the drone on an anchor point created " +
            "at a fixed distance relative to the user. Select this mode by " +
            "pressing the Drone button." +
            "\n\n" +
            targets +
            "\n• None required" +
            "\n\n" +
            instructions +
            "\n• Launch the app and view your environment" +
            "\n• Look around until the indicator shows that you have found a surface" +
            "\n• Tap to place Chair on the ground" +
            "\n• Drag with one finger to move Chair along ground" +
            "\n• Touch and hold with two fingers to rotate Chair" +
            "\n• Select Ground Plane mode" +
            "\n• Tap to place Astronaut on the ground" +
            "\n• Tap again to move Astronaut to second point" +
            "\n• Select Mid-Air mode" +
            "\n• Tap to place Drone in the air" +
            "\n• Tap again to move Drone to the desired position" +
            "\n\n" +
            footer + "\n");


        // Background Texture Access

        this.descriptions.Add(
            "BackgroundTextureAccess",
            description +
            "\nThe Background Texture Access sample shows how to use two shaders to " +
            "manipulate the background video. One shader turns the video into inverted " +
            "black-and-white and another distorts the video where you touch on the screen." +
            "\n\n" +
            keyFunctionality +
            "\n• Apply shaders to video background" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>ImageTarget: Fissure</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Tap and drag to distort video background" +
            "\n\n" +
            footer + "\n");


        // Occlusion Management

        this.descriptions.Add(
            "OcclusionManagement",
            description +
            "\nThe Occlusion Management sample shows the use of transparent shaders to " +
            "let users partially look inside a real object with an occlusion effect." +
            "\n\n" +
            keyFunctionality +
            "\n• Manage occlusion" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>MultiTarget: MarsBox</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Double tap to focus" +
            "\n\n" +
            footer + "\n");


        // Books

        this.descriptions.Add(
            "Books",
            description +
            "\nThe Books sample shows how to use the Cloud Recognition service to build a " +
            "simple UI to scan a sample target book cover and display info on that book." +
            "\n\n" +
            keyFunctionality +
            "\n• Display reco query status" +
            "\n• Request book info meta data based on reco response" +
            "\n• Render book info on target" +
            "\n• Transition display of book info to screen when off target" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>ImageTargets: 3 Book Covers</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at sample book cover to view info" +
            "\n• Press close button to scan another book" +
            "\n\n" +
            footer + "\n");


        // ARVR

        this.descriptions.Add(
            "ARVR",
            description +
            "\nThis sample demonstrates a mixed reality experience that starts in AR and moves to VR." +
            "\n\n" +
            keyFunctionality +
            "\n• Transition between AR camera tracking and VR device tracking" +
            "\n\n" +
            targets +
            "\n• " + targetPDFsURL +
            "<color=blue><u>ImageTarget: Astronaut</u></color></link>" +
            "\n\n" +
            instructions +
            "\n• Point camera at target to view" +
            "\n• Aim the cursor at the button (labeled “VR”) to trigger the transition to VR" +
            "\n• In VR mode, look at the button on the floor to return to AR" +
            "\n\n" +
            footer + "\n");
    }

    #endregion // CONSTRUCTOR

}
