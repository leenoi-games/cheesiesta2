using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class RestartButton : NetworkBehaviour
{
    public void RestartGame() 
    {
    	//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        NetworkManager.SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}