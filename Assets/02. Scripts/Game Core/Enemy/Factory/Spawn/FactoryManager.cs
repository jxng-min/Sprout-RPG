using System.Collections.Generic;

public class FactoryManager : Singleton<FactoryManager>
{
    #region Variables
    private Dictionary<EnemyCode, EnemyFactory> m_enemy_factory_dict = new();
    private Dictionary<CoinCode, CoinFactory> m_coin_factory_dict = new();
    #endregion Variables

    public override void Awake()
    {
        base.Awake();

        RegisterEnemyFactory<SlimeFactory>(
            EnemyCode.GREEN_SLIME,
            EnemyCode.RED_SLIME,
            EnemyCode.PINK_SLIME,
            EnemyCode.BLUE_SLIME
        );

        RegisterCoinFactory<CoinFactory>(
            CoinCode.BRONZE,
            CoinCode.SILVER,
            CoinCode.GOLD
        );
    }

    #region Helper Methods
    private void RegisterEnemyFactory<TFactory>(params EnemyCode[] codes) where TFactory : EnemyFactory, new()
    {
        foreach (var code in codes)
        {
            m_enemy_factory_dict[code] = new TFactory();
        }
    }

    private void RegisterCoinFactory<TFactory>(params CoinCode[] codes) where TFactory : CoinFactory, new()
    {
        foreach (var code in codes)
        {
            m_coin_factory_dict[code] = new TFactory();
        }
    }

    public EnemyCtrl CreateEnemy(EnemyCode code)
    {
        if (m_enemy_factory_dict.TryGetValue(code, out var factory))
        {
            return factory.Create(code);
        }

        return null;
    }

    public FieldCoin CreateCoin(CoinCode code)
    {
        if (m_coin_factory_dict.TryGetValue(code, out var factory))
        {
            return factory.Create(code);
        }

        return null;
    }
    #endregion Helper Methods
}
