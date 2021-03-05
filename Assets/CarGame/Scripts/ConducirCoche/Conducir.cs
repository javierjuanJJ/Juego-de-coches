using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Conducir : MonoBehaviour
{
    private float velocidad = 1000;
    private Camera camara;
    [HideInInspector] public GameObject car;
    private Button botonClaxon, delanteBoton, detrasBoton, derechaBoton, izquierdaBoton, rotarDerechaBoton, rotarIzquierdaBoton;
    private bool estaPresionado;

    public float horizontalInput;
    private float forwardInput;
    [HideInInspector]
    public AudioSource arrancarVehiculo;
    [HideInInspector]
    public AudioSource motorFuncionando;
    [HideInInspector]
    public AudioSource claxon;
    
    private bool isInPlane;
    
    private void Awake()
    {
        camara = FindObjectOfType<Camera>();
        isInPlane = true;
        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        Button[] botones = FindObjectsOfType<Button>();
        
        foreach (Button boton in botones)
        {
            switch (boton.name)
            {
                case "claxon" : botonClaxon = boton ; break;
            }
        }
        
       

        foreach (AudioSource sonido in sonidos)
        {
            switch (sonido.name)
            {
                case "arrancarCoche" : arrancarVehiculo = sonido ; break;
                case "claxon" : claxon = sonido; break;
                case "motorFuncionando" : motorFuncionando = sonido ; break;
            }
        }
        botonClaxon.onClick.AddListener(() => claxon.Play());
        arrancarVehiculo.Play();
        motorFuncionando.loop = true;
        motorFuncionando.Play();
        
    }

    private Vector3 m_movementVector;


    /// <summary>
    /// Gets all user input from a standalone device.
    /// </summary>
    private void GetInput_PC()
    {
        m_movementVector = car.transform.position;
        // Update the movement vector
        m_movementVector.x = Input.GetAxis("Horizontal");
        m_movementVector.y = 0.0f;
        m_movementVector.z = Input.GetAxis("Vertical");

        // Add force to the player's rigid body.
        
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad * m_movementVector.z);
        transform.Rotate(Vector3.up, m_movementVector.x * velocidad * Time.deltaTime);
        //car.GetComponent<Rigidbody>().AddForce(m_movementVector * velocidad * Time.deltaTime);
        //car.transform.Rotate(Vector3.up, m_movementVector.x * velocidad * Time.deltaTime);
        escribirEnPrefs();
    }

    /// <summary>
    /// Gets all user input from an Android device.
    /// </summary>
    private void GetInput_Android()
    {
        m_movementVector = car.transform.position;
        // Update the movement vector
        m_movementVector.x = Input.GetAxis("Mouse X");
        m_movementVector.y = 0.0f;
        m_movementVector.z = Input.GetAxis("Mouse Y");

        // Get the touch count
        if (Input.touchCount > 0)
        {
            // Update the movement vector
            m_movementVector.x += Input.touches[0].deltaPosition.x;
            m_movementVector.y = 0.0f;
            m_movementVector.z += Input.touches[0].deltaPosition.y;

            // Add force to the player's rigid body.
            transform.Translate(Vector3.forward * Time.deltaTime * 30 * m_movementVector.z);
            transform.Rotate(Vector3.up, m_movementVector.x * 30 * Time.deltaTime);
            escribirEnPrefs();
        }
    }

    private void escribirEnPrefs()
    {
        PlayerPrefs.SetFloat("PosX",car.transform.position.x);
        PlayerPrefs.SetFloat("PosY",car.transform.position.y);
        PlayerPrefs.SetFloat("PosZ",car.transform.position.z);
    }
    private Vector3 vectorPrefs()
    {
        return CreateCar.POSITION_INITIAL;
    }

    private void cambiarPosicionCamara()
    {
        camara.gameObject.transform.position = car.transform.position + new Vector3(0, 100, -7);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        isInPlane = isInPlaneMethod(other);
    }

    private void OnTriggerExit(Collider other)
    {
        isInPlane = false;
        //isInPlane = isInPlaneMethod(other);
    }

    private void OnTriggerStay(Collider other)
    {
        isInPlane = isInPlaneMethod(other);
    }

    private bool isInPlaneMethod(Collider other)
    {
        Debug.Log("Name " + other.gameObject.name);
        return other.gameObject.CompareTag("Bridge");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        if (isInPlane)
        {
            GetInput_PC();
        }
        else
        {
            car.transform.position = vectorPrefs();
            isInPlane = true;
        }
                
#elif UNITY_ANDROID
        
        if (isInPlane)
        {
            GetInput_Android();
        }
        else
        {
            car.transform.position = vectorPrefs();
            isInPlane = true;
        }
                
#endif
        cambiarPosicionCamara();
    }
}