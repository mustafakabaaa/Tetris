using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;
public class GameMAnager : MonoBehaviour
{
    Spawner spawner;
    BoardManager boardManager;
    ShapeManager aktifSekil;
    

    public bool gameOver;
    [Header("Sayaclar")]
    //Scrool Bar oluþturur.
    [Range(0.02f, 1f)]
    [SerializeField] private float asagiInmeSuresi = .5f;
    private float asagiInmeSayac;
    private float asagiInmeLevelSayac;
    [Range(0.02f, 1f)]
    [SerializeField] float sagSolBasmaSuresi = 0.25f;
    private float sagSolBasmaSayac;
    [Range(0.02f, 1f)]
    [SerializeField] float sagSolDonmeSuresi = 0.25f;
    private float sagSolDonmeSayac;
    [Range(0.02f, 1f)]
    [SerializeField] float asagýTusaBasmaSuresi = 0.025f;
    private float asagýTusaBasmaSayac;

    public GameObject gameoverPanel;

    ScoreManager scoreManager;

    TakipShapeManager takipShape;

    enum Direction { none,sol,sag,asagi,yukari}

    private Direction suruklemeYonu=Direction.none;
    private Direction suruklemeBitisYonu=Direction.none;

    private float sonrakiDokunmaZamaný;
    private float sonrakiSuruklemeZamani;

    [Range(0.05f, 1f)] public float minDokunmaZamani = 0.15f;
    [Range(0.05f, 1f)] public float minSuruklemeZamani = 0.3f;

    private bool dokunduMu = false;


    private void Start()
    {

       
        OyunaBaslaFNC();
    }

    private void OyunaBaslaFNC()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        boardManager = FindObjectOfType<BoardManager>();
        spawner = FindObjectOfType<Spawner>();
        takipShape = GameObject.FindObjectOfType<TakipShapeManager>();

