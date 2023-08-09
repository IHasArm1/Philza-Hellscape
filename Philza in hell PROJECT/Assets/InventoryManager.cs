using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    public Item[] startItems;

    public int _maxItemStack;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        foreach (var item in startItems)
        {
            AddItem(item);
        }
    }

    private void Update()
    {
        if(selectedSlot == 0)
        {
            if (Input.GetButtonDown("InventoryLeft"))
            {
                ChangeSelectedSlot(9);
            }
        }
        if(selectedSlot >= 9)
        {
            ChangeSelectedSlot(0);
        }
        // changing inventory slots for controller
        if (Input.GetButtonDown("InventoryRight"))
        {
            ChangeSelectedSlot(selectedSlot + 1);
        }

        if (Input.GetButtonDown("InventoryLeft"))
        {
            ChangeSelectedSlot(selectedSlot - 1);
        }
        // changing inventory slots for mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) // Selection with scroll wheel.
        {
            int newValue = selectedSlot + (int)(scroll / Mathf.Abs(scroll));
            if (newValue < 0)
            {
                newValue = inventorySlots.Length - 1;
            }
            else if (newValue >= inventorySlots.Length)
            {
                newValue = 0;
            }
            ChangeSelectedSlot(newValue);
        }
        // changing inventory slots for keyboard numbers
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 11)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    public void SaveInv()
    {

    }

    public void LoadInv()
    {

    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < _maxItemStack &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        // find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null){
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

}
