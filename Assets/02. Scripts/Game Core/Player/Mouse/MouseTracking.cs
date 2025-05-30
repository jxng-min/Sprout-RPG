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

    #region Helper Methods
    private void Set()
    {
        m_mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion Helper Methods
}