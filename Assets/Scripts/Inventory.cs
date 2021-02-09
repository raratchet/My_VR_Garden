using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	#region Singleton

	public static Inventory instance = null;

	void Awake()
	{
		if(instance == null)
        {
			instance = this;
			DontDestroyOnLoad(this);
		}else
        {
			DestroyImmediate(this);
        }

	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 10; 

	public List<Seed> seeds = new List<Seed>();
	//Los iconos para las plantas -- Esto no va aqui compa
	public List<Sprite> seedIcons;

	public int Money = 0;

	public void Add(Seed seed)
	{
		if (seeds.Count >= space)
		{
			Debug.Log("Not enough room.");
			return;
		}

		seeds.Add(seed);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void Remove(Seed seed)
	{
		seeds.Remove(seed);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public bool IsEmpty()
    {
		if (seeds.Count > 0) return false;
		return true;
    }

}