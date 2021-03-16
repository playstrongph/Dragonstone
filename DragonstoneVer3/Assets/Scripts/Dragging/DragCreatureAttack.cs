using UnityEngine;
using System.Collections;

public class DragCreatureAttack : DraggingActions {

    // reference to the sprite with a round "Target" graphic
    private SpriteRenderer sr;
    // LineRenderer that is attached to a child game object to draw the arrow
    private LineRenderer lr;
    // reference to WhereIsTheCardOrCreature to track this object`s state in the game
    //private WhereIsTheCardOrCreature whereIsThisCreature;
    // the pointy end of the arrow, should be called "Triangle" in the Hierarchy
    private Transform triangle;
    // SpriteRenderer of triangle. We need this to disable the pointy end if the target is too close.
    private SpriteRenderer triangleSR;
    // when we stop dragging, the gameObject that we were targeting will be stored in this variable.
    private GameObject Target;
    // Reference to creature manager, attached to the parent game object
    //private OneCreatureManager manager;

    void Awake()
    {
        // establish all the connections
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponentInChildren<LineRenderer>();
        lr.sortingLayerName = "Above Everything";
        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();

        //manager = GetComponentInParent<OneCreatureManager>();
        //whereIsThisCreature = GetComponentInParent<WhereIsTheCardOrCreature>();
    }

    public override bool CanDrag
    {
        get
        {   
            // TEST LINE: just for testing 
            // return true;

            // we can drag this card if 
            // a) we can control this our player (this is checked in base.canDrag)
            // b) creature "CanAttackNow" - this info comes from logic part of our code into each creature`s manager script
            
            return GetComponentInParent<HeroLogic>().isActive;

            //return base.CanDrag && manager.CanAttackNow;
            
        }
    }

    public override void OnStartDrag()
    {
        //whereIsThisCreature.VisualState = VisualStates.Dragging;
        // enable target graphic
        sr.enabled = true;
        // enable line renderer to start drawing the line.
        lr.enabled = true;

    }

    public override void OnDraggingInUpdate()
    {

        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction*2.3f).magnitude;
        if (notNormalized.magnitude > distanceToTarget)
        {
            // draw a line between the creature and the target
            lr.SetPositions(new Vector3[]{ transform.parent.position, transform.position - direction*2.3f });
            lr.enabled = true;

            // position the end of the arrow between near the target.
            triangleSR.enabled = true;
            triangleSR.transform.position = transform.position - 1.5f*direction;

            // proper rotarion of arrow end
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // if the target is not far enough from creature, do not show the arrow
            lr.enabled = false;
            triangleSR.enabled = false;
        }
            
    }

    public override void OnEndDrag()
    {
        
        Target = null;
        RaycastHit[] hits;
        // TODO: raycast here anyway, store the results in 
        hits = Physics.RaycastAll(origin: Camera.main.transform.position, 
            direction: (-Camera.main.transform.position + this.transform.position).normalized, 
            maxDistance: 30f) ;

        foreach (RaycastHit h in hits)
        {
            /*
            if ((h.transform.tag == "TopPlayer" && this.tag == "LowCreature") ||
                (h.transform.tag == "LowPlayer" && this.tag == "TopCreature"))
            {
                // go face
                Target = h.transform.gameObject;
            }
            else if ((h.transform.tag == "TopCreature" && this.tag == "LowCreature") ||
                    (h.transform.tag == "LowCreature" && this.tag == "TopCreature"))
            {
                // hit a creature, save parent transform
                Target = h.transform.parent.gameObject;
            }
            */

            if (this.gameObject != h.transform.gameObject)
                Target = h.transform.parent.gameObject;
            //fix bug that adds another raycast hit which is invalid
              
        }
/*
        bool targetValid = false;

        if (Target != null)
        {
            int targetID = Target.GetComponent<IDHolder>().UniqueID;
            //Debug.Log("Target ID: " + targetID);
            if (targetID == GlobalSettings.Instance.LowPlayer.PlayerID || targetID == GlobalSettings.Instance.TopPlayer.PlayerID)
            {
                // attack character
                Debug.Log("Attacking "+Target);
                Debug.Log("TargetID: " + targetID);
//                CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].GoFace();
                targetValid = true;

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;
            }
            //ORIGINAL
            //else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null)

            else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null && CreatureLogic.CreaturesCreatedThisGame[targetID].canBeAttacked)
            {
                // if targeted creature is still alive, attack creature
                targetValid = true;
                CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].AttackCreatureWithID(targetID);                

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;

                //Debug.Log("Attacking "+Target);
            }
            else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null && !CreatureLogic.CreaturesCreatedThisGame[targetID].canBeAttacked)
            {
                // if targeted creature is still alive, attack creature
                Debug.Log("Invalid Target: Attack a Taunt Creature");
                //Debug.Log("Attacking "+Target);

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;                
            }
                
        }

        if (!targetValid)
        {
            // not a valid target, return
            if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
            else
                whereIsThisCreature.VisualState = VisualStates.TopTable;
            whereIsThisCreature.SetTableSortingOrder();
        }

        // return target and arrow to original position
        transform.localPosition = Vector3.zero;
        sr.enabled = false;
        lr.enabled = false;
        triangleSR.enabled = false;
*/

        transform.localPosition = Vector3.zero;
        sr.enabled = false;
        lr.enabled = false;
        triangleSR.enabled = false;

        //if (Target != null) new HeroAttackCommand(GetComponentInParent<HeroLogic>(),Target.GetComponent<HeroLogic>()).AddToQueue();
        
        if (Target != null) //first check, for Raycasts
        {
            //new HeroAttackCommand(this.transform.parent.gameObject, Target).AddToQueue();
            //new HeroEndTurnCommand(GetComponentInParent<HeroLogic>()).AddToQueue();
            
            //Original attack
            //AttackSystem.Instance.AttackHero(this.transform.parent.gameObject, Target);

            //Reference this to the first skill in herologic
            //SkillSystem.Instance.UseSkill(GetComponentInParent<SkillComponent>(), Target);  

            //var FirstSkill = this.transform.parent.gameObject.GetComponent<HeroLogic>().skills[0];
            //SkillSystem.Instance.UseSkill(FirstSkill, Target);

            if(Target.GetComponent<HeroLogic>() != null)  //this is to consider Box Collider attached to SkillCard Object
            {
                if(Target.GetComponent<HeroLogic>().isValidTarget)
                {
                    this.transform.parent.gameObject.GetComponent<BasicAttack>().UseSkill(this.transform.parent.gameObject, Target);

                    //Transfer this to first skill
                    //TurnManager.Instance.EndTurn(this.transform.parent.gameObject);
                }else
                {
                    Debug.Log("Invalid Target: Taunt Hero in the Way");
                    VisualSystem.Instance.CreateFloatingText("Invalid Target", Target, Color.white);
                }

                


            }
            else 
            {
                Debug.Log ("Invalid target: Skill Card");
            }

            
            
            //AttackSystem.Instance.AttackHero(this.transform.parent.gameObject, Target);

            //SkillSystem.Instance.UseSkill(GetComponentInParent<SkillComponent>(), Target);
        }
        else{} 
        //Debug.Log ("Invalid target: No GameObject");


    }

    // NOT USED IN THIS SCRIPT
    protected override bool DragSuccessful()
    {
        return true;
    }


    //DS
        public override void OnCancelDrag()
    {}
}
