using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/Desition")]
public class RTS_Decision : RTS_ICoversation
{
    
    public RTS_TypeConversation type = RTS_TypeConversation.OPTIONS;

    public RTS_Actor actor;
    public string headerText;
    public List<RTS_Option> options;

    public RTS_Decision()
    {
        options = new List<RTS_Option>();
    }

    public override RTS_TypeConversation GetTypeCoversation()
    {
        return this.type;
    }
}
