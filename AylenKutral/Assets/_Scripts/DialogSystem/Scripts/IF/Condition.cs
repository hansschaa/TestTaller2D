using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/Condition")]
public class Condition : RTS_ICoversation
{
    public RTS_TypeConversation type = RTS_TypeConversation.CONDITION;

    public List<BooleanValue> conditions;

    public RTS_ICoversation nextAreTrue;
    public RTS_ICoversation nextAreFalse;

    
    public bool IsTrue()
    {
        foreach (BooleanValue boolean in conditions)
        {
            if(!boolean.value)
            {
                return false;
            }
        }
        return true;
    }

    public override RTS_TypeConversation GetTypeCoversation()
    {
        return this.type;
    }
}
