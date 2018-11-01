using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/Choose")]
public class RTS_Choose : RTS_ICoversation
{

    public RTS_TypeConversation type = RTS_TypeConversation.CHOOSE;

    public bool changeTo;

    public BooleanValue condition;

    public RTS_ICoversation next;

    protected RTS_Choose()
    {

    }

    public override RTS_TypeConversation GetTypeCoversation()
    {
        return this.type;
    }   

    
}
