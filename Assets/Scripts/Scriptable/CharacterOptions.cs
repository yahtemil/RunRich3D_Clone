using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Setting", order = 0)]
public class CharacterOptions : ScriptableObject
{
    public int characterValue;
    public string characterName;
    public Color characterColor;
    public string animationName;

}
