using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Variables
    protected bool m_can_use;
    protected float m_cooltime;
    protected float m_origin_cooltime;
    #endregion Variables

    private void Update()
    {
        CheckCooldown();
    }

    #region Helper Methods
    public void Initialize(float cooltime)
    {
        m_cooltime = cooltime;
        m_origin_cooltime = cooltime;
    }

    public abstract void Use();
    protected void CheckCooldown()
    {
        if (m_cooltime >= m_origin_cooltime)
        {
            m_can_use = true;
            m_cooltime = m_origin_cooltime;
        }
        else
        {
            m_can_use = false;
            m_cooltime += Time.deltaTime;
        }
    }

    public void ResetState()
    {
        GameManager.Instance.Player.Attacking.IsAttacking = false;
    }

    protected void InstantiateIndicator(Vector3 position, float damage)
    {
        var obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        obj.transform.position = position;

        var indicator = obj.GetComponent<DamageIndicator>();
        indicator.Initialize(damage);
    }
    #endregion Helper Methods
}
