using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreateFloatingTextCommand : Command
{
    
    string floatingText;
   GameObject target;
    Color textColor;

    float animationDelay = 0.15f;

    float yOffset = 2.5f;

    public CreateFloatingTextCommand(string floatingText, GameObject target, Color textColor)
    {
        this.floatingText = floatingText;
        this.target = target;
        this.textColor = textColor;
    }

    public override void StartCommandExecution()
    {
       
            GameObject newFloatingText = GameObject.Instantiate(GlobalSettings.Instance.FloatingTextPrefab, target.transform.position, Quaternion.identity) as GameObject;   

            newFloatingText.transform.SetParent(target.transform);

            Text FloatingText = newFloatingText.GetComponentInChildren<Text>();

            FloatingText.text = floatingText;
            FloatingText.color = textColor;

            float newY = target.transform.position.y + yOffset;

            float animTime = newFloatingText.GetComponent<AutoSelfDestruct>().timer;

            Vector3 newPosition = new Vector3(target.transform.position.x, newY, target.transform.position.z);

            newFloatingText.transform.DOLocalMoveY(yOffset, animTime);

            var s = DOTween.Sequence();

            //animation delay prevents text from sticking together, making it readable for the player

            s.AppendInterval(animationDelay).OnComplete(()=>{
                Command.CommandExecutionComplete();    
            });
            
            
    
    }

}
