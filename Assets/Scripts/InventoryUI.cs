using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{

	public GameObject inventoryUI; 
	public Transform itemsParent;

	Inventory inventory;

	#region Singleton

	public static InventoryUI instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}

	}

	#endregion

	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		inventoryUI.SetActive(false);
	}

	void Update()
	{

	}

	public void OpenInvUI()
    {
		inventoryUI.SetActive(!inventoryUI.activeSelf);
		UpdateUI();
	}

	public void HideInvUI()
    {
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}

	public void UpdateUI()
	{
		InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.seeds.Count)
			{
				slots[i].AddSeed(inventory.seeds[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}

}
