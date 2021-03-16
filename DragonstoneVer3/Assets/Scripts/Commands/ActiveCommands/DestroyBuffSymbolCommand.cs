using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DestroyBuffSymbolCommand : Command
{

   //Note:  NO Internal delay or animations required for this command.
   //Creates the BuffCard on top of the hero

   BuffComponent bc;
   
  

   public DestroyBuffSymbolCommand(BuffComponent bc)
   {
       this.bc = bc;
      
   }

 

    public override void StartCommandExecution()
    {
            
        if(bc.buffVisualObject != null)
        GameObject.Destroy(bc.buffVisualObject); //this should be a command 

        if(bc.buffAnimationObject != null)
        GameObject.Destroy(bc.buffAnimationObject); //this should be a command

        Command.CommandExecutionComplete();
    }




}
