using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score=0;
    private int satirlar;
    public int level=1;
    public int seviyedekiSatirSayisi = 5;

    private int minSatir = 1;
    private int maxSatir = 4;


    public TextMeshProUGUI satirTXT;
    public TextMeshProUGUI levelTXT;
    public TextMeshProUGUI skorTXT;

    public bool LevelGecildiMi = false;

    private void Start()
    {
        ResetFNC();
    }
    private void ResetFNC()
    {
        level = 1;
        satirlar = seviyedekiSatirSayisi * level;
        TExtGuncelleFNC();
    }
    public void SatirSkoru(int n)
    {
        LevelGecildiMi = false;

        n=Mathf.Clamp(n, minSatir, maxSatir);
        
        switch(n)
        {
            case 1:
                score += 30*level; break;
            case 2:
                score += 55* level; break;
            case 3:
                score += 130 * level; break;
            case 4:
                score += 420 * level; break;
        }
        satirlar -= n;
       
        if (satirlar <= 0)
        {
            LevelArttýrmaFNC();
        }
        TExtGuncelleFNC();
    }
    public void TExtGuncelleFNC() {
        if (skorTXT)
        {
            skorTXT.text = BasaSifiEkleme(score,5);
        }
        if(levelTXT)
        {
            levelTXT.text=level.ToString();
        }
        if (satirTXT)
        {
            satirTXT.text=satirlar.ToString();
        }

    }
    string BasaSifiEkleme(int skor,int rakamSayisi)
    {
        string  skorSTR=skor.ToString();
        while(skorSTR.Length <rakamSayisi)
        {
            skorSTR = "0" + skorSTR;
        }
        return skorSTR;
    }

    public void LevelArttýrmaFNC() 
    {
        level++;
        satirlar = seviyedekiSatirSayisi * level;
        LevelGecildiMi = true;
    }
     
}
