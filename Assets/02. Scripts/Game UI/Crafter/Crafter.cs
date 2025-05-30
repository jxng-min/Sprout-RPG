using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Crafter : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    [Header("제작 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("제작 가능 토글")]
    [SerializeField] private Toggle m_craft_toggle;

    private Animator m_animator;
    private List<CraftingSlot> m_slots;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    #endregion Properties

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_slots = new();
    }

    #region Helper Methods
    public void OpenUI(List<Receipe> receipes)
    {
        Initialize(receipes);

        m_is_active = true;
        m_animator.SetBool("Open", true);

        GameEventBus.Publish(GameEventType.CHECKING);
    }

    public void Button_CloseUI()
    {
        m_is_active = false;
        m_animator.SetBool("Open", false);

        Invoke("Return", 0.5f);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    public void Toggle_PickOut()
    {
        if (!m_craft_toggle.isOn)
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
                if (!m_slots[i].CanCraft)
                {
                    m_slots[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void Initialize(List<Receipe> receipes)
    {
        Instantiate(receipes);

        for (int i = 0; i < receipes.Count; i++)
        {
            m_slots[i].Initialize(receipes[i]);
        }

        Updates();
    }

    private void Instantiate(List<Receipe> receipes)
    {
        for (int i = 0; i < receipes.Count; i++)
        {
            var slot_obj = ObjectManager.Instance.GetObject(ObjectType.CRAFTINGSLOT);
            slot_obj.transform.SetParent(m_slot_root, false);

            var slot = slot_obj.GetComponent<CraftingSlot>();
            m_slots.Add(slot);
        }
    }

    public void Updates()
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].Updates();
        }
    }

    private void Return()
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].Return();

            m_slots[i].transform.SetParent(ObjectManager.Instance.GetPool(ObjectType.CRAFTINGSLOT).Container, false);
            ObjectManager.Instance.ReturnObject(m_slots[i].gameObject, ObjectType.CRAFTINGSLOT);
        }

        m_slots.Clear();
    }
    #endregion Helper Methods
}
