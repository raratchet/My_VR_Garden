using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{

	public Image icon;

	Seed seed; 

	public void AddSeed(Seed newSeed)
	{
		seed = newSeed;

		icon.sprite = seed.icon;
		icon.enabled = true;
	}

	public void ClearSlot()
	{
		seed = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	public void RemoveSeedFromInventory()
	{
		Inventory.instance.Remove(seed);
	}

	public void UseItem()
	{
		if (seed != null)
		{
			Pot pot = PlayerController.instance.onFocus as Pot;
			if(pot != null )
            {
				pot.Plant(seed);
				InventoryUI.instance.HideInvUI();
				RemoveSeedFromInventory();
				ClearSlot();
            }
		}
	}

}
