using UnityEngine;
using UnityEngine.UI;

public enum DragMode
{
    DEFAULT = 0,
    SHIFT = 1,
    CTRL = 2,
}

public class DragSlot : Singleton<DragSlot>
{
    #region Variables
    private ItemSlot m_current_slot;
    private DragMode m_current_mode;

    [Header("드래그 슬롯의 이미지")]
    [SerializeField] private Image m_item_image;
    #endregion Variables

    #region Properties
    public ItemSlot Slot
    {
        get => m_current_slot;
        set => m_current_slot = value;
    }

    public DragMode Mode
    {
        get => m_current_mode;
        set => m_current_mode = value;
    }
    #endregion Properties

    #region Helper Methods
    private void SetAlpha(float alpha)
    {
        var color = m_item_image.color;
        color.a = alpha;
        m_item_image.color = color;
    }

    public void PickSlot(ItemSlot slot)
    {
        m_current_slot = slot;
        m_item_image.sprite = m_current_slot.Item.Sprite;
        SetAlpha(1f);
    }

    public void DropSlot()
    {
        m_current_slot = null;
        m_item_image.sprite = null;
        SetAlpha(0f);
    }
    #endregion Helper Methods
}
