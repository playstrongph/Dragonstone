using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageEffect : MonoBehaviour
{
    
    public Image DamageImage;

    public CanvasGroup canvasGroup;

    public Text AmountText;

    public float animDuration = 0.12f;
    public float localScale = 1.5f;

    public int loopsCount = 4;

    public IEnumerator ShowDamageEffect()
    {
        // make this effect non-transparent
        canvasGroup.alpha = 1f;
        // wait for 1 second before fading

//DS

        //GameObject sfx_explosion = GameObject.Instantiate(SfxExplosion_1_Prefab, this.transform.position, Quaternion.identity) as GameObject;

        transform.DOScale(this.transform.localScale*localScale, animDuration).SetLoops(loopsCount, LoopType.Yoyo).SetEase(Ease.InOutQuad);


        yield return new WaitForSeconds(1f);
        // gradually fade the effect by changing its alpha value        

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(this.gameObject);



    }

    
}
