using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    #region Variables
    [Header("재료 아이템 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("아이템 슬롯 프리펩")]
    [SerializeField] private GameObject m_prefab;

    [Header("타겟 아이템 슬롯")]
    [SerializeField] private ItemSlot m_target_slot;

    [Header("제작 버튼")]
    [SerializeField] private Button m_crafting_button;

    [Header("비활성화 이미지")]
    [SerializeField] private GameObject m_prohibition_image;

    [Header("비활성화 라벨")]
    [SerializeField] private TMP_Text m_prohibition_label;

    private Receipe m_receipe;
    private List<ItemSlot> m_slots;
    private Inventory m_inventory;

    private bool m_can_craft;
    #endregion Variables

    #region Properties
    public bool CanCraft { get => m_can_craft; }
    #endregion Properties

    private void Awake()
    {
        m_slots = new();
    }

    #region Helper Methods
    public void Initialize(Receipe receipe)
    {
        m_inventory = FindFirstObjectByType<Inventory>();

        m_receipe = receipe;

        for (int i = 0; i < m_receipe.Ingredient.Count; i++)
        {
            var slot_obj = ObjectManager.Instance.GetObject(ObjectType.ITEMSLOT);
            slot_obj.transform.SetParent(m_slot_root, false);

            var slot = slot_obj.GetComponent<ItemSlot>();
            m_slots.Add(slot);
            slot.Clear();
            slot.Add(m_receipe.Ingredient[i].Item, m_receipe.Ingredient[i].Count, true);
        }

        m_target_slot.Clear();
        m_target_slot.Add(m_receipe.Target.Item, m_receipe.Target.Count, true);

        Updates();
    }

    public void Updates()
    {
        m_crafting_button.interactable = true;
        m_prohibition_image.SetActive(false);
        m_can_craft = true;

        for (int i = 0; i < m_receipe.Ingredient.Count; i++)
        {
            int total_count = 0;
            for (int j = 0; j < m_inventory.Slots.Length; j++)
            {
                if (m_inventory.Slots[j].Item == null)
                {
                    continue;
                }

                if (m_receipe.Ingredient[i].Item.ID == m_inventory.Slots[j].Item.ID)
                {
                    total_count += m_inventory.Slots[j].Count;
                }
            }

            if (total_count < m_receipe.Ingredient[i].Count)
            {
                m_can_craft = false;
                m_crafting_button.interactable = false;

                m_prohibition_label.text = "<color=red>재료가 부족합니다.</color>";
                m_prohibition_image.SetActive(true);
            }
        }
    }

    public void Return()
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            m_slots[i].transform.SetParent(ObjectManager.Instance.GetPool(ObjectType.ITEMSLOT).Container, false);
            ObjectManager.Instance.ReturnObject(m_slots[i].gameObject, ObjectType.ITEMSLOT);
        }

        m_slots.Clear();
    }

    public void Button_Craft()
    {
        for (int i = 0; i < m_receipe.Ingredient.Count; i++)
        {
            int last_count = m_receipe.Ingredient[i].Count;
            for (int j = 0; j < m_inventory.Slots.Length; j++)
            {
                if (m_inventory.Slots[j].Item == null)
                {
                    continue;
                }

                if (m_receipe.Ingredient[i].Item.ID == m_inventory.Slots[j].Item.ID)
                {
                    if (last_count > m_inventory.Slots[j].Count)
                    {
                        last_count -= m_inventory.Slots[j].Count;
                        m_inventory.Slots[j].Updates(-m_inventory.Slots[j].Count);
                    }
                    else
                    {
                        m_inventory.Slots[j].Updates(-last_count);
                        break;
                    }
                }
            }
        }

        StartCoroutine(Co_Crafting());
    }

    private IEnumerator Co_Crafting()
    {
        float elapsed_time = 0f;
        float target_time = m_receipe.Time;

        m_crafting_button.interactable = false;
        m_prohibition_label.text = "<color=green>아이템 제작 중...</color>";
        m_prohibition_image.SetActive(true);

        while (elapsed_time <= target_time)
        {
            elapsed_time += Time.unscaledDeltaTime;
            yield return null;
        }

        m_crafting_button.interactable = true;
        m_prohibition_label.text = "";
        m_prohibition_image.SetActive(false);

        m_inventory.AquireItem(m_receipe.Target.Item, m_receipe.Target.Count);

        FindFirstObjectByType<Crafter>().Updates();
    }

    #endregion Helper Methods
}
