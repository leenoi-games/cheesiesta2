using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject m_winUI;
    [SerializeField] GameObject m_loseUI;
    [SerializeField] GameObject m_inGameUI;
    [SerializeField] GameObject m_tutorialUI;

 
    private void Start() 
    {
        m_loseUI.SetActive(false);
        m_winUI.SetActive(false);
        m_inGameUI.SetActive(false);
        m_tutorialUI.SetActive(true);
    }

    public void EndTutorial()
    {
        m_loseUI.SetActive(false);
        m_winUI.SetActive(false);
        m_inGameUI.SetActive(true);
        m_tutorialUI.SetActive(false);
    }

    public void OnLose()
    {
        m_loseUI.SetActive(true);
        m_winUI.SetActive(false);
        m_inGameUI.SetActive(false);
        m_tutorialUI.SetActive(false);
    }

    public void OnWin()
    {
        m_loseUI.SetActive(false);
        m_winUI.SetActive(true);
        m_inGameUI.SetActive(false);
        m_tutorialUI.SetActive(false);
    }
}
