using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    #region Variables
    private Item m_item;
    private int m_item_count;

    [Header("슬롯 타입 마스크")]
    [SerializeField] protected ItemType m_slot_mask;

    [Space(50f)]
    [Header("아이템 슬롯 UI 컴포넌트")]
    [Header("아이템 이미지")]
    [SerializeField] private Image m_item_image;

    [Header("아이템 개수 프레임")]
    [SerializeField] private GameObject m_item_count_frame;

    [Header("아이템 개수 라벨")]
    [SerializeField] private TMP_Text m_item_count_label;

    [Header("아이템 쿨타임 이미지")]
    [SerializeField] private Image m_cooldown_image;

    private ItemActionManager m_item_action_manager;
    private ItemTooltip m_tooltip;
    #endregion Variables

    #region Properties
    public Item Item
    {
        get => m_item;
        set => m_item = value;
    }

    public int Count
    {
        get => m_item_count;
        set => m_item_count = value;
    }

    public ItemType Mask
    {
        get => m_slot_mask;
    }
    #endregion Properties

    private void Update()
    {
        if (!m_item)
        {
            return;
        }

        m_cooldown_image.fillAmount = ItemCoolManager.Instance.GetTime(m_item.ID) / m_item.Cooltime;
    }

    #region Helper Methods
    private void SetAlpha(float alpha)
    {
        Color color = m_item_image.color;
        color.a = alpha;
        m_item_image.color = color;
    }

    public bool IsMask(Item item)
    {
        return ((int)item.Type & (int)m_slot_mask) == 0 ? false : true;
    }

    public void Add(Item item, int count = 1)
    {
        m_item = item;
        m_item_count = count;

        m_item_image.sprite = m_item.Sprite;
        SetAlpha(1f);

        if (m_item.Stackable)
        {
            m_item_count_frame.SetActive(true);
            m_item_count_label.text = m_item_count.ToString();
        }
        else
        {
            m_item_count_label.text = "";
            m_item_count_frame.SetActive(false);
        }
        m_cooldown_image.gameObject.SetActive(true);
    }

    public void Updates(int count)
    {
        m_item_count += count;
        m_item_count_label.text = m_item_count.ToString();

        if (m_item_count <= 0)
        {
            Clear();
        }
    }

    public void Clear()
    {
        m_item_action_manager = FindFirstObjectByType<ItemActionManager>();
        m_tooltip = FindFirstObjectByType<ItemTooltip>();

        m_item = null;
        m_item_count = 0;
        SetAlpha(0f);

        m_item_image.sprite = null;
        m_item_count_label.text = "";
        m_item_count_frame.SetActive(false);
        m_cooldown_image.gameObject.SetActive(false);
    }

    private void ChangeSlot()
    {
        if (DragSlot.Instance.Slot.Item.Type < ItemType.Equipment_Helmet)
        {
            if (m_item != null && m_item.ID == DragSlot.Instance.Slot.Item.ID)
            {
                int changed_slot_count;
                if (DragSlot.Instance.Mode == DragMode.SHIFT)
                {
                    changed_slot_count = (int)(DragSlot.Instance.Slot.Count * 0.5f);
                    if (changed_slot_count == 0)
                    {
                        Updates(1);
                        DragSlot.Instance.Slot.Clear();

                        return;
                    }
                }
                else if (DragSlot.Instance.Mode == DragMode.CTRL)
                {
                    changed_slot_count = 1;
                    if (DragSlot.Instance.Slot.Count == 1)
                    {
                        Updates(1);
                        DragSlot.Instance.Slot.Clear();

                        return;
                    }
                }
                else
                {
                    changed_slot_count = DragSlot.Instance.Slot.Count;
                }

                Updates(changed_slot_count);
                DragSlot.Instance.Slot.Updates(-changed_slot_count);

                return;
            }
        }

        if (DragSlot.Instance.Mode == DragMode.SHIFT)
        {
            int changed_slot_count = (int)(DragSlot.Instance.Slot.Count * 0.5f);

            if (changed_slot_count == 0)
            {
                Add(DragSlot.Instance.Slot.Item, 1);
                DragSlot.Instance.Slot.Clear();

                return;
            }

            Add(DragSlot.Instance.Slot.Item, changed_slot_count);
            DragSlot.Instance.Slot.Updates(-changed_slot_count);

            return;
        }

        if (DragSlot.Instance.Mode == DragMode.CTRL)
        {
            Add(DragSlot.Instance.Slot.Item, 1);
            if (DragSlot.Instance.Slot.Count == 1)
            {
                DragSlot.Instance.Slot.Clear();
            }
            else
            {
                DragSlot.Instance.Slot.Updates(-1);
            }

            return;
        }

        Item temp_item = m_item;
        int temp_item_count = m_item_count;

        Add(DragSlot.Instance.Slot.Item, DragSlot.Instance.Slot.Count);

        if (temp_item != null)
        {
            DragSlot.Instance.Slot.Add(temp_item, temp_item_count);
        }
        else
        {
            DragSlot.Instance.Slot.Clear();
        }
    }

    public void UseItem()
    {
        if (!m_item)
        {
            return;
        }

        if (ItemCoolManager.Instance.GetTime(m_item.ID) > 0f)
        {
            return;
        }

        if (!m_item_action_manager.UseItem(m_item, this))
        {
            return;
        }

        if (m_item.Cooltime > 0f)
        {
            ItemCoolManager.Instance.Enqueue(m_item.ID, m_item.Cooltime);
        }

        if (m_item != null && m_item.Type == ItemType.Consumable)
        {
            Updates(-1);
        }
    }
    #endregion Helper Methods

    #region Event Methods
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_item)
        {
            return;
        }

        m_tooltip.OpenUI(m_item.ID);

        if (CursorManager.Instance.Current != CursorMode.GRAB)
        {
            CursorManager.Instance.SetCursor(CursorMode.CAN_GRAB);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tooltip.CloseUI();

        CursorManager.Instance.SetCursor(CursorMode.DEFAULT);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!m_item)
        {
            return;
        }

        m_tooltip.CloseUI();

        (DragSlot.Instance.transform as RectTransform).SetAsLastSibling();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            DragSlot.Instance.Mode = DragMode.SHIFT;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            DragSlot.Instance.Mode = DragMode.CTRL;
        }
        else
        {
            DragSlot.Instance.Mode = DragMode.DEFAULT;
        }

        DragSlot.Instance.PickSlot(this);
        DragSlot.Instance.transform.position = eventData.position;

        CursorManager.Instance.SetCursor(CursorMode.GRAB);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!m_item)
        {
            return;
        }

        DragSlot.Instance.transform.position = eventData.position;

        CursorManager.Instance.SetCursor(CursorMode.GRAB);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.DropSlot();

        CursorManager.Instance.SetCursor(CursorMode.CAN_GRAB);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.Instance.Slot.Item == null)
        {
            return;
        }

        if (DragSlot.Instance.Mode == DragMode.SHIFT || DragSlot.Instance.Mode == DragMode.CTRL)
        {
            if (m_item != null && m_item.ID != DragSlot.Instance.Slot.Item.ID)
            {
                return;
            }
        }

        if (!IsMask(DragSlot.Instance.Slot.Item))
        {
            return;
        }

        if (m_item != null && !DragSlot.Instance.Slot.IsMask(m_item))
        {
            return;
        }

        ChangeSlot();
        m_item_action_manager.SlotOnDropEvent(DragSlot.Instance.Slot, this);
        m_tooltip.OpenUI(m_item.ID);

        CursorManager.Instance.SetCursor(CursorMode.DEFAULT);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
    #endregion Event Methods
}