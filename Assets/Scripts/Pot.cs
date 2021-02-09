using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, ISelectable
{
    [SerializeField]
    private WorldButton plant_button;
    [SerializeField]
    private WorldButton water_button;
    [SerializeField]
    private WorldButton fert_button;
    [SerializeField]
    private WorldButton pick_button;
    public Renderer _renderer;

    //Pasar este array a un objecto único para no cargar la memoria
    public GameObject[] plantPrefabs;

    public GameObject waterDrops;
    public GameObject fertilizeDrops;
    public AudioSource audioS;

    Seed currentPlant;
    [SerializeField]
    PlantState plantState;
    [SerializeField]
    SoilState soilState;
    public bool isPlanted;
    bool isFullGrown;
    float plantSizeMod;
    [SerializeField]
    float MAX_PLANT_SIZE;
    GameObject plant;


    public AudioClip buttonPress;


    // Start is called before the first frame update
    void Start()
    {
        CurrentPlants.instance.OnLoadScene();
        _renderer = GetComponent<Renderer>();
        audioS = GetComponent<AudioSource>();
        isPlanted = false;
        isFullGrown = false;
        plantSizeMod = 0.5f;
        RegisterButtonsListeners();
        DeactivateParticles();
        DeactivateButtons();
    }

    void RegisterButtonsListeners()
    {
        plant_button.buttonPress += DoPlantation;
        fert_button.buttonPress += Fertilize;
        water_button.buttonPress += Water;
        pick_button.buttonPress += Pick;
    }

    void DoPlantation()
    {
        //Abrir inventario y seleccionar una semilla
        audioS.Play();
        InventoryUI.instance.OpenInvUI();
    }

    void Fertilize()
    {
        // Codigo para fertilizar
        audioS.Play();
        Debug.Log("Fertilizando :)");
        fertilizeDrops.SetActive(true);
        Invoke("DeactivateParticles", 2.5f);
    }

    void Water()
    {
        //Codigo para regar
        audioS.Play();
        Debug.Log("Regando :)");
        waterDrops.SetActive(true);
        Invoke("DeactivateParticles",2.5f);
    }

    void DeactivateParticles()
    {
        waterDrops.SetActive(false);
        fertilizeDrops.SetActive(false);
    }

    void Pick()
    {
        //Código para recoger la planata madura
        audioS.Play();
        isPlanted = false;
        isFullGrown = false;
        plantSizeMod = 0.5f;
        Destroy(plant);
        Destroy(currentPlant.gameObject);
        Inventory.instance.Money += 100;
        Debug.Log("Recogiendo la planta");
    }

    void DeactivateButtons()
    {
        plant_button.gameObject.SetActive(false);
        water_button.gameObject.SetActive(false);
        fert_button.gameObject.SetActive(false);
        pick_button.gameObject.SetActive(false);
    }

    public void Plant(Seed seed)
    {
        if(!isPlanted)
        {
            currentPlant = seed;
            if (seed.Type.Equals(PlantType.APPLE))
            {
                plant = Instantiate(plantPrefabs[0]);
            }
            else if (seed.Type.Equals(PlantType.CARROT))
            {
                plant = Instantiate(plantPrefabs[1]);
            }
            plant.transform.parent = this.transform;
            plant.transform.localScale = plant.transform.localScale * plantSizeMod;
            plant.transform.position = this.transform.position + new Vector3(0, 2, 0);
            InvokeRepeating("Grow", seed.growTime, seed.growTime);
            isPlanted = true;
        }
    }

    void Grow()
    {
        if(plantSizeMod < MAX_PLANT_SIZE)
        {
            plant.transform.localScale = plant.transform.localScale / plantSizeMod; 
            plantSizeMod += 0.1f;
            plant.transform.localScale = plant.transform.localScale * plantSizeMod;
        }
        else
        {
            //CANCEL INVOKE
            isFullGrown = true;
            CancelInvoke();
        }
    }

    public void OnDeselect()
    {
        _renderer.material.color = Color.white;
        DeactivateButtons();
    }

    float lastTime = 0;

    public void OnSelect()
    {
        if (!(lastTime + 0.5f < Time.time))
        {
            return;
        }

        lastTime = Time.time;

            //Vuelve el foco de selección hasta que otro pase a serlo
            PlayerController.instance.onFocus = this;
        _renderer.material.color = Color.red;
        if(!isPlanted)
        {
            plant_button.gameObject.SetActive(true);
        }
        else
        {
            if(!isFullGrown)
            {
                fert_button.gameObject.SetActive(true);
                water_button.gameObject.SetActive(true);
            }
            else
            {
                pick_button.gameObject.SetActive(true);
            }

        }

    }
}
