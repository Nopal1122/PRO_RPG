using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO; // Reference to the ItemSo scriptable object that holds item data
    public int quantity; // Variable to hold the quantity of the item in the slot

    public Image itemImage; // Reference to the UI Image component that displays the item icon
    public TMP_Text quantityText; // Reference to the UI Text component that displays the item quantity
    private InventoryManager inventoryManager; // Reference to the InventoryManager script to manage inventory actions

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot diklik!");
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }

        }
    }

    public void UpdateUi()
    {
        if(itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();   
        }

        else
        {
            itemImage.gameObject.SetActive(false); // Hide the item image if no item is assigned
            quantityText.text = "";
        }


    }

   
}
