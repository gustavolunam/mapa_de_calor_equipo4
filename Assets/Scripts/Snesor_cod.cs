using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Snesor_cod : MonoBehaviour
{
    public string url = "http://localhost:3000/registros";
    public string findId;
    public GameObject prefab_verde;
    public GameObject prefab_amarillo;
    public GameObject prefab_rojo;
    public Material verde;
    public Material amarillo;
    public Material rojo;

    double rangeX = 7.088776646;
    //double longMax = -100.292350845394;
    double longMin = -100.307835379571;
    // double latMax = 25.728408037838;
    double latMin = 25.7153017991021;
    double rangoLat = 0.013106238736;
    double rangolon = 0.015484533177;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Actualiza());
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class Users
    {
        public string idSensor, fechaSensor, horaSensor, coordenadasXSensor, coordenadasYSensor, PPM, idDispositivo, Interfaz_idInterfaz;
    }

    [System.Serializable]
    public class RootUsers
    {
        public Users[] users;
    }

    IEnumerator Actualiza()
    {
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Result is " + www.result);
                Debug.Log(www.error);
            }

            else
            {
                Debug.Log(www.responseCode);
                Debug.Log(www.downloadHandler.text);

                RootUsers json = JsonUtility.FromJson<RootUsers>(("{\"users\":" + www.downloadHandler.text + "}"));

                foreach (var user in json.users)
                {
                    if (findId == user.idDispositivo)
                    {
                        int idSensor = int.Parse(user.idSensor);
                        string fechaSensor = user.fechaSensor;
                        string horaSensor = user.horaSensor;
                        double sensorY = Convert.ToDouble(user.coordenadasXSensor);
                        double sensorX = Convert.ToDouble(user.coordenadasYSensor);
                        int PPM = int.Parse(user.PPM);
                        int idDispositivo = int.Parse(user.idDispositivo);
                        int Interfaz_idInterfaz = int.Parse(user.Interfaz_idInterfaz);

                        Debug.Log(sensorX);
                        Debug.Log(sensorY);

                        float newSenX = (float)((float)(((sensorX - longMin) * rangeX) / rangolon) - (rangeX / 2));
                        float newSenY = (float)(((sensorY - latMin) * 6) / rangoLat - 3);

                        float senXround = (float)System.Math.Round(newSenX, 6);
                        float senYround = (float)System.Math.Round(newSenY, 6);
                        Debug.Log(senXround);
                        Debug.Log(senYround);

                        transform.position = new Vector3(senXround, senYround, transform.position.z);

                        if (PPM < 50)
                        {
                            mesh[1].material = verde;
                        }

                        else if (PPM < 200)
                        {
                            mesh[1].material = amarillo;
                        }
                        else
                        {
                            mesh[1].material = rojo;
                        }

                        yield return new WaitForSeconds(20);

                        if (PPM < 50)
                        {
                            mesh[1].material = verde;
                            Instantiate(prefab_verde, transform.position, Quaternion.identity);
                        }

                        else if (PPM < 200)
                        {
                            mesh[1].material = amarillo;
                            Instantiate(prefab_amarillo, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            mesh[1].material = rojo;
                            Instantiate(prefab_rojo, transform.position, Quaternion.identity);
                        }
                    }
                }

            }
        }
    }
           
}