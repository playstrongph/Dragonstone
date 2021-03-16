using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyBuffCommand : Command
{
    BuffComponent bc;
    public DestroyBuffCommand(BuffComponent bc)
    {
        this.bc = bc;
    }

    public override void StartCommandExecution()
    {   
        if(bc.buffVisualObject != null)
        GameObject.Destroy(bc.buffVisualObject);

        if(bc.buffAnimationObject != null)
        GameObject.Destroy(bc.buffAnimationObject);

        if(bc != null)
        GameObject.Destroy(bc);

        Command.CommandExecutionComplete();

    }

}
