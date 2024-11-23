using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int m_levelMaxLives = 4;
    
    [SerializeField] IntVariable m_cheeseCollected;
    [SerializeField] IntVariable m_maxCheese;
    [SerializeField] IntVariable m_lives;
    [SerializeField] IntVariable m_maxLives;
    [SerializeField] GameEvent m_WinEvent;
    [SerializeField] GameEvent m_LoseEvent;

    private int m_deathCounter;

    private void Start() 
    {
        m_deathCounter = 0;
        m_maxLives.SetValue(m_levelMaxLives);
        m_lives.SetValue(m_maxLives.GetValue());
        m_cheeseCollected.SetValue(0);
        m_maxCheese.SetValue(m_levelMaxLives);

        if(m_cheeseCollected != null)
        {
            m_cheeseCollected.onValueChanged.AddListener(delegate{UpdateCheese();});
        }
        if(m_lives != null)
        {
            m_lives.onValueChanged.AddListener(delegate{UpdateLives();});
        }
    }
    
    private void UpdateCheese()
    {
        if(m_cheeseCollected.GetValue() == m_lives.GetValue())
        {
            Win();
        }
    }
    private void Update() {
        if(m_lives.GetValue() == 0)
        {
            Lose();
            return;
        }
        m_maxCheese.SetValue(m_maxLives.GetValue());
        if(m_cheeseCollected.GetValue() == m_lives.GetValue())
        {
            Win();
        }
    }

    public void UpdateLives()
    {
        m_deathCounter++;
        if(m_lives.GetValue() == 0)
        {
            Lose();
            return;
        }
        m_maxCheese.SetValue(m_lives.GetValue());
    }

    private void Lose()
    {
        m_LoseEvent.Raise();
    }

    private void Win()
    {
        m_WinEvent.Raise();
    }
}
