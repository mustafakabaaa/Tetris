using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakipShapeManager : MonoBehaviour
{
   private ShapeManager takipShape=null;

    private bool zemineDegdimi=false;

    public Color color=new Color(1f,1f,1f,.2f);

    public void TakipShapElosturFNC(ShapeManager gercekShape,BoardManager board)
    {
        if (!takipShape)
        {
            takipShape=Instantiate(gercekShape,gercekShape.transform.position,gercekShape.transform.rotation) as ShapeManager ;
            takipShape.name = "TakipShape";


            SpriteRenderer[] tumSprite=takipShape.GetComponentsInChildren<SpriteRenderer>(); 

            foreach (SpriteRenderer spriteRenderer in tumSprite) 
            { spriteRenderer.color = color; }

        }
        else
        {
            takipShape.transform.position = gercekShape.transform.position;
            takipShape.transform.rotation = gercekShape.transform.rotation;
        }

        zemineDegdimi = false;
       
        while (!zemineDegdimi)
        {
            takipShape.AsagiHareketPNC();
            if(!board.GecerliPosizyonaMi(takipShape))
            {
                takipShape.YukarýHareketPNC();
                zemineDegdimi = true;
            }
        }
    }
    public void ResetFNC()
    {
        Destroy(takipShape.gameObject);
        print("destryr");
    }
}
