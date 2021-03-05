using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotarVehiculo : MonoBehaviour
{
    private const int GRADES_MAXIM = 360;
    private int contGrades;
    public static GameObject car;
    public int numCar;

    private void Awake()
    {
        contGrades = 0;
        StartCoroutine("Fade");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    IEnumerator Fade() 
    {
        contGrades = contGrades == 360 ? contGrades = 0 : contGrades++;
        
        //transform.Rotate(new Vector3(transform.position.x,contGrades,transform.position.z));
        transform.Rotate(new Vector3(contGrades,transform.position.y,transform.position.z));
        
        Debug.Log("Fade");

        yield return new WaitForSeconds(0.1f);
        
        StartCoroutine("Fade");
        
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("car",numCar);
        SceneManager.LoadScene("RoadGame");
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
