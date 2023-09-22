using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase: MonoBehaviour
{
    public abstract void UpdateUI();
    public abstract void OpenUI();
    public abstract void CloseUI();
}
