using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackUI
{
    public WeaponType Type;
    public Weapon Interface;
}

public class Attack2D : MonoBehaviour
{
    #region Variables
    [Header("플레이어 컨트롤러")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    [Header("플레이어의 무기 인터페이스의 목록")]
    [SerializeField] private List<AttackUI> m_attack_uis;

    private Dictionary<WeaponType, AttackUI> m_attack_ui_dict;

    private AttackUI m_current_ui;
    private bool m_is_attacking;
    #endregion Variables

    #region Properties
    public bool IsAttacking { get => m_is_attacking; }
    public AttackUI UI { get => m_current_ui; }
    #endregion Properties

    private void Awake()
    {
        Initialize();
    }

    #region Helper Methods
    private void Initialize()
    {
        m_attack_ui_dict = new();
        for (int i = 0; i < m_attack_uis.Count; i++)
        {
            m_attack_ui_dict.Add(m_attack_uis[i].Type, m_attack_uis[i]);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_player_ctrl.ChangeState(PlayerState.ATTACK);
        }
    }

    public void SwapWeapon(WeaponItem item)
    {
        var type = item.Weapon;
        if (m_attack_ui_dict.TryGetValue(type, out var weapon_ui))
        {
            if (m_current_ui != null)
            {
                m_current_ui.Interface.gameObject.SetActive(false);
            }

            weapon_ui.Interface.gameObject.SetActive(true);

            m_current_ui = weapon_ui;
            m_current_ui.Interface.Initialize(item.Cooltime);
        }
    }
    #endregion Helper Methods
}
