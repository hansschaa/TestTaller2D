using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/Act")]
public class RTS_Act : ScriptableObject
{
    public List<RTS_ICoversation> dialogs = new List<RTS_ICoversation>();
}
