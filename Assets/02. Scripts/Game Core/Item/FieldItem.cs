using UnityEngine;

public class FieldItem : FieldObject
{
    #region Variables
    private Inventory m_inventory;
    private Item m_item;
    #endregion Variables

    #region Properties
    public Item Item
    {
        get => m_item;
        set => m_item = value;
    }
    #endregion Properties 

    private void OnEnable()
    {
        m_inventory = FindFirstObjectByType<Inventory>();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_inventory.AquireItem(m_item);
            ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ITEM);
        }
    }
}