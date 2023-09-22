using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    private Inventory _playerInventory;
    private List<UIItemCell> _itemCells = new List<UIItemCell>();

    private void Start()
    {
        _playerInventory = GameManager.Instance.Player.GetComponent<Inventory>();
    }

    public override void UpdateUI()
    {

    }
    public override void OpenUI()
    {
        UpdateUI();
        gameObject.SetActive(true);
    }
    public override void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
