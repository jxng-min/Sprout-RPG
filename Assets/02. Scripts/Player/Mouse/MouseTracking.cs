using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    #region Variables
    private Vector2 m_mouse_position;
    #endregion Variables

    #region Properties
    public Vector2 Position { get => m_mouse_position; }
    #endregion Properties

    private void Update()
    {
        Set();
    }

    private void Set()
    {
        // TODO: 플레이어가 공격중일때는 회전 X
        m_mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}