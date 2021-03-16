using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowTargetsCommand : Command
{
    List<GameObject> target;

    public ShowTargetsCommand(List<GameObject> target)
    {
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        foreach(GameObject go in target)
        {
            go.GetComponent<HeroLogic>().isValidTarget = true;
            go.GetComponent<CardManager>().ShowTargetGlow();
        }

        
        
         Command.CommandExecutionComplete();
    }
}
