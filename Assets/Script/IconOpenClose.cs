using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOpenClose : MonoBehaviour
{
    public Sprite acikicon;
    public Sprite kapaliicon;
    private Image iconImage;
    public bool varsayilanIcınDurumu = true;


    private void Start()
    {
        
        iconImage = GetComponent<Image>();
        if(varsayilanIcınDurumu)
        {
            iconImage.sprite=acikicon;

        }
        else
        {
            iconImage.sprite=kapaliicon;    
        }
    }
    public void iconKapatma( bool iconDurum)
    {
        if(!iconImage|| !acikicon || !kapaliicon)
        {
            return;
        }
        iconImage.sprite=(iconDurum)?acikicon : kapaliicon;
    }
}
