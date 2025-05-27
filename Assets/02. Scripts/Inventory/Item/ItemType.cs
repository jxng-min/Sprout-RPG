[System.Flags]
public enum ItemType
{
    None = 0,

    Coin = 1 << 0,
    Consumable = 1 << 1,

    Equipment_Helmet = 1 << 2,
    Equipment_Armor = 1 << 3,
    Equipment_Weapon = 1 << 4,
    Equipment_Shield = 1 << 5,
}
