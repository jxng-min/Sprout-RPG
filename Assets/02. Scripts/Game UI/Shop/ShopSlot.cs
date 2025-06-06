using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    #region Variables
    private Inventory m_inventory;

    [Header("UI 관련 컴포넌트")]
    [Header("아이템 슬롯")]
    [SerializeField] private ItemSlot m_item_slot;

    [Header("아이템 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("아이템 가격 라벨")]
    [SerializeField] private TMP_Text m_cost_label;

    [Header("아이템 구매 버튼")]
    [SerializeField] private Button m_purchase_button;

    [Header("비활성화 이미지")]
    [SerializeField] private GameObject m_prohibition_image;

    private int m_cost;
    private int m_constraint_level;
    private bool m_can_purchase;
    #endregion Variables

    #region Properties
    public bool CanPurchase { get => m_can_purchase; }
    #endregion Properties

    private void Awake()
    {
        m_inventory = FindFirstObjectByType<Inventory>();
    }

    #region Helper Methods
    public void Initialize(Sale item)
    {
        m_item_slot.Clear();
        m_item_slot.Add(item.Item, 1, true);
        m_cost = item.Cost;
        m_constraint_level = item.Constraint;

        Updates();
    }

    public void Updates()
    {
        m_name_label.text = DataManager.Instance.ItemData.GetName(m_item_slot.Item.ID);
        m_cost_label.text = NumberFormatter.FormatNumber(m_cost);

        if (DataManager.Instance.PlayerData.Data.LV >= m_constraint_level)
        {
            m_can_purchase = true;
            m_purchase_button.interactable = true;
            m_prohibition_image.SetActive(false);
        }
        else
        {
            m_can_purchase = false;
            m_purchase_button.interactable = false;
            m_prohibition_image.SetActive(true);

            return;
        }

        if (DataManager.Instance.PlayerData.Data.Money < m_cost)
        {
            m_can_purchase = false;
            m_cost_label.text = $"<color=red>{m_cost_label.text}</color>";
            m_purchase_button.interactable = false;
        }
    }

    public void Button_Purchase()
    {
        DataManager.Instance.PlayerData.Data.Money -= m_cost;
        m_inventory.AquireItem(m_item_slot.Item);

        FindFirstObjectByType<Shop>().Updates();
    }
    #endregion Helper Methods
}
