using UnityEngine;

public class FieldObject : MonoBehaviour
{
    #region Variables
    [Header("오브젝트가 튕기는 횟수")]
    [SerializeField] private int m_max_bounce;

    [Header("충격을 받을 힘")]
    [SerializeField] private Vector2 m_force;

    [Header("떨어지는 힘")]
    [SerializeField] private float m_gravity;

    [Space(30)]
    [Header("오브젝트 관련")]
    [Header("오브젝트의 스프라이트 트랜스폼")]
    [SerializeField] private Transform m_sprite_transform;

    [Header("오브젝트의 그림자 트랜스폼")]
    [SerializeField] private Transform m_shadow_transform;

    private Vector2 m_direction;

    private float m_max_height;
    private float m_current_height;

    private int m_current_bounce;
    private bool m_is_grounded;
    #endregion Variables

    private void OnEnable()
    {
        Initialize(new Vector2(Random.Range(-m_force.x, m_force.x), Random.Range(-m_force.x, m_force.x)));
    }

    private void Update()
    {
        if (m_is_grounded)
        {
            return;
        }

        m_current_height += -m_gravity * Time.deltaTime;

        m_sprite_transform.position += new Vector3(0f, m_current_height, 0f) * Time.deltaTime;
        transform.position += (Vector3)m_direction * Time.deltaTime;

        float total_velocity = Mathf.Abs(m_current_height) + Mathf.Abs(m_max_height);
        float scale_xy = Mathf.Abs(m_current_height) / total_velocity;
        m_shadow_transform.localScale = Vector2.one * Mathf.Clamp(scale_xy, 0.75f, 1.25f);

        CheckGroundHit();
    }

    #region Helper Methods
    private void Initialize(Vector2 direction)
    {
        m_current_height = Random.Range(m_force.y - 1, m_force.y);
        m_max_height = m_current_height;

        m_is_grounded = false;
        m_max_height /= 1.5f;

        m_direction = direction;

        m_current_height = m_max_height;
        m_current_bounce++;

        m_shadow_transform.gameObject.SetActive(true);
    }

    private void CheckGroundHit()
    {
        if (m_sprite_transform.position.y < m_shadow_transform.position.y)
        {
            m_sprite_transform.position = m_shadow_transform.position;
            m_shadow_transform.localScale = Vector2.one;

            if (m_current_bounce < m_max_bounce)
            {
                Initialize(m_direction / 1.5f);
            }
            else
            {
                m_shadow_transform.gameObject.SetActive(false);
                m_is_grounded = true;
            }
        }
    }
    #endregion Helper Methods
}
