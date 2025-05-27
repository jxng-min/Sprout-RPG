
using UnityEngine;

public class Bow : Weapon
{
    #region Variables
    private Animator m_animator;

    private float m_arrow_speed = 100f;
    #endregion Variables

    public void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public override void Use()
    {
        if (!m_can_use)
        {
            return;
        }

        m_cooltime = 0f;
        m_animator.SetTrigger("Attack");
    }

    private void Shoot()
    {
        var arrow_obj = ObjectManager.Instance.GetObject(ObjectType.ARROW);
        arrow_obj.transform.position = transform.position;

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        var arrow = arrow_obj.GetComponent<Arrow>();
        arrow.Initialize(0, m_arrow_speed, direction);
    }
}
