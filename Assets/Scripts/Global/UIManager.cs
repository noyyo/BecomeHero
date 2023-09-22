using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    InGameHUD,
    PlayerStatus,
    Inventory,
    Store,
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Dictionary<UIType, GameObject> UIDictionary = new Dictionary<UIType, GameObject>();
    private void Awake()
    {
        Instance = this;
    }

    public void OpenUI(UIType uiType)
    {
        GameObject UIWindow = UIDictionary[uiType];
        UIWindow.SetActive(true);
    }
    public void CloseUI(UIType uiType)
    {
        GameObject UIWIndow = UIDictionary[uiType];
        UIWIndow.SetActive(false);
    }



}
