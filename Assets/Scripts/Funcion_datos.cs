using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Funcion_datos : MonoBehaviour
{
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
            GameObject sensor = GameObject.FindGameObjectWithTag("Sensor");
            GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");

            double sensorX = UnityEngine.Random.Range((float)-100.292350846394, (float)-100.307835379571);
            double sensorY = UnityEngine.Random.Range((float)25.7153017991021, (float)25.728408037838);

            float newSenX = (float)((float)(((sensorX - longMin) * rangeX) / rangolon) - (rangeX / 2));
            float newSenY = (float)(((sensorY - latMin) * 6) / rangoLat - 3);

            float senXround = (float)System.Math.Round(newSenX, 6);
            float senYround = (float)System.Math.Round(newSenY, 6);
            Debug.Log(senXround);
            Debug.Log(rangolon);

            sensor.transform.position = new Vector3(senXround, senYround, sensor.transform.position.z);

            mesh.transform.position = new Vector3(sensor.transform.position.x, sensor.transform.position.y, 0);

            yield return new WaitForSeconds(5);
        }
    }
    private IEnumerator meshColor()
    {
        GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");
        while (true)
        {
            //PPM = Random.Range(1, 3);
            PPM = 3;
            Debug.Log(PPM);

            if (PPM == 1)
            {
                mesh.GetComponent<MeshRenderer>().material = verde;
            }
            else if (PPM == 2)
            {
                mesh.GetComponent<MeshRenderer>().material = amarillo;
            }
            else
            {
                mesh.GetComponent<MeshRenderer>().material = rojo;
            }
            yield return new WaitForSeconds(5);
        }
    }
}