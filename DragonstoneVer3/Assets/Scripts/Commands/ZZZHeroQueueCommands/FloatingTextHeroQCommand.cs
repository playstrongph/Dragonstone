using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingTextHeroQCommand : Command
{
    
    string floatingText;
   GameObject target;
    Color textColor;

    float yOffset = 2.5f;

    float textDelay = 0.2f;

    //float k = 0.5f;   //time scaler

    public FloatingTextHeroQCommand(string floatingText, GameObject target, Color textColor)
    {
        this.floatingText = floatingText;
        this.target = target;
        this.textColor = textColor;
    }

    public override void StartCommandExecution()
    {
       
       Debug.Log("HeroQueue Start Command Execution");
       
       GameObject newFloatingText = GameObject.Instantiate(GlobalSettings.Instance.FloatingTextPrefab, target.transform.position, Quaternion.identity) as GameObject;   

       newFloatingText.transform.SetParent(target.transform);

       

       Text FloatingText = newFloatingText.GetComponentInChildren<Text>();

      

       FloatingText.text = floatingText;
       FloatingText.color = textColor;

       float newY = target.transform.position.y + yOffset;

       float animTime = newFloatingText.GetComponent<AutoSelfDestruct>().timer;

       Vector3 newPosition = new Vector3(target.transform.position.x, newY, target.transform.position.z);


       Sequence s = DOTween.Sequence();       

       //s.AppendInterval(textDelay);
       //s.PrependInterval(textDelay);
       //s.Append(newFloatingText.transform.DOMove(newPosition, animTime));
       //newFloatingText.transform.DOMove(newPosition, animTime);

       newFloatingText.transform.DOLocalMoveY(yOffset, animTime);
       

       s.AppendInterval(textDelay).OnComplete(()=>{target.GetComponent<HeroQueue>().CommandExecutionComplete();});

        

        //Command.CommandExecutionComplete();





        
    }

}
