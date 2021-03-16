using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSkill : SkillComponent
{
    // Start is called before the first frame update
    

    CoroutineTree tree = new CoroutineTree();

    public override void UseSkill(GameObject Attacker, GameObject Target)
    {
        tree.Start();
        tree.AddRoot(UseSkillCoroutine(Attacker, Target));

    }

    IEnumerator UseSkillCoroutine(GameObject Attacker, GameObject Target)
    {

        VisualSystem.Instance.CreateFloatingText("Hi Raynor!", Attacker, Color.yellow);        

        //tree.AddCurrent(AddBuff(Attacker, Target, BuffName.RESURRECT, 2, tree));

        //tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.POISON, 2, tree));

        //tree.AddCurrent(AddBuff(Attacker, Target, BuffName.HASTE, 2, tree));

        //tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.CENSOR, 2, tree));

        tree.AddCurrent(DealDamage(Attacker, Target, 300, tree));

        //tree.AddCurrent(AddBuff(Attacker, Target, BuffName.IMMUNITY, 2, tree));        

        //tree.AddCurrent(ReesetSkillCooldownAfterUse(Attacker, Target, tree));

        tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
}
