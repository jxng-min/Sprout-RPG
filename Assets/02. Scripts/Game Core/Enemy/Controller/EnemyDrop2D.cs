using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop2D : MonoBehaviour
{
    #region Variables
    private EnemyCtrl m_enemy_ctrl;

    private int m_exp_amount;
    private int m_exp_deviation;

    private int m_coin_amount;
    private int m_coin_deviation;

    private List<ItemTable> m_item_list;
    #endregion Variables

    private void Awake()
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();

        m_exp_amount = m_enemy_ctrl.ScriptableObject.EXP;
        m_exp_deviation = m_enemy_ctrl.ScriptableObject.EXP_DEV;

        m_coin_amount = m_enemy_ctrl.ScriptableObject.Gold;
        m_coin_deviation = m_enemy_ctrl.ScriptableObject.Gold_DEV;

        m_item_list = m_enemy_ctrl.ScriptableObject.Item_list;
    }

    #region Helper Methods
    public void Drop()
    {
        GetEXP();
        InstantiateCoin();
        InstantiateItem();
    }

    private void GetEXP()
    {
        var amount = Random.Range(m_exp_amount - m_exp_deviation, m_exp_amount + m_exp_deviation);
        DataManager.Instance.Data.EXP += amount;
    }

    private void InstantiateCoin()
    {
        var amount = Random.Range(m_coin_amount - m_coin_deviation, m_coin_amount + m_coin_deviation);

        int gold_count = amount / 25;
        amount %= 25;

        int sliver_count = amount / 5;
        int bronze_count = amount % 5;

        for (int i = 0; i < gold_count; i++)
        {
            FactoryManager.Instance.CreateCoin(CoinCode.GOLD).transform.position = transform.position;
        }

        for (int i = 0; i < sliver_count; i++)
        {
            FactoryManager.Instance.CreateCoin(CoinCode.SILVER).transform.position = transform.position;
        }

        for (int i = 0; i < bronze_count; i++)
        {
            FactoryManager.Instance.CreateCoin(CoinCode.BRONZE).transform.position = transform.position;
        }

    }

    private void InstantiateItem()
    {
        float rate = Random.Range(0f, 100f);
        if (rate > m_enemy_ctrl.ScriptableObject.DropRate)
        {
            return;
        }

        float total_weight = 0f;
        foreach (var item in m_item_list)
        {
            total_weight += item.Weight;
        }

        float pick = Random.Range(0f, total_weight);
        float current = 0f;

        foreach (var item in m_item_list)
        {
            current += item.Weight;
            if (pick <= current)
            {
                var item_obj = ObjectManager.Instance.GetObject(ObjectType.ITEM);
                var field_item = item_obj.GetComponent<FieldItem>();
                field_item.transform.position = transform.position;
                field_item.Item = item.Item;
            }
        }
    }
    #endregion Helper Methods
}
