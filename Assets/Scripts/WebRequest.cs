using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    public string url = "http://localhost:3000/registros";
    public string findId = "1";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetUsers());
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

    IEnumerator GetUsers()
    {
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

                foreach(var user in json.users)
                {
                    if (findId == user.idDispositivo)
                    {
                        Debug.Log(user.idSensor);
                    }
                }
            
            }
        }
    }
}
