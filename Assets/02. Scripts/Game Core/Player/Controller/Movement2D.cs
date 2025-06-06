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

    private float m_spd;

    private Vector2 m_direction;
    #endregion Variables

    #region Properties
    public float SPD { get => m_spd; }

    public Vector2 Direction { get => m_direction; }
    #endregion Properties

    private void Awake()
    {
        transform.position = DataManager.Instance.PlayerData.Data.Position;
    }

    private void Start()
    {
        UpdateSPD();   
    }

    private void Update()
    {
        m_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        SetAnimation();
    }

    private void FixedUpdate()
    {
        Move(m_spd);
    }

    #region Helper Methods
    public void UpdateSPD()
    {
        m_spd = DataManager.Instance.PlayerData.Data.Status.SPD + m_player_ctrl.Equipment.Effect.SPD;
    }

    public void Move(float speed)
    {
        m_rigidbody.linearVelocity = m_direction.normalized * speed;
    }

    public bool IsMoving()
    {
        return m_direction.magnitude > 0f;
    }
    private void SetAnimation()
    {
        Vector2 normarlized_direction = (m_player_ctrl.Mouse.Position - (Vector2)transform.position).normalized;

        m_player_ctrl.Animator.SetFloat("MoveX", normarlized_direction.x);
        m_player_ctrl.Animator.SetFloat("MoveY", normarlized_direction.y);
    }
    #endregion Helper Methods
}
