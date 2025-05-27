using UnityEngine;

[System.Serializable]
public class EquipmentEffect
{
    #region Variables
    [Header("체력")]
    [SerializeField] private float m_hp;

    [Header("추가 마나")]
    [SerializeField] private float m_mp;

    [Header("추가 공격력")]
    [SerializeField] private int m_atk;

    [Header("추가 이동속도")]
    [SerializeField] private float m_spd;
    #endregion Variables

    #region Properties
    public float HP
    {
        get => m_hp;
        set => m_hp = value;
    }

    public float MP
    {
        get => m_mp;
        set => m_mp = value;
    }

    public int ATK
    {
        get => m_atk;
        set => m_atk = value;
    }

    public float SPD
    {
        get => m_spd;
        set => m_spd = value;
    }

    #endregion Properties

    public static EquipmentEffect operator +(EquipmentEffect eft1, EquipmentEffect eft2)
    {
        EquipmentEffect calculated_effect = new EquipmentEffect();

        calculated_effect.HP = eft1.HP + eft2.HP;
        calculated_effect.MP = eft1.MP + eft2.MP;
        calculated_effect.ATK = eft1.ATK + eft2.ATK;
        calculated_effect.SPD = eft1.SPD + eft2.SPD;

        return calculated_effect;
    }
}
