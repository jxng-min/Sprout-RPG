[System.Flags]
public enum ItemType
{
    None = 0,

    Consumable = 1 << 0,

    Equipment_Helmet = 1 << 1,
    Equipment_Armor = 1 << 2,
    Equipment_Weapon = 1 << 3,
    Equipment_Shield = 1 << 4,
}
