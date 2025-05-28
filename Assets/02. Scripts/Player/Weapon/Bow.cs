using UnityEngine;

public class Bow : Weapon
{
    #region Variables
    private Animator m_animator;
    private float m_arrow_speed = 100f;
    private Vector2 m_last_direction;
    #endregion Variables

    public void Awake()
    {
        m_animator = GetComponent<Animator>();
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
        m_last_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameManager.Instance.Player.Attacking.IsAttacking = true;
    }

    public void Shoot()
    {
        var arrow_obj = ObjectManager.Instance.GetObject(ObjectType.ARROW);
        arrow_obj.transform.position = transform.position;

        Vector2 direction = (m_last_direction - (Vector2)GameManager.Instance.Player.transform.position).normalized;

        var arrow = arrow_obj.GetComponent<Arrow>();
        arrow.Initialize(0, m_arrow_speed, direction);
    }
    #endregion Helper Methods
}