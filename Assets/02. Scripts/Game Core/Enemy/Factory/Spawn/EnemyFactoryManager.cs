using System.Collections.Generic;

public class EnemyFactoryManager : Singleton<EnemyFactoryManager>
{
    #region Variables
    private Dictionary<EnemyCode, EnemyFactory> m_factory_dict = new();
    #endregion Variables

    public override void Awake()
    {
        base.Awake();

        RegisterFactory<SlimeFactory>(
            EnemyCode.GREEN_SLIME,
            EnemyCode.RED_SLIME,
            EnemyCode.PINK_SLIME,
            EnemyCode.BLUE_SLIME
        );
    }

    #region Helper Methods
    private void RegisterFactory<TFactory>(params EnemyCode[] codes) where TFactory : EnemyFactory, new()
    {
        foreach (var code in codes)
        {
            m_factory_dict[code] = new TFactory();
        }
    }

    public EnemyCtrl Create(EnemyCode code)
    {
        if (m_factory_dict.TryGetValue(code, out var factory))
        {
            return factory.Create(code);
        }

        return null;
    }
    #endregion Helper Methods
}
