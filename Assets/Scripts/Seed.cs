using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour,ISelectable
{
    [SerializeField]
    private PlantType p_type;

    public Sprite icon;
    public float growTime = 5;

    public AudioSource source;
    public AudioClip pickUpSound;

    public MeshRenderer m_renderer;

    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = pickUpSound;
        source.playOnAwake = false;
        m_renderer = GetComponent<MeshRenderer>();
        int i = Random.Range(0, 2);
        if(i == 0)
        {
            p_type = PlantType.APPLE;
            icon = Inventory.instance.seedIcons[0];
        }else
        {
            p_type = PlantType.CARROT;
            icon = Inventory.instance.seedIcons[1];
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.Play();
        }
    }
    public void OnSelect()
    {
        source.Play();
        Inventory.instance.Add(this);
        DontDestroyOnLoad(this);
        m_renderer.enabled = false;
        Invoke("Deactivate", 1f);
    }

    public void Deactivate()
    {

        gameObject.SetActive(false);
    }

    public void OnDeselect()
    {

    }

    public PlantType Type { get { return p_type; } set { p_type = value;} }

}
