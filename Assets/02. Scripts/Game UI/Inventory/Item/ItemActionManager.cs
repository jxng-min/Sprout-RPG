using UnityEngine;
using UnityEngine.UI;

public class ItemActionManager : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    private Inventory m_inventory;
    private Equipment m_equipment;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    #endregion Properties

    private void Awake()
    {
        m_inventory = GetComponent<Inventory>();
        m_equipment = GetComponent<Equipment>();
    }

    #region Helper Methods
    public bool UseItem(Item item, ItemSlot called_slot = null)
    {
        switch (item.Type)
        {
            case ItemType.Consumable:
                ActivateItem(item);
                break;

            case ItemType.Equipment_Helmet:
            case ItemType.Equipment_Armor:
            case ItemType.Equipment_Weapon:
            case ItemType.Equipment_Shield:
                ToggleEquipment(item, called_slot);
                return false;
        }
        return true;
    }

    private void ActivateItem(Item item)
    {
        switch (item.ID)
        {
            case (int)ItemCode.SMALL_HP_POTION:
                break;

            case (int)ItemCode.SMALL_MP_POTION:
                break;

            case (int)ItemCode.SMALL_POTION:
                break;

            case (int)ItemCode.MEDIUM_HP_POTION:
                break;

            case (int)ItemCode.MEDIUM_MP_POTION:
                break;

            case (int)ItemCode.MEDIUM_POTION:
                break;
        }
    }

    private void ToggleEquipment(Item item, ItemSlot called_slot)
    {
        if (Item.CheckEquipmentType(called_slot.Mask))
        {
            var inventory_slot = m_inventory.GetValidSlot(item);

            if (inventory_slot != null)
            {
                called_slot.Clear();
                m_inventory.AquireItem(item);
            }
        }
        else
        {
            var equipment_slot = m_equipment.GetSlot(item.Type);

            var temp_item = equipment_slot.Item;
            equipment_slot.Add(item);

            if (temp_item != null)
            {
                called_slot.Add(temp_item);
            }
            else
            {
                called_slot.Clear();
            }
        }

        m_equipment.Calculation();
    }

    public void SlotOnDropEvent(ItemSlot from_slot, ItemSlot to_slot)
    {
        if (Item.CheckEquipmentType(from_slot.Mask) || Item.CheckEquipmentType(to_slot.Mask))
        {
            m_equipment.Calculation();
        }
    }
    #endregion Helper Methods
}
