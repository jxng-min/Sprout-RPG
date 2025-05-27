using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    #region Variables
    [Header("UI 관련 컴포넌트")]
    [Header("툴팁 UI 오브젝트")]
    [SerializeField] private GameObject m_tooltip_object;

    [Header("아이템 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("아이템 설명 라벨")]
    [SerializeField] private TMP_Text m_description_label;

    [Space(30)]
    [Header("캔버스")]
    [SerializeField] private Canvas m_canvas;

    private RectTransform m_rect_transform;
    #endregion Variables

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (m_tooltip_object.activeInHierarchy)
        {
            CalculateMousePosition();
        }
    }

    #region Helper Methods
    private void Initialize()
    {
        m_name_label.text = "";
        m_description_label.text = "";

        m_rect_transform = m_canvas.transform as RectTransform;

        m_tooltip_object.SetActive(false);
    }

    private void Clear()
    {
        m_name_label.text = "";
        m_description_label.text = "";
    }

    public void OpenUI(int item_id)
    {
        Clear();

        m_name_label.text = ItemDataManager.Instance.GetName(item_id);
        m_description_label.text = ItemDataManager.Instance.GetDescription(item_id);

        m_tooltip_object.SetActive(true);
        (m_tooltip_object.transform as RectTransform).SetAsLastSibling();
    }

    public void CloseUI()
    {
        Clear();

        m_tooltip_object.SetActive(false);
    }

    private void CalculateMousePosition()
    {
        Vector2 local_position;
        Vector2 mouse_position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        var rect_transform = m_tooltip_object.transform as RectTransform;

        Camera ui_camera = m_canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : m_canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_rect_transform, 
            mouse_position, 
            ui_camera, 
            out local_position
        );

        if (mouse_position.x > Screen.width * 0.85f)
        {
            local_position.x -= rect_transform.sizeDelta.x;
        }
        else
        {
            local_position.x += 40f;
        }

        if (mouse_position.y < Screen.height * 0.2f)
        {
            local_position.y += rect_transform.sizeDelta.y;
        }

        rect_transform.anchoredPosition = local_position;        
    }
    #endregion Helper Methods
}
