/*===============================================================================
Copyright (c) 2018 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;

public class SafeAreaManager : MonoBehaviour
{
    #region PRIVATE_MEMBERS

    [System.Serializable]
    class SafeAreaRect
    {
        public RectTransform rectTransform = null;
        public bool applyTopSafeArea = false;
        public bool applyBottomSafeArea = false;
    }

    [SerializeField] RectTransform topArea = null;
    [SerializeField] RectTransform bottomArea = null;
    [SerializeField] Color topAreaColor;
    [SerializeField] Color bottomAreaColor;
    [SerializeField] SafeAreaRect[] safeAreaRects = null;
    Rect lastSafeArea = new Rect(0, 0, 0, 0);
    ScreenOrientation lastOrientation;

    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    void Awake()
    {
        SetAreaColors();
    }

    private void Start()
    {
        this.lastOrientation = Screen.orientation;

        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS

    void Refresh()
    {
        if (Screen.safeArea != this.lastSafeArea)
        {
            ApplySafeArea();
            UpdateNonSafeArea();
        }

        if (Screen.orientation != this.lastOrientation)
        {
            ApplySafeArea();
            UpdateNonSafeArea();
        }
    }

    void ApplySafeArea()
    {
        this.lastSafeArea = Screen.safeArea;
        this.lastOrientation = Screen.orientation;

        foreach (SafeAreaRect areaRect in this.safeAreaRects)
        {
            Vector2 anchorMin = Screen.safeArea.position;
            Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y = areaRect.applyBottomSafeArea ? anchorMin.y / Screen.height : 0;
            anchorMax.x /= Screen.width;
            anchorMax.y = areaRect.applyTopSafeArea ? anchorMax.y / Screen.height : 1;

            areaRect.rectTransform.anchorMin = anchorMin;
            areaRect.rectTransform.anchorMax = anchorMax;
        }
    }

    void UpdateNonSafeArea()
    {
        Vector2 anchorMin = Screen.safeArea.position;
        Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y = anchorMin.y / Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y = anchorMax.y / Screen.height;

        SetNonSafeAreaSizes(anchorMin.y, anchorMax.y);
    }

    void SetNonSafeAreaSizes(float safeAreaAnchorMinY, float safeAreaAnchorMaxY)
    {
        this.topArea.anchorMin = new Vector2(0, safeAreaAnchorMaxY);
        this.topArea.anchorMax = Vector2.one;

        this.bottomArea.anchorMin = Vector2.zero;
        this.bottomArea.anchorMax = new Vector2(1, safeAreaAnchorMinY);
    }

    void SetAreaColors()
    {
        this.topArea.GetComponent<Image>().color = this.topAreaColor;
        this.bottomArea.GetComponent<Image>().color = this.bottomAreaColor;
    }

    #endregion // PRIVATE_METHODS


    #region PUBLIC_METHODS

    public void SetAreasEnabled(bool topAreaEnabled, bool bottomAreaEnabled)
    {
        this.topArea.gameObject.SetActive(topAreaEnabled);
        this.bottomArea.gameObject.SetActive(bottomAreaEnabled);
    }

    /// <summary>
    /// Sets the area colors programmatically and bypasses Inspector colors.
    /// </summary>
    /// <param name="topColor">Top color.</param>
    /// <param name="bottomColor">Bottom color.</param>
    public void SetAreaColors(Color topColor, Color bottomColor)
    {
        this.topAreaColor = topColor;
        this.bottomAreaColor = bottomColor;

        this.topArea.GetComponent<Image>().color = this.topAreaColor;
        this.bottomArea.GetComponent<Image>().color = this.bottomAreaColor;
    }

    #endregion // PUBLIC_METHODS
}
