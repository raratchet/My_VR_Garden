using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    public GameObject menuElements;

    public Sprite forestIcon;
    public Sprite gardenIcon;

    public TextMeshProUGUI moneyText;

    public Button sceneChangeButton;

    // Start is called before the first frame update
    void Start()
    {
        ChangeIcon();
        menuElements.SetActive(false);
    }

    void ChangeIcon()
    {
        if (SceneManager.GetActiveScene().name.Equals("Garden"))
        {
            sceneChangeButton.image.sprite = forestIcon;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Forest"))
        {
            sceneChangeButton.image.sprite = gardenIcon;
        }

    }

    public void OpenInv()
    {
        InventoryUI.instance.OpenInvUI();
    }

    public void Exit()
    {
        Debug.Log("Salir al menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void SH_Menu()
    {
        menuElements.SetActive(!menuElements.activeSelf);
    }

    public void ChangeScene()
    {
        if (SceneManager.GetActiveScene().name.Equals("Garden"))
        {
            sceneChangeButton.image.sprite = gardenIcon;
            SceneManager.LoadScene("Forest");
            CurrentPlants.instance.OnUnloadScene();
        }
        else if (SceneManager.GetActiveScene().name.Equals("Forest"))
        {
            sceneChangeButton.image.sprite = forestIcon;
            SceneManager.LoadScene("Garden");
        }
        else
        {
            Debug.Log("Cambiar escena");
        }
        SH_Menu();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        moneyText.text = Inventory.instance.Money + " $";
    }
}
