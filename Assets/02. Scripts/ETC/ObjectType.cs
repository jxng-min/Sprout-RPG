public enum ObjectType
{
    #region SFX / UI (0 ~ 10)
    SFX = 0, SHOPSLOT = 1, ITEMSLOT = 2, CRAFTINGSLOT = 3, DAMAGE_INDICATOR = 4,
    #endregion

    #region Item(20 ~ 100)
    ITEM = 20, BRONZE_COIN = 21, SILVER_COIN = 22, GOLD_COIN = 23,
    #endregion

    #region Enemy(101 ~ 1000)
    GREEN_SLIME = 101, RED_SLIME = 102, PINK_SLIME = 103, BLUE_SLIME = 104,
    #endregion

    #region Skill(1001 ~ )
    ARROW = 1001,
    #endregion
}