using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyCtrl))]
public class EnemyMovement2D : MonoBehaviour
{
    #region Variables
    private EnemyCtrl m_enemy_ctrl;
    private Rigidbody2D m_rigidbody;
    private float m_spd;

    private Vector2 m_direction;
    private Coroutine m_move_coroutine;
    private bool m_is_moving;

    private List<Node> m_current_path;
    private Node m_current_node;
    #endregion Variables

    #region Properties
    public Rigidbody2D Rigidbody { get => m_rigidbody; }

    public float SPD
    {
        get => m_spd;
        set => m_spd = value;
    }

    public Vector2 Direction { get => m_direction; }

    public bool IsMoving
    {
        get => m_is_moving;
        set => m_is_moving = value;
    }
    #endregion Properties

    private void Awake()
    {
        m_current_path = new();

        m_enemy_ctrl = GetComponent<EnemyCtrl>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_rigidbody.simulated = true;
    }

    private void Start()
    {
        m_spd = m_enemy_ctrl.ScriptableObject.SPD;
    }

    #region Helper Methods
    public void Move()
    {
        m_current_path = m_enemy_ctrl.Pathfinder.Pathfind(transform.position, GetRandomPos());
        if (m_current_path == null)
        {
            return;
        }

        if (m_move_coroutine != null)
        {
            return;
        }

        m_is_moving = true;
        m_move_coroutine = StartCoroutine("Co_Move", false);
        return;
    }

    public void Trace(Vector3 target_position)
    {
        m_current_path = m_enemy_ctrl.Pathfinder.Pathfind(transform.position, target_position);
        if (m_current_path == null)
        {
            return;
        }

        m_is_moving = true;
        if (m_move_coroutine != null)
        {
            StopCoroutine(m_move_coroutine);
            m_move_coroutine = null;
        }
        m_move_coroutine = StartCoroutine("Co_Move", true);
        return;
    }

    protected virtual IEnumerator Co_Move(bool is_trace)
    {
        int index = 0;

        while (true)
        {
            yield return new WaitUntil(() => GameManager.Instance.Current == GameEventType.PLAYING);

            if (m_current_path == null || m_current_path.Count == 0)
            {
                m_is_moving = false;
                m_move_coroutine = null;
                yield break;
            }

            if (index >= m_current_path.Count)
            {
                m_move_coroutine = null;
                m_is_moving = false;

                m_enemy_ctrl.ChangeState(EnemyState.IDLE);
                yield break;
            }

            m_current_node = m_current_path[index];

            if (Vector2.Distance(transform.position, m_current_node.World) < 0.1f)
            {
                index++;
                continue;
            }

            m_direction = (m_current_node.World - (Vector2)transform.position).normalized;
            m_enemy_ctrl.Animator.SetFloat("DirX", m_direction.x);
            m_enemy_ctrl.Animator.SetFloat("DirY", m_direction.y);

            if (!m_enemy_ctrl.Health.IsStaggered)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_current_node.World, Time.deltaTime * m_spd);
            }

            yield return null;
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 offset = Random.insideUnitCircle * 4f;
        offset.z = 0f;

        return transform.position + offset;
    }

    public void Reset()
    {
        if (m_move_coroutine != null)
        {
            StopCoroutine(m_move_coroutine);
            m_move_coroutine = null;
        }

        m_is_moving = false;

        Rigidbody.linearVelocity = Vector2.zero;
        Rigidbody.angularVelocity = 0f;
    }
    #endregion Helper Methods
}
