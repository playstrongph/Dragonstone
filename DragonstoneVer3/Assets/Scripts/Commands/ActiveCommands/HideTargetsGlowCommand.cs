using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideTargetsGlowCommand : Command
{
    List<GameObject> target;
       public HideTargetsGlowCommand(List<GameObject> target)
    {
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        foreach(GameObject go in target)
        {
            go.GetComponent<HeroLogic>().isValidTarget = false;
            go.GetComponent<CardManager>().HideTargetGlow();
        }

         Command.CommandExecutionComplete(); 
    }
}
