using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    [Header("슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("인벤토리의 애니메이터")]
    [SerializeField] private Animator m_inventory_animator;

    [Header("돈을 표시할 라벨")]
    [SerializeField] private TMP_Text m_money_label;

    private ItemSlot[] m_slots;

    private int m_money;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    public ItemSlot[] Slots { get => m_slots; }
    #endregion Properties

    private void Awake()
    {
        m_slots = m_slot_root.GetComponentsInChildren<ItemSlot>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ToggleUI();
    }

    #region Helper Methods
    private void ToggleUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (m_is_active)
            {
                CloseUI();
            }
            else
            {
                OpenUI();
            }
        }
    }

    private void OpenUI()
    {
        m_is_active = true;

        m_inventory_animator.SetBool("Open", true);
    }

    public void CloseUI()
    {
        m_is_active = false;

        m_inventory_animator.SetBool("Open", false);
    }

    private void Initialize()
    {
        ClearSlots();
        ClearMoney();
        LoadData();
    }

    private void ClearSlots()
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].Clear();
        }
    }

    private void ClearMoney()
    {
        m_money = 0;
        m_money_label.text = m_money.ToString();
    }

    private void LoadData()
    {
        var inventory = DataManager.Instance.Data.Inventory;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].ID != (int)ItemCode.EMPTY)
            {
                m_slots[i].Add(ItemDataManager.Instance.GetItem(inventory[i].ID), inventory[i].Count);
            }
            else
            {
                m_slots[i].Clear();
            }
        }
    }

    private void LoadItem(Item item, ItemSlot slot, int count = 1)
    {
        slot.Add(item, count);
    }

    public void AquireItem(Item item, int count = 1)
    {
        if (item.Stackable)
        {
            for (int i = 0; i < m_slots.Length; i++)
            {
                if (m_slots[i].Item != null && m_slots[i].IsMask(item))
                {
                    if (m_slots[i].Item.ID == item.ID)
                    {
                        if (m_slots[i].Count >= 99)
                        {
                            continue;
                        }

                        m_slots[i].Updates(count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < m_slots.Length; i++)
        {
            if (m_slots[i].Item == null && m_slots[i].IsMask(item))
            {
                m_slots[i].Add(item, count);
                return;
            }
        }
    }

    public void AquireItem(Item item, ItemSlot target_slot, int count = 1)
    {
        target_slot.Add(item, count);
    }

    public ItemSlot GetValidSlot(Item item)
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            if (item.Stackable && m_slots[i].IsMask(item))
            {
                if (m_slots[i].Count < 99)
                {
                    return m_slots[i];
                }
            }

            if (m_slots[i].Item == null)
            {
                return m_slots[i];
            }
        }

        return null;
    }

    public void AquireMoney(int amount)
    {
        m_money += amount;
        m_money_label.text = m_money.ToString();
    }
    #endregion Helper Methods
}
