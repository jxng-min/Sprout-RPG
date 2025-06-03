using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    #region Variables
    private Rigidbody2D m_rigidbody;

    private int m_atk;
    private float m_speed;
    private Vector2 m_direction;
    #endregion Variables

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
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
    public void Initialize(int atk, float speed, Vector2 direction)
    {
        m_atk = atk;
        m_speed = speed;
        m_direction = direction;

        StartCoroutine(Co_Return());

        Translation();
        Rotation(m_direction);
    }

    private void Translation()
    {
        m_rigidbody.linearVelocity = m_direction * m_speed;
    }

    private void Rotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);
    }

    public void Stop()
    {
        m_rigidbody.linearVelocity = Vector2.zero;
    }

    public void Resume()
    {
        Translation();
    }

    private void Return()
    {
        Stop();
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ARROW);
    }

    private IEnumerator Co_Return()
    {
        float elapsed_time = 0f;
        float target_time = 1.5f;

        while (elapsed_time <= target_time)
        {
            // TODO: PLAYING 상태가 아닐 때엔 정지

            elapsed_time += Time.deltaTime;
            yield return null;
        }

        Return();
    }
    #endregion Helper Methods
}
