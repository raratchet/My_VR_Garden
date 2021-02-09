using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlants : MonoBehaviour
{

	public Dictionary<string, GameObject> pots = new Dictionary<string, GameObject>();

	#region Singleton

	public static CurrentPlants instance;

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

    public void OnUnloadScene()
    {
		foreach(var pot in GameObject.FindObjectsOfType<Pot>())
        {
			if(pot.isPlanted)
            {
				pots.Add(pot.gameObject.name, pot.gameObject);
				pot.gameObject.SetActive(false);
				DontDestroyOnLoad(pot.gameObject.transform.root);
            }
			else
            {
				DestroyImmediate(pot.gameObject);
            }
        }
    }

    public void OnLoadScene()
    {
		if (pots.Count == 0) return;

		List<GameObject> toDestroy = new List<GameObject>();

		foreach (var pot in GameObject.FindObjectsOfType<Pot>())
		{
			if(pots.ContainsKey(pot.gameObject.name))
            {
				GameObject go = pots[pot.gameObject.name];
				toDestroy.Add(pot.gameObject);
				go.SetActive(true);
            }
		}

		foreach(var poti in toDestroy)
        {
			DestroyImmediate(poti.gameObject);
			Debug.Log("Destrui");
		}

		pots.Clear();

		Debug.Log("Recontrui plantas");

	}
}
