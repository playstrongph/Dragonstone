using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSkillCooldownHeroQCommand : Command
{
    
    SkillComponent skill;
    int value;    

    GameObject Target;

    public UpdateSkillCooldownHeroQCommand(GameObject Target, SkillComponent skill, int value)
    {
        this.value = value;
        this.skill = skill;
        this.Target = Target;

    }

    public override void StartCommandExecution()
    {
        skill.UpdateSkillCooldown(value);
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }

}
