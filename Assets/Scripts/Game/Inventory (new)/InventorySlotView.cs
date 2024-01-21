using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image _slotIcon;

    private int _slotIndex;

    public UnityAction<int> SlotClicked;

    public void SetItemIcon(Sprite sprite) // включаем и задаем иконку предмета
    {
        _slotIcon.enabled = true;
        _slotIcon.sprite = sprite;
    }

    public void ClearItemIcon()
    {
        _slotIcon.enabled = false;
        _slotIcon.sprite = null;
    }

    public void ClickSlot() // повесил на кнопку
    {
        SlotClicked?.Invoke(_slotIndex);
    }

    public void SetSlotIndex(int slotIndex)
    {
        _slotIndex = slotIndex;
    }

    private void Reset()
    {
        _slotIcon = transform.GetChild(0).GetComponent<Image>();
    }
}
