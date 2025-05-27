using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    #endregion Properties

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
        m_tooltip = FindFirstObjectByType<ItemTooltip>();

        m_item = null;
        m_item_count = 0;
        SetAlpha(0f);

        m_item_image.sprite = null;
        m_item_count_label.text = "";
        m_item_count_frame.SetActive(false);
        m_cooldown_image.gameObject.SetActive(true);
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tooltip.CloseUI();
    }
    #endregion Event Methods
}