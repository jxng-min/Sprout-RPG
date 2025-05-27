using UnityEngine;

public class ItemActionManager : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    private Inventory m_inventory;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    #endregion Properties

    private void Awake()
    {
        m_inventory = GetComponent<Inventory>();
    }

    #region Helper Methods
    public bool UseItem(Item item, ItemSlot called_slot = null)
    {
        switch (item.Type)
        {
            case ItemType.Consumable:
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
                break;
                
            case ItemType.Equipment_Helmet:
            case ItemType.Equipment_Armor:
            case ItemType.Equipment_Weapon:
            case ItemType.Equipment_Shield:
                return false;


        }
        return true;
    }
    #endregion Helper Methods
}
