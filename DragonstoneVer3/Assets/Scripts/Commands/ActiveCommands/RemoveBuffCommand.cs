using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RemoveBuffCommand : Command
{
    BuffComponent bc;
    
    public RemoveBuffCommand(BuffComponent bc)
    {
        this.bc = bc;
    }
    
    public override void StartCommandExecution()
    {
        //Remove Visual gameObject
        GameObject.Destroy(bc.buffVisualObject);

        if(bc.buffAnimationObject != null)
        GameObject.Destroy(bc.buffAnimationObject);

        GameObject.Destroy(bc);  
        
        Command.CommandExecutionComplete();

        
        
    
    }
}
