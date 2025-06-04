public class CoinFactory
{
    public FieldCoin Create(CoinCode code)
    {
        switch (code)
        {
            case CoinCode.BRONZE:
                return ObjectManager.Instance.GetObject(ObjectType.BRONZE_COIN).GetComponent<FieldCoin>();

            case CoinCode.SILVER:
                return ObjectManager.Instance.GetObject(ObjectType.SILVER_COIN).GetComponent<FieldCoin>();

            case CoinCode.GOLD:
                return ObjectManager.Instance.GetObject(ObjectType.GOLD_COIN).GetComponent<FieldCoin>();

            default:
                return null;
        }
    }
}
