using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    #region Variables
    [Header("몬스터 컨트롤러")]
    [SerializeField] private EnemyCtrl m_enemy_ctrl;

    private float m_hp;

    private Coroutine m_knockback_coroutine;
    #endregion Variables

    #region Properties
    public float HP
    {
        get => m_hp;
        set => m_hp = value;
    }
    #endregion Properties

    #region Helper Methods
    public void Initialize(Enemy enemy)
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();

        m_hp = enemy.HP;
    }
    public void UpdateHP(float amount)
    {
        m_hp += amount;

        if (m_hp <= 0f)
        {
            m_enemy_ctrl.ChangeState(EnemyState.DEAD);
        }
    }

    public void Death()
    {
        
    }
    #endregion Helper Methods
}
