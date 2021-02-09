using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, ISelectable
{

    public GameObject particles;
    public GameObject pointToSpawn;


    public AudioClip seedSound;

    bool hasSeed = false;

    public GameObject seedBase;

    // Start is called before the first frame update
    void Start()
    {

        int haveSeed = Random.Range(0, 100);
        particles.SetActive(false);

        if (haveSeed < 60)
        {
            hasSeed = true;
            particles.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDeselect()
    {
        Debug.Log("Adios Arbol");
    }

    public void OnSelect()
    {
        Debug.Log("Seleccionado una arbol");
        if(hasSeed)
        {
            Debug.Log("Usé mi semilla");
            particles.SetActive(false);
            hasSeed = false;
            Invoke("CreateSeed", 1f);
        }

    }

    void CreateSeed()
    {
        Seed seed = Instantiate(seedBase).AddComponent<Seed>();
        seed.transform.position = pointToSpawn.transform.position;
        seed.pickUpSound = seedSound;
    }
}
