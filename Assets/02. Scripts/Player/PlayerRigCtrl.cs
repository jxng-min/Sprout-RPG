using UnityEngine;

public class PlayerRigCtrl : MonoBehaviour
{
    #region Variables
    [Header("마우스 트래커")]
    [SerializeField] private MouseTracking m_mouse;
    #endregion Variables

    private void Update()
    {
        if (GameManager.Instance.Player.Attacking.IsAttacking)
        {
            return;
        }

        Rotation();
    }

    #region Helper Methods
    private void Rotation()
    {
        Vector2 direction = (m_mouse.Position - (Vector2)transform.position).normalized;
        float z_angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, z_angle);
    }
    #endregion Helper Methods
}
