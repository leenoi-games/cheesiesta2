using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseState", menuName = "GamePlay/MouseState", order = 0)]
public class MouseState : ScriptableObject
{
    public float speed;
    public bool isLoud;
}
