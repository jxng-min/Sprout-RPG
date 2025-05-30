using UnityEngine;

public class FieldItem : MonoBehaviour
{
    #region Variables
    [Header("아이템의 데이터")]
    [SerializeField] private Item m_item;
    #endregion Variables

    #region Properties
    public Item Item { get => m_item; }
    #endregion Properties 

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindFirstObjectByType<Inventory>().AquireItem(m_item);
        }
    }
}
