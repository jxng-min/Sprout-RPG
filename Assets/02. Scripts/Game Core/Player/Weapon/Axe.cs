using UnityEngine;

public class Axe : Weapon
{
    #region Variables
    private Animator m_animator;
    private PolygonCollider2D m_collider;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<PolygonCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            var enemy_ctrl = collider.GetComponent<EnemyCtrl>();
            enemy_ctrl.Health.UpdateHP(-1);
        }
    }

    #region Helper Methods
    public override void Use()
    {
        if (!m_can_use)
        {
            return;
        }

        m_cooltime = 0f;
        m_animator.SetTrigger("Attack");
        GameManager.Instance.Player.Attacking.IsAttacking = true;
    }

    public void EnableCollider()
    {
        m_collider.enabled = true;
    }

    public void DisableCollider()
    {
        m_collider.enabled = false;
    }
    #endregion Helper Methods
}
