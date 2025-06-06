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
        if (GameManager.Instance.Current == GameEventType.PLAYING || GameManager.Instance.Current == GameEventType.CHECKING)
        {
            ToggleUI();
        }
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

        GameEventBus.Publish(GameEventType.CHECKING);
    }

    public void CloseUI()
    {
        m_is_active = false;

        m_inventory_animator.SetBool("Open", false);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    private void Initialize()
    {
        ClearSlots();
        LoadData();
        ClearMoney();
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
        m_money_label.text = DataManager.Instance.PlayerData.Data.Money.ToString();
    }

    private void LoadData()
    {
        AquireMoney(DataManager.Instance.PlayerData.Data.Money);

        var inventory = DataManager.Instance.PlayerData.Data.Inventory;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].ID != (int)ItemCode.EMPTY)
            {
                m_slots[i].Add(DataManager.Instance.ItemData.GetItem(inventory[i].ID), inventory[i].Count);
            }
            else
            {
                m_slots[i].Clear();
            }
        }
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
        DataManager.Instance.PlayerData.Data.Money += amount;
        m_money_label.text = NumberFormatter.FormatNumber(DataManager.Instance.PlayerData.Data.Money);
    }
    #endregion Helper Methods
}
