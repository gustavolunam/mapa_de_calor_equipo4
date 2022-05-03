using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Snesor_cod : MonoBehaviour
{
    public GameObject prefab_verde;
    public GameObject prefab_amarillo;
    public GameObject prefab_rojo;
    public float PPM;
    public double rangeX = 7.088776646;
    public double longMax = -100.292350845394;
    public double longMin = -100.307835379571;
    public double latMax = 25.728408037838;
    public double latMin = 25.7153017991021;


    public Material verde;
    public Material amarillo;
    public Material rojo;
// Start is called before the first frame update
  void Start()
{
    //GameObject sensor = GameObject.FindGameObjectWithTag("Sensor");
    //GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");

    //mesh.transform.position = new Vector3(sensor.transform.position.x, sensor.transform.position.y, sensor.transform.position.z);
    
    StartCoroutine("pos_sensor");
    StartCoroutine("meshColor");
}

// Update is called once per frame
void Update()
{

}
private IEnumerator pos_sensor()
{
    double rangoLat = 0.013106238736;
    double rangolon = 0.015484533177;
    while (true)
    {
        //GameObject sensor = GameObject.FindGameObjectWithTag("Sensor");
        //GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");

        double sensorX = UnityEngine.Random.Range((float)-100.292350846394, (float)-100.307835379571);
        double sensorY = UnityEngine.Random.Range((float)25.7153017991021, (float)25.728408037838);

        float newSenX = (float)((float)(((sensorX - longMin) * rangeX) / rangolon) - (rangeX / 2));
        float newSenY = (float)(((sensorY - latMin) * 6) / rangoLat - 3);

        float senXround = (float)System.Math.Round(newSenX, 6);
        float senYround = (float)System.Math.Round(newSenY, 6);
        Debug.Log(senXround);
        Debug.Log(rangolon);

        transform.position = new Vector3(senXround, senYround, transform.position.z);

        //mesh.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        yield return new WaitForSeconds(20);
    }
}
private IEnumerator meshColor()
{
        //GameObject mesh = GameObject.find("Mesh");
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
    while (true)
    {
        PPM = UnityEngine.Random.Range(1, 3);
        //PPM = 3;
        Debug.Log(PPM);

        if (PPM == 1)
        {

            mesh[1].material = verde;
            Instantiate(prefab_verde, transform.position, Quaternion.identity);
            }
        else if (PPM == 2)
        {
            mesh[1].material = amarillo;
            Instantiate(prefab_amarillo, transform.position, Quaternion.identity);
            }
        else
        {
            mesh[1].material = rojo;
            Instantiate(prefab_rojo, transform.position, Quaternion.identity);
            }
        yield return new WaitForSeconds(20);
    }
}
}
