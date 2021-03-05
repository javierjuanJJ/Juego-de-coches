using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{

    public Button opciones;
    public Button volverAlPrincipio;
    public Button salirDeLaAplicacion;


    public void verOpciones()
    {
        volverAlPrincipio.gameObject.SetActive(!volverAlPrincipio.gameObject.active);
        salirDeLaAplicacion.gameObject.SetActive(!salirDeLaAplicacion.gameObject.active);
    }
    
    public void salir()
    {
        Application.Quit();
    }

    public void volver()
    {
        SceneManager.LoadScene("CarElection");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
