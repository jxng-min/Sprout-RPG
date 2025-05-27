using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Scriptable Object/Create new equipment item")]
public class EquipmentItem : Item
{
    [Space(50)]
    [Header("장비 아이템 효과")]
    [SerializeField] private EquipmentEffect m_effect;
    public EquipmentEffect Effect { get => m_effect; }
}
