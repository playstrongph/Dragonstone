using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearAllGlowsCommand : Command
{
    List<GameObject> targets;

    public ClearAllGlowsCommand(List<GameObject> targets)
    {
        this.targets = targets;
    }

    public override void StartCommandExecution()
    {
        foreach(GameObject go in targets)
        {
            go.GetComponent<HeroLogic>().isValidTarget = false;
            go.GetComponent<CardManager>().ClearAllGlows();
        }

        Command.CommandExecutionComplete();
    }
}
