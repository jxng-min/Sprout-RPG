using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sale
{
    public Item Item;
    public int Cost;
    public int Constraint;
}

[RequireComponent(typeof(Animator))]
public class Shop : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    [Header("슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("상점 슬롯 프리펩")]
    [SerializeField] private ShopSlot m_slot;

    [Header("가능한 품목만 추려낼 토글")]
    [SerializeField] private Toggle m_pickout_toggle;

    private List<ShopSlot> m_slots;
    private Animator m_shop_animator;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    #endregion Properties

    private void Awake()
    {
        m_shop_animator = GetComponent<Animator>();
        m_slots = new();
    }

    #region Helper Methods
    public void OpenUI(List<Sale> sale_list)
    {
        Initialize(sale_list);

        m_is_active = true;
        m_shop_animator.SetBool("Open", true);
    }

    public void Button_CloseUI()
    {
        m_is_active = false;
        m_shop_animator.SetBool("Open", false);

        Invoke("ReturnSlots", 0.5f);
    }

    public void Toggle_PickOut()
    {
        if (!m_pickout_toggle.isOn)
        {
            for (int i = 0; i < m_slots.Count; i++)
            {
                m_slots[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < m_slots.Count; i++)
            {
                if (!m_slots[i].CanPurchase)
                {
                    m_slots[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void ReturnSlots()
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].transform.SetParent(ObjectManager.Instance.GetPool(ObjectType.SHOPSLOT).Container, false);
            ObjectManager.Instance.ReturnObject(m_slots[i].gameObject, ObjectType.SHOPSLOT);
        }
    }

    private void Instantiate(List<Sale> sale_list)
    {
        for (int i = 0; i < sale_list.Count; i++)
        {
            var slot_obj = ObjectManager.Instance.GetObject(ObjectType.SHOPSLOT);
            slot_obj.transform.SetParent(m_slot_root, false);

            var slot = slot_obj.GetComponent<ShopSlot>();
            m_slots.Add(slot);
        }
    }

    public void Initialize(List<Sale> sale_list)
    {
        m_slots.Clear();
        
        Instantiate(sale_list);

        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].Initialize(sale_list[i]);
        }
    }

    public void Updates()
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].Updates();
        }
    }
    #endregion Helper Methods
}
