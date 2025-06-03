public class SlimeFactory : EnemyFactory
{
    public override EnemyCtrl Create(EnemyCode code)
    {
        switch (code)
        {
            case EnemyCode.GREEN_SLIME:
                return ObjectManager.Instance.GetObject(ObjectType.GREEN_SLIME).GetComponent<EnemyCtrl>();

            case EnemyCode.RED_SLIME:
                return ObjectManager.Instance.GetObject(ObjectType.RED_SLIME).GetComponent<EnemyCtrl>();

            case EnemyCode.PINK_SLIME:
                return ObjectManager.Instance.GetObject(ObjectType.PINK_SLIME).GetComponent<EnemyCtrl>();

            case EnemyCode.BLUE_SLIME:
                return ObjectManager.Instance.GetObject(ObjectType.BLUE_SLIME).GetComponent<EnemyCtrl>();

            default:
                return null;
        }
    }
}
