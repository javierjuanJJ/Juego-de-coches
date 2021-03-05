using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCar : MonoBehaviour
{

    public GameObject[] listCars;
    [HideInInspector]
    public Conducir conducir;

    public static Vector3 POSITION_INITIAL = new Vector3(0, 6, 0);
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject car = Instantiate(listCars[PlayerPrefs.GetInt("car")], POSITION_INITIAL, Quaternion.identity);
        conducir = car.GetComponent<Conducir>();
        conducir.car = car;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
