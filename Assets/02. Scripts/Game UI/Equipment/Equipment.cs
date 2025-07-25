using TMPro;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    [Header("플레이어의 컨트롤러")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    [Space(50)]
    [Header("UI 관련 컴포넌트")]
    [Header("장비 슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("장비 UI의 애니메이터")]
    [SerializeField] private Animator m_equipment_animator;

    [Header("체력 라벨")]
    [SerializeField] private TMP_Text m_hp_label;

    [Header("마나 라벨")]
    [SerializeField] private TMP_Text m_mp_label;

    [Header("공격력 라벨")]
    [SerializeField] private TMP_Text m_atk_label;

    [Header("이동속도 라벨")]
    [SerializeField] private TMP_Text m_spd_label;

    private ItemSlot[] m_slots;

    private EquipmentEffect m_current_equipment_effect;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    public EquipmentEffect Effect { get => m_current_equipment_effect; }
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
    private void Initialize()
    {
        ClearSlots();
        LoadData();
    }

    private void ClearSlots()
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].Clear();
        }
    }

    private void LoadData()
    {
        var equipment = DataManager.Instance.PlayerData.Data.Equipment;
        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i].ID != (int)ItemCode.EMPTY)
            {
                m_slots[i].Add(DataManager.Instance.ItemData.GetItem(equipment[i].ID));
            }
            else
            {
                m_slots[i].Clear();
            }
        }

        Calculation();
    }

    private void ToggleUI()
    {
        if (Input.GetKeyDown(KeyCode.U))
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

        m_equipment_animator.SetBool("Open", true);

        GameEventBus.Publish(GameEventType.CHECKING);
    }

    public void CloseUI()
    {
        m_is_active = false;

        m_equipment_animator.SetBool("Open", false);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    public ItemSlot GetSlot(ItemType type)
    {
        switch (type)
        {
            case ItemType.Equipment_Helmet:
                return m_slots[0];

            case ItemType.Equipment_Armor:
                return m_slots[1];

            case ItemType.Equipment_Weapon:
                return m_slots[2];

            case ItemType.Equipment_Shield:
                return m_slots[3];
        }

        return null;
    }

    public void Calculation()
    {
        var calculated_effect = new EquipmentEffect();

        foreach (var slot in m_slots)
        {
            if (!slot.Item)
            {
                continue;
            }

            calculated_effect += (slot.Item as EquipmentItem).Effect;
        }

        m_current_equipment_effect = calculated_effect;
        UpdateUI();
        UpdatePlayer();

        m_player_ctrl.Attacking.SwapWeapon(GetSlot(ItemType.Equipment_Weapon).Item as WeaponItem);
    }

    private void UpdateUI()
    {
        m_hp_label.text = NumberFormatter.FormatNumber(DataManager.Instance.PlayerData.Data.Status.MaxHP + m_current_equipment_effect.HP);
        m_mp_label.text = NumberFormatter.FormatNumber(DataManager.Instance.PlayerData.Data.Status.MaxMP + m_current_equipment_effect.MP);
        m_atk_label.text = NumberFormatter.FormatNumber(DataManager.Instance.PlayerData.Data.Status.ATK + m_current_equipment_effect.ATK);
        m_spd_label.text = NumberFormatter.FormatNumber(DataManager.Instance.PlayerData.Data.Status.SPD + m_current_equipment_effect.SPD);
    }

    private void UpdatePlayer()
    {
        m_player_ctrl.Movement.UpdateSPD();
        m_player_ctrl.Attacking.UpdateATK();
        m_player_ctrl.Health.UpdateMaxHPMP();
    }
    #endregion Helper Methods
}
