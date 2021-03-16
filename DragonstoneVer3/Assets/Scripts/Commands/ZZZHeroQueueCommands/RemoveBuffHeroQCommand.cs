using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBuffHeroQCommand : Command
{
    BuffComponent bc;
    GameObject Target;


    public RemoveBuffHeroQCommand(GameObject Target, BuffComponent bc)
    {
        this.bc = bc;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        //Remove Visual gameObject
        GameObject.Destroy(bc.buffVisualObject);

        if(bc.buffAnimationObject != null)
        GameObject.Destroy(bc.buffAnimationObject);

        //Remove buff component
        //GameObject.Destroy(bc);

        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }
}
