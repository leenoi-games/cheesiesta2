using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private bool m_loadActiveScene = true;
    [SerializeField] private string m_nextSceneName;
    public void RestartGame() 
    {
        if (m_loadActiveScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(SceneManager.GetSceneByName(m_nextSceneName) != null)
        {
            SceneManager.LoadScene(m_nextSceneName);
        }
        else
        {
            Debug.LogWarning("Failed to load Scene with Name " + m_nextSceneName);
        }
    }
}