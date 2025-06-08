using UnityEngine;

public class MouseRaycasting : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.Current != GameEventType.PLAYING)
        {
            return;
        }

        CheckObject();
        InteractionObject();
    }

    #region Helper Methods 
    private void CheckObject()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouse_position, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("NPC"))
            {
                GameManager.Instance.Cursor.SetCursor(CursorMode.CAN_TALK);
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                GameManager.Instance.Cursor.SetCursor(CursorMode.CAN_ATTACK);
            }
            else
            {
                GameManager.Instance.Cursor.SetCursor(CursorMode.DEFAULT);
            }
        }
        else
        {
            GameManager.Instance.Cursor.SetCursor(CursorMode.DEFAULT);
        }
    }

    private void InteractionObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var obj = Physics2D.OverlapPoint(mouse_position);
            if (obj != null && obj.CompareTag("NPC"))
            {
                obj.GetComponent<NPC>().Interaction();
            }
        }
    }
    #endregion Helper Methods
}
