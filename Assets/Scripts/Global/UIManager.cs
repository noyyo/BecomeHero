using System;
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
    public Dictionary<UIType, UIBase> UIDictionary = new Dictionary<UIType, UIBase>();

    private const string UIPrefabFolderPath = "Prefabs/UI/";
    private const string UIPrefabPrefix = "UI";
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RoadUI();
    }
    private void RoadUI()
    {
        string path;
        UIType type;
        for (int i = 0; i < Enum.GetValues(typeof(UIType)).Length; i++)
        {
            type = (UIType)i;
            path = UIPrefabFolderPath + UIPrefabPrefix + type.ToString();
            UIBase UI = Resources.Load<UIBase>(path);
            if (UI == null)
            {
                continue;
            }
            UI = Instantiate(UI);
            UI.gameObject.SetActive(false);
            UI.InitializeUI();
            UIDictionary.Add(type, UI);
        }
    }

    public void OpenUI(UIType uiType)
    {
        UIBase UIWindow = UIDictionary[uiType];
        UIWindow.OpenUI();
    }
    public void CloseUI(UIType uiType)
    {
        UIBase UIWIndow = UIDictionary[uiType];
        UIWIndow.CloseUI();
    }
    public bool IsOpenedUI(UIType uiType)
    {
        return UIDictionary[uiType].isActiveAndEnabled;
    }



}