        if (spawner)
        {
            if (aktifSekil == null)
            {
                aktifSekil = spawner.SekilOlusturFNC();

                aktifSekil.transform.position = VectoruIntYapFNC(aktifSekil.transform.position);
            }
        }
        if (gameoverPanel)
        {
            gameoverPanel.SetActive(false);
        }
        asagiInmeLevelSayac = asagiInmeSayac;
    }

    private void Update()
    {
        if (!boardManager || !spawner||!aktifSekil||gameOver||!scoreManager)
        {
            return;

        }
        GirisKontrolFNC();

    }
    private void LateUpdate()
    {
        if (takipShape)
        {
            takipShape.TakipShapElosturFNC(aktifSekil,boardManager);
        }
    }

    private void GirisKontrolFNC()
    {
        if (!boardManager.GecerliPosizyonaMi(aktifSekil))
        {
            YerlestiFNC();
        }
        
        if (!boardManager || !spawner || !aktifSekil)
        {
            return;
        }

        if ((Input.GetKeyDown("right") || Input.GetKeyDown("d")) ||
            (Input.GetKey("right") && Time.time > sagSolBasmaSayac) ||
            (Input.GetKey("d") && Time.time > sagSolBasmaSayac))
        {
            sagaHareketFNC();
        }
        
        else if ((Input.GetKeyDown("left") || Input.GetKeyDown("a")) ||
            (Input.GetKey("left") && Time.time > sagSolBasmaSayac) ||
            (Input.GetKey("a") && Time.time > sagSolBasmaSayac))
        {
            SolaHareketFNC();
        }
        
        else if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")))
        {
            DondurFNC();
        }
        
        else if (((Input.GetKey("s") && Time.time > asagýTusaBasmaSayac)) || Time.time > asagiInmeSayac)
        {
            asagiHareketFNC();
        }
        else if((suruklemeBitisYonu==Direction.sag && Time.time > sonrakiSuruklemeZamani) || 
            (suruklemeYonu == Direction.sag &&Time.time>sonrakiDokunmaZamaný))
        {
            sagaHareketFNC();
            sonrakiDokunmaZamaný=Time.time+minDokunmaZamani;
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            //suruklemeYonu= Direction.none;
            //suruklemeBitisYonu= Direction.none;
        }
        else if ((suruklemeBitisYonu == Direction.sol && Time.time > sonrakiSuruklemeZamani) || 
            (suruklemeYonu == Direction.sol) &&Time.time>sonrakiDokunmaZamaný)
        {
            SolaHareketFNC() ;
            sonrakiDokunmaZamaný = Time.time + minDokunmaZamani;
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            //suruklemeYonu = Direction.none;
            //suruklemeBitisYonu = Direction.none;
        }
        else if ((suruklemeBitisYonu==Direction.yukari&& Time.time>sonrakiSuruklemeZamani||dokunduMu)) {
            DondurFNC();
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            //suruklemeBitisYonu=Direction.none;
        }
        else if (suruklemeYonu == Direction.asagi &&Time.time>sonrakiDokunmaZamaný)
        {
            asagiHareketFNC();
            //suruklemeYonu = Direction.none;
        }
        suruklemeYonu = Direction.none;
        suruklemeBitisYonu = Direction.none;
        dokunduMu = false;
    }

    private void asagiHareketFNC()
    {
        asagýTusaBasmaSayac = Time.time + asagiInmeLevelSayac;
        asagýTusaBasmaSayac = Time.time + asagýTusaBasmaSuresi;
        if (aktifSekil)
        {

            aktifSekil.AsagiHareketPNC();


            if (!boardManager.GecerliPosizyonaMi(aktifSekil))
            {

                if (boardManager.DisariTasmismiFNC(aktifSekil))
                {
                    //aktifSekil.YukarýHareketPNC();
                    gameOver = true;
                    if (gameoverPanel)
                    {
                        gameoverPanel.SetActive(true);
                        SoundManager.instance.SesEfektiCilar(7);

                    }
                    SoundManager.instance.SesEfektiCilar(2);

                }
                else
                {

                    YerlestiFNC();
                    SoundManager.instance.SesEfektiCilar(5);



                }
                aktifSekil.YukarýHareketPNC();
                boardManager.SekliIzgaraIcineAlFNC(aktifSekil);
                if (spawner)
                {
                    aktifSekil = spawner.SekilOlusturFNC();
                    aktifSekil.transform.position = boardManager.VectoruIntYapFNC(aktifSekil.transform.position);
                }
            }
        }
        asagiInmeSayac = Time.time + asagiInmeSuresi;
    }

    private void DondurFNC()
    {
        // Yukarý hareket iþlemi (Döndürme)
        sagSolDonmeSayac = Time.time + sagSolDonmeSuresi;
        aktifSekil.SagaDonPNC();
        SoundManager.instance.SesEfektiCilar(1);

        if (!boardManager.GecerliPosizyonaMi(aktifSekil))
        {
            // Döndürme iþlemi sonucunda sýnýrlarý aþýyorsa, geri al ve önceki haline getir.
            aktifSekil.SolaDonPNC(); // Döndürme iþlemini geri al.
        }
    }

    private void SolaHareketFNC()
    {
        sagSolBasmaSayac = Time.time + sagSolBasmaSuresi;

        aktifSekil.SolaHareketPNC();
        if (!boardManager.GecerliPosizyonaMi(aktifSekil))
        {
            SoundManager.instance.SesEfektiCilar(3);
            aktifSekil.SagaHareketPNC();
        }
        else
        {
            SoundManager.instance.SesEfektiCilar(4);

        }
    }

    private void sagaHareketFNC()
    {
        sagSolBasmaSayac = Time.time + sagSolBasmaSuresi;
        aktifSekil.SagaHareketPNC();
        if (!boardManager.GecerliPosizyonaMi(aktifSekil))
        {
            SoundManager.instance.SesEfektiCilar(3);
            aktifSekil.SolaHareketPNC();
        }
        else
        {
            SoundManager.instance.SesEfektiCilar(4);

        }
    }

    private void YerlestiFNC()
    {
        sagSolBasmaSayac=Time.time;
        asagýTusaBasmaSayac = Time.time;
        sagSolDonmeSayac = Time.time;   
          
        aktifSekil.YukarýHareketPNC();

        boardManager.SekliIzgaraIcineAlFNC(aktifSekil);

        if (spawner)
        {

            aktifSekil = spawner.SekilOlusturFNC();


        }
        if (takipShape)
        {
            takipShape.ResetFNC();
           
        }
        boardManager.TumSatirlariTemizleFNC();
        if (boardManager.tamamlananSatir > 0)
        {
            scoreManager.SatirSkoru(boardManager.tamamlananSatir);

            if (boardManager.tamamlananSatir > 1)
            {
                if(scoreManager.LevelGecildiMi)
                {
                    SoundManager.instance.SesEfektiCilar(8);
                    asagiInmeLevelSayac = asagiInmeSuresi - Mathf.Clamp(((float)scoreManager.level-1)*0.01f,0.05f,1f);
                }
                if (boardManager.tamamlananSatir > 2)
                {
                    SoundManager.instance.VocalSesiCikar();
                }
                SoundManager.instance.VocalSesiCikar();
            }
            SoundManager.instance.SesEfektiCilar(6);

        }

    }

    Vector2 VectoruIntYapFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x),Mathf.Round( vector.y));
    }

    public void Rotation()
    {
        sagSolDonmeSayac = Time.time + sagSolDonmeSuresi;
        aktifSekil.SagaDonPNC();
        SoundManager.instance.SesEfektiCilar(1);

        if (!boardManager.GecerliPosizyonaMi(aktifSekil))
        {
            // Döndürme iþlemi sonucunda sýnýrlarý aþýyorsa, geri al ve önceki haline getir.
            aktifSekil.SolaDonPNC(); // Döndürme iþlemini geri al.
        }

    }

    void SurukleFNC(Vector2 suruklemeHareketi)
    {
        suruklemeYonu=YonuBelirleFNC(suruklemeHareketi);
    }
    void SurukleBittiFNC(Vector2 suruklemeHareketi)
    {
        suruklemeBitisYonu = YonuBelirleFNC(suruklemeHareketi);
    }
    private void OnEnable()
    {
        TouchManager.DragEvent += SurukleFNC;
        TouchManager.SwipeEvent += SurukleBittiFNC;
        TouchManager.TopEvent += TapFNC;

    }
    
    private void OnDisable()
    {
        TouchManager.DragEvent -= SurukleFNC;
        TouchManager.SwipeEvent -= SurukleBittiFNC;
        TouchManager.TopEvent -= TapFNC;

    }
    void TapFNC(Vector2 suruklemeHareket)
    {
        dokunduMu = true;
    }
    Direction YonuBelirleFNC(Vector2 suruklemeHareketi)
    {
        Direction suruklemeYonu = Direction.none;
        if (Mathf.Abs(suruklemeHareketi.x)>Mathf.Abs(suruklemeHareketi.y))
        {
            suruklemeYonu = (suruklemeHareketi.x >= 0) ? Direction.sag : Direction.sol;
        }
        else
        {
            suruklemeYonu = (suruklemeHareketi.y >= 0) ? Direction.yukari : Direction.asagi;
        }
        return suruklemeYonu;
    }
}
