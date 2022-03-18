using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funcion_datos : MonoBehaviour
{
    public float PPM;
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
        while (true)
        {
            GameObject sensor = GameObject.FindGameObjectWithTag("Sensor");
            GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");

            int sensorX = Random.Range(-5, 5);
            int sensorY = Random.Range(-3, 3);

            sensor.transform.position = new Vector3(sensorX, sensorY, sensor.transform.position.z);

            mesh.transform.position = new Vector3(sensor.transform.position.x, sensor.transform.position.y, sensor.transform.position.z);

            yield return new WaitForSeconds(5);
        }
    }
    private IEnumerator meshColor()
    {
        GameObject mesh = GameObject.FindGameObjectWithTag("Mesh");
        while (true)
        {
            PPM = Random.Range(1, 3);
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
