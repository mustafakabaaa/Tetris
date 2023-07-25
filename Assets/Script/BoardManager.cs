using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
   [SerializeField] private Transform tilePrefab;
    public int yukseklik = 22;
    public int genislik = 10;
    private Transform[,] izgara;
    public int tamamlananSatir = 0;

    private void Awake()
    {
        izgara = new Transform[genislik, yukseklik];
        print(izgara.Length);
    }
    private void Start()
    {
        BosKareleriOlusturFNC();
    }
    
    bool BoarDIcýndeMiFNC(int x,int y)
    {

        return(x>=0&&x<genislik &&y>=0);

    }
    bool KareDolumu(int x, int y, ShapeManager shape)
    {
        return (izgara[x, y] != null && izgara[x, y].parent != shape.transform);
    }

    public bool GecerliPosizyonaMi(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectoruIntYapFNC(child.position);

            if (!BoarDIcýndeMiFNC((int)pos.x,(int)pos.y)){

                
                    return false;
                
                
                
            }
            if (pos.y < yukseklik)
            {
                if (KareDolumu((int)pos.x, (int)pos.y, shape))
                {

                    return false;
                    
                }
            }

           
        }
        return true;    
    }
    void BosKareleriOlusturFNC()
    {
        
        if (tilePrefab != null)
        {
            for (int Y = 0; Y < yukseklik; Y++)
            {
                for (int X = 0; X < genislik; X++)
                {
                    Transform tile = Instantiate(tilePrefab, new Vector3(X, Y, 0), Quaternion.identity);
                    tile.name = "x" + X.ToString() + "," + "y" + Y.ToString();
                    tile.transform.parent = transform;
                }
            }
        }
        else
        {
            print("TilePrefab girilmedi");
        }
    }
    
    bool SatirTamamlandiMiFNC(int y)
    {
        for(int x = 0;x < genislik; ++x)
        {
            if (izgara[x,y] == null)
            {
                return false;
            }
        }
        return true;

    }
    void satiriTemizleFNC(int y)
    {
        for( int x = 0;x< genislik; ++x)
        {
            if (izgara[x,y] != null)
            {
                Destroy(izgara[x, y].gameObject);
            }
            izgara[x,y] = null;
        }
    }
    private void BirSatirAsagiIndýr(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x,y] != null)
            {
                izgara[x,y - 1] = izgara[x, y];
                izgara[x,y]=null;
                izgara[x,y - 1].position += Vector3.down;

            }
        }

    }
    void TumsatirlariAsagiIndýr( int BaslangicY)
    {
        for (int i = BaslangicY; i < yukseklik; ++i)
        {
            BirSatirAsagiIndýr(i);
        }
    }
    public void TumSatirlariTemizleFNC()
    {
        tamamlananSatir = 0;
        for(int y = 0; y < yukseklik; y++)
        {
            if(SatirTamamlandiMiFNC(y))
            {
                tamamlananSatir++;
                satiriTemizleFNC(y);
                TumsatirlariAsagiIndýr(y + 1);
                y--;
            }
        }
    }
    public Vector2 VectoruIntYapFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public void SekliIzgaraIcineAlFNC(ShapeManager shape) {
        if (shape == null) { return; }
            
            
            foreach (Transform child in shape.transform)
            {
              Vector2 pos=  VectoruIntYapFNC(child.position);

              izgara[(int)pos.x,(int)pos.y]=child ;

          
            }

        
    
    }
    public bool DisariTasmismiFNC(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y>= yukseklik - 1)
            {
                return true;
            }
        }
        return false;
    }
}
