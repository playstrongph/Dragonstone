using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillPreviewHeroQCommand : Command
{
    
    SkillHoverPreview skillHoverPreview;
    GameObject Target;

    public ShowSkillPreviewHeroQCommand(GameObject Target, SkillHoverPreview skillHoverPreview)
    {
        this.skillHoverPreview = skillHoverPreview;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        skillHoverPreview.ShowPreview();
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }

}
