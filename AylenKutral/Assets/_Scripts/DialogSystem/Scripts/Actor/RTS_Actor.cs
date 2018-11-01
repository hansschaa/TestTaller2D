using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog System/Actor")]
[System.Serializable]
public class RTS_Actor : ScriptableObject
{
    public string actorName = "Default name";
    public Sprite[] expressions = new Sprite[6];

    public enum Expression
    {
        NORMAL,
        HAPPY,
        SAD,
        ANGRY,
        SURPRISED,
        ASHAMED //avergonzado
    }
}

