using UnityEngine;

public class Attack2D : MonoBehaviour
{
    #region Variables
    [Header("플레이어 컨트롤러")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    [Header("무기 애니메이터")]
    [SerializeField] private Animator m_weapon_animator;

    private bool m_is_attacking;

    private IWeapon m_weapon;
    #endregion Variables

    #region Properties
    public bool IsAttacking { get => m_is_attacking; }
    public Animator Animator { get => m_weapon_animator; }
    public IWeapon Weapon { get => m_weapon; }
    #endregion Properties

    private void Awake()
    {
    }

    #region Helper Methods
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_player_ctrl.ChangeState(PlayerState.ATTACK);
        }
    }

    public void SwapWeapon(IWeapon weapon)
    {
        m_weapon = weapon;
    }
    #endregion Helper Methods
}
