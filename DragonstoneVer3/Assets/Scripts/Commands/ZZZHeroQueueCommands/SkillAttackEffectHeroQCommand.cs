using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillAttackEffectHeroQCommand : Command
{
    
    GameObject Attacker;
    GameObject Target;

    public SkillAttackEffectHeroQCommand(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        GameObject specialEffect = GameObject.Instantiate(GlobalSettings.Instance.specialEffects[1], Attacker.transform.position, Quaternion.identity) as GameObject;

         Sequence s = DOTween.Sequence();
        s.AppendInterval(0.5f);
        s.Append(specialEffect.transform.DOMove(Target.transform.position, 0.5f));
        
        s.AppendInterval(0.2f)
        .OnStepComplete(()=> GameObject.Destroy(specialEffect))
        .OnComplete(()=>{ Target.GetComponent<HeroQueue>().CommandExecutionComplete();});


    }


}
