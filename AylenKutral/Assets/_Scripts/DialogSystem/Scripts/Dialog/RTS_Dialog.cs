using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog System/Dialog")]
public class RTS_Dialog :  RTS_ICoversation
{
    public RTS_TypeConversation type = RTS_TypeConversation.DIALOG;
    public List<RTS_Sentence> sentences;

    public RTS_ICoversation next;

    public RTS_Dialog()
    {
        this.sentences = new List<RTS_Sentence>();
    }

    public override RTS_TypeConversation GetTypeCoversation()
    {
        return this.type;
    }


}

