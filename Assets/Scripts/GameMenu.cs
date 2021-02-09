using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public void Start()
    {
        Inventory invetory = GameObject.FindObjectOfType<Inventory>();
        InventoryUI invetoryUI = GameObject.FindObjectOfType<InventoryUI>();

        if (invetory != null) Destroy(invetory.gameObject);
        if (invetoryUI != null) Destroy(invetoryUI.gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Garden");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
