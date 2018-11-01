using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class RTS_ICoversation : ScriptableObject
{
    public Sprite scenario;
    public abstract RTS_TypeConversation GetTypeCoversation();
}

[System.Serializable]
public enum RTS_TypeConversation
{
    DIALOG,
    OPTIONS,
    END,
    CHOOSE,
    CONDITION
}