
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Spawner : MonoBehaviour
{
    public int sekilNumber;
    public int SpawnPosition;
    [SerializeField] private ShapeManager[] tumSekiller;
    
    [SerializeField] private Image[] sekilImage=new Image[2];
    
    private ShapeManager[] siradakiSekil=new ShapeManager[2];

    public ShapeManager SekilOlusturFNC()
    {
        ShapeManager sekil = null;

        sekil=siradakiSekliAlfNC();
        sekil.gameObject.SetActive(true);
        sekil.transform.position=transform.position;

        if( sekil != null)
        {
            
            return sekil;
        }
        else {
            print("dizibos");
            return null; 
        }
        
    }
    private void Awake()
    {
        
        HepsiniNullYapFNC();
    }
    ShapeManager RastgeleSekilOlusturFNC()
    {
        int randomSeki=Random.Range(0,tumSekiller.Length);
        if( tumSekiller[randomSeki])
        {
            return tumSekiller[randomSeki];
        }
        else
        {
            return null;
        }
    }
    IEnumerator sekilImageAcRoutine()
    {
        for(int i=0;i<sekilImage.Length;i++)
        {
            sekilImage[i].GetComponent<CanvasGroup>().alpha = 0;
            sekilImage[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }
        yield return new WaitForSeconds(.1f);


        //sýrayla görüntü açmak.
        int sayac = 0;
        while(sayac < sekilImage.Length)
        {
            sekilImage[sayac].GetComponent<CanvasGroup>().DOFade(1, .6f);
            sekilImage[sayac].GetComponent<RectTransform>().DOScale(1, .6f).SetEase(Ease.OutBack);

            sayac++;

            yield return new WaitForSeconds(.4f);
        }

      
    }
    void HepsiniNullYapFNC()
    {
        for(int i=0;i<siradakiSekil.Length;i++)
        {
            siradakiSekil[i] = null;
        }
        SirayiDoldurFNC();
    }
    void SirayiDoldurFNC()
    {
        for (int i = 0; i < siradakiSekil.Length; i++)
        {
            if (!siradakiSekil[i] )
            {
                siradakiSekil[i]=Instantiate(RastgeleSekilOlusturFNC(),transform.position,Quaternion.identity) as ShapeManager;
                siradakiSekil[i].gameObject.SetActive(false);
                sekilImage[i].sprite = siradakiSekil[i].shapeSekil;
            }
        }
        StartCoroutine(sekilImageAcRoutine());
    }
    ShapeManager siradakiSekliAlfNC()
    {
        ShapeManager sonrakisekil = null;
        if (siradakiSekil[0])
        {
            sonrakisekil = siradakiSekil[0];
        }
        for(int i = 1; i < siradakiSekil.Length; i++)
        {
            siradakiSekil[i - 1] = siradakiSekil[i];
            sekilImage[i - 1].sprite = siradakiSekil[i-1].shapeSekil;
        }
        siradakiSekil[siradakiSekil.Length-1] = null; 
        
        SirayiDoldurFNC();

        return sonrakisekil;

    }
    
        
        
        
        
        
        
        
        //int I = 3;
    //public void DogruSpawn()
    //{
    //    if( sekilNumber==0 ) {
    //        SpawnPosition = 18;
    //        //Vector3 tempVector=transform.position;
    //        //tempVector.y=SpawnPosition;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);
    //    }
    //    else if (sekilNumber == 1)
    //    {
    //        SpawnPosition = 18;
    //        //Vector3 tempVector = transform.position;
    //        //tempVector.y = SpawnPosition;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);

    //    }
    //    else if (sekilNumber == 2)
    //    {
    //        SpawnPosition = 18;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);

    //    }

    //    else if (sekilNumber == 3)
    //    {
    //        SpawnPosition = 18;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);

    //    }
    //    else if (sekilNumber == 4)
    //    {
    //        SpawnPosition = 18;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);

    //    }
    //    else if (sekilNumber == 5)
    //    {
    //        SpawnPosition = 18;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);
            
    //    }
    //    else if (sekilNumber == 6)
    //    {
    //        SpawnPosition = 18;
    //        transform.localPosition = new Vector3(transform.position.x, SpawnPosition, transform.position.z);

    //    }
    //}
}
