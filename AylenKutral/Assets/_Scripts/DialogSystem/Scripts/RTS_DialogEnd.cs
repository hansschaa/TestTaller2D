using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/End Dialog")]
public class RTS_DialogEnd : RTS_ICoversation
{
    public string path;
    public int act;

    public override RTS_TypeConversation GetTypeCoversation()
    {
        return RTS_TypeConversation.END;
    }

    public void GoTo()
    {
        RTS_GameManager.Instance.stage = act;
        RTS_GameManager.Instance.LoadScene(path);
    }
}
