using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Scriptable Object/Create new weapon item")]
public class WeaponItem : EquipmentItem
{
    [Space(50)]
    [Header("무기의 타입")]
    [SerializeField] private WeaponType m_weapon_type;
    public WeaponType Weapon { get => m_weapon_type; }
}
