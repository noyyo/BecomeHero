using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemCell : MonoBehaviour
{
    public int CellIndex;
    private Item _item;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = _item.Icon;
    }

    private void OnDoubleClicked()
    {
        switch (_item)
        {
            case EquipmentItem:
                // Equip하는 로직이 필요함.
                break;
            case UsableItem:
                break;
        }
    }
}
