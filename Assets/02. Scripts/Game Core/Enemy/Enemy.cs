using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTable
{
    public Item Item;
    public float Weight;
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Create new enemy")]
public class Enemy : ScriptableObject
{
    [Header("몬스터의 고유한 ID")]
    [SerializeField] private EnemyCode m_id;
    public EnemyCode ID { get => m_id; }

    [Header("몬스터의 오브젝트 타입")]
    [SerializeField] private ObjectType m_object_type;
    public ObjectType Type { get => m_object_type; }


    [Space(30)]
    [Header("몬스터의 능력치")]
    [Header("몬스터의 체력")]
    [SerializeField] private float m_hp;
    public float HP { get => m_hp; }

    [Header("몬스터의 공격력")]
    [SerializeField] private int m_atk;
    public int ATK { get => m_atk; }

    [Header("몬스터의 이동 속도")]
    [SerializeField] private float m_spd;
    public float SPD { get => m_spd; }

    [Space(30)]
    [Header("몬스터 처치 보상")]
    [Header("경험치 드랍량")]
    [SerializeField] private int m_exp;
    public int EXP { get => m_exp; }

    [Header("경험치 편차")]
    [SerializeField] private int m_exp_dev;
    public int EXP_DEV { get => m_exp_dev; }

    [Header("골드 드랍량")]
    [SerializeField] private int m_gold;
    public int Gold { get => m_gold; }

    [Header("골드 편차")]
    [SerializeField] private int m_gold_dev;
    public int Gold_DEV { get => m_gold_dev; }

    [Header("드랍 아이템 목록")]
    [SerializeField] private List<ItemTable> m_item_list;
    public List<ItemTable> Item_list { get => m_item_list; }

    [Header("아이템 드랍 확률(0 ~ 100)")]
    [SerializeField] private float m_drop_rate;
    public float DropRate { get => m_drop_rate; }
}
