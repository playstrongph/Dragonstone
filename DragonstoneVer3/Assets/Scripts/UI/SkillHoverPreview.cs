using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillHoverPreview : MonoBehaviour
{
    public GameObject previewGameObject;  
	
	public float scaleIncreaseMutliplier = 30f;
    //public float scaleDecreaseMultiplier = 0.2f;

    public float skillPreviewDuration = 3f;
    public float skillDisplayDuration = 3f;

	
    public Transform skillpreviewOriginalPosition;

    Vector3 curPosition;
	Vector3 lastPosition;

    Vector3 PreviewCardScale =  new Vector3(1f,1f,1f);

    
 
    void OnMouseDown()
    {
        
        VisualSystem.Instance.ShowSkillPreview(this);
        
        
        //ShowPreview();

        curPosition = Input.mousePosition;
		lastPosition = curPosition;

    }

    

    void OnMouseUp()
    {
        
        VisualSystem.Instance.HideSkillPreview(this);
        
        //HidePreview();
      

    }

    //prevent the preview card from being dragged when attacking
	void Update()
	{
		curPosition = Input.mousePosition;

		//if (previewGameObject.activeSelf && gameObject.GetComponent<DraggingActions>().CanDrag)
        if (previewGameObject.activeSelf)  //removed canDrag condition for passive skill previews 
		{
			if (curPosition != lastPosition)
			{
				previewGameObject.SetActive(false);
			}
		}
		lastPosition = curPosition;
	}


    
    // OTHER METHODS

    //call this in the command
    public void ShowPreview(){

        
        
        Sequence s = DOTween.Sequence();              

            
        previewGameObject.SetActive(true); 
        
        //previewGameObject.transform.DOMove(GlobalSettings.Instance.skillPreviewLocation.transform.position, skillPreviewDuration);
        //previewGameObject.transform.DOScale(PreviewCardScale*scaleIncreaseMutliplier, skillPreviewDuration);  
        
        
        previewGameObject.transform.position = GlobalSettings.Instance.skillPreviewLocation.transform.position;
        previewGameObject.transform.localScale = previewGameObject.transform.localScale*scaleIncreaseMutliplier;

    }

    public void HidePreview()
    {
        previewGameObject.transform.position = skillpreviewOriginalPosition.position;
        previewGameObject.transform.localScale = PreviewCardScale;        

            previewGameObject.SetActive(false);  

            //previewGameObject.transform.DOMove(skillpreviewOriginalPosition.position, skillPreviewDuration);
            //previewGameObject.transform.DOScale(PreviewCardScale, skillPreviewDuration);       

    }
    
    public void HideSkillDisplay()
    {
        //previewGameObject.transform.position = skillpreviewOriginalPosition;
        //previewGameObject.transform.localScale = previewGameObject.transform.localScale/scaleMultiplier;

        Sequence s = DOTween.Sequence();

        s.AppendInterval(skillDisplayDuration);
        s.AppendCallback(()=>{

            previewGameObject.SetActive(false);

        });

        s.AppendCallback(()=>{

            previewGameObject.transform.DOMove(skillpreviewOriginalPosition.position, skillPreviewDuration);
            previewGameObject.transform.DOScale(PreviewCardScale, skillPreviewDuration);

        });

       

        

    }
}
