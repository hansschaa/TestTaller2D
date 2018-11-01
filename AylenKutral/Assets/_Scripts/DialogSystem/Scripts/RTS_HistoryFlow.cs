using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/HistoryFlow")]
public class RTS_HistoryFlow : ScriptableObject
{
    public List<RTS_Act> acts = new List<RTS_Act>();
    public RTS_ICoversation actual;

    public void SetActual(int stage, int dialog)
    {
        actual = acts[stage].dialogs[dialog];
    }
}
