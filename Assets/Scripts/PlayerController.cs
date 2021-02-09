using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //El objeto actual sobre el que se está realizando una acción, se pierde hasta que otro se vuelve el focus
    public ISelectable onFocus = null;
    //El objeto actual sobre el que se dió click, se pierde si se hace click en otro lado
    public ISelectable selectedObject = null;

    #region Singleton
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return) || Input.touchCount > 0)
        {
            OnScreenTap();
        }
    }

    //Lanza un rayo en la posición donde se toca la pantalla
    // si el rayo encuentra un elemento ISelectable lo coloca en 
    // selectedObject y llama a su método OnSelect o OnDeselect en caso de
    // que ya no sea el seleccionado. Si no encuentra un ISelectable borra
    // la selección actual.
    private void OnScreenTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ISelectable selection = hit.transform.gameObject.GetComponent<ISelectable>();

            if(selectedObject != selection)
            {
                if (selection != null)
                {
                    if(selectedObject != null) selectedObject.OnDeselect();
                    selectedObject = selection;
                    selection.OnSelect();
                }
                else
                {
                    if (selectedObject != null)
                    {
                        WorldButton selectedButton = hit.transform.gameObject.GetComponent<WorldButton>();
                        if(selectedButton != null)
                        {
                            selectedButton.OnButtonPressed();
                        }
                        selectedObject.OnDeselect();
                        selectedObject = null;
                    }
                }

            }
        }
    }
}
