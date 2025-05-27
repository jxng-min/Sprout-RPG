using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Create new equipment item")]
public class EquipmentItem : Item
{
    [Space(50)]
    [Header("아이템의 공격력")]
    [SerializeField] private int m_atk;
    public int ATK { get => m_atk; }

    [Header("아이템의 체력")]
    [SerializeField] private float m_hp;
    public float HP { get => m_hp; }

    [Header("아이템의 마나")]
    [SerializeField] private float m_mp;
    public float MP { get => m_mp; }

    [Header("아이템의 성장 공격력")]
    [SerializeField] private int m_growth_atk;
    public int GrowthATK { get => m_growth_atk; }

    [Header("아이템의 성장 체력")]
    [SerializeField] private float m_growth_hp;
    public float GrowthHP { get => m_growth_hp; }

    [Header("아이템의 성장 마나")]
    [SerializeField] private float m_growth_mp;
    public float GrowthMP { get => m_growth_mp; }
}
