using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuManager : MonoBehaviour
{
    [SerializeField] string levelName;

    public void StartHost()
    {
        SceneManager.LoadScene(levelName);

        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host Conectado");
        }
        else
        {
            Debug.Log("Error al conectar Host");
        }
    }

    public void StartServer()
    {
        SceneManager.LoadScene(levelName);

        if (NetworkManager.Singleton.StartServer())
        {
            Debug.Log("Servidor Conectad");
        }
        else
        {
            Debug.Log("Error al conectar Servidor");
        }
    }

    public void StartClient()
    {
        SceneManager.LoadScene(levelName);

        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Cliente Conectad");
        }
        else
        {
            Debug.Log("Error al conectar Cliente");
        }
    }
}
