using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCtrl))]
public class Movement2D : MonoBehaviour
{
    #region Variables
    [Header("플레이어 컨트롤러")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    [Space(30)]
    [Header("플레이어의 강체")]
    [SerializeField] private Rigidbody2D m_rigidbody;

    [Header("플레이어의 이동 속도")]
    [Range(10f, 20f)][SerializeField] private float m_speed = 15f;

    private Vector2 m_direction;
    #endregion Variables

    #region Properties
    public float SPD { get => m_speed; }
    public Vector2 Direction { get => m_direction; }
    #endregion Properties

    private void Update()
    {
        m_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move(m_speed);
    }

    #region Helper Methods
    public void Move(float speed)
    {
        m_rigidbody.linearVelocity = m_direction.normalized * speed;
    }

    public bool IsMoving()
    {
        return m_direction.magnitude > 0f;
    }
    #endregion Helper Methods
}
