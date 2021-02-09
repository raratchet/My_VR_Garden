using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, ISelectable
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeselect()
    {

    }

    public void OnSelect()
    {
        anim.SetTrigger("isJumping_Up");
        PlayerController.instance.selectedObject = null;
    }
}
