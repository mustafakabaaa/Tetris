using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchManager : MonoBehaviour
{

    public delegate void DokunmaEventDelegate(Vector2 swipePos);
    public static event DokunmaEventDelegate DragEvent;
    [SerializeField] TextMeshProUGUI taniTXT1, taniTXT2;

    private Vector2 dokunmaHareketi;
    [Range(50, 250)]
    public int minDragUzakligi = 100;
    [Range(50, 250)]
    public int minSuruklemeUzakligi = 200;

    public int minSurukleme = 200;
    public bool taniKulllanilsinmi = false;
    public static event DokunmaEventDelegate SwipeEvent;
   
    private float tiklamaMaxSure = 0f;
    public float ekranaTiklamaSuresi = .1f;
    public static event DokunmaEventDelegate TopEvent;
    void TiklandiFNC()
    {
        if (TopEvent != null)
        {
            TopEvent(dokunmaHareketi);
        }
    }
    void SurrukleFNC()
    {

        if (DragEvent!=null)
        {
            DragEvent(dokunmaHareketi);
        }
    }
    void SurukleBittiEventFnc()
    {
        if (SwipeEvent!=null) {
            SwipeEvent(dokunmaHareketi);
        }
    }
    void TaniYazdirFNC(string text1,string text2)
    {
        taniTXT1.gameObject.SetActive(taniKulllanilsinmi);
        taniTXT2.gameObject.SetActive(taniKulllanilsinmi);
        if (text1!=null && text2!=null) { 
            taniTXT1.text = text1;
            taniTXT2.text = text2;
        }
    }
    string SuruklemeTaniFNC(Vector2 suruklemeHareekt)
    {
        string direction = "";
        if (Mathf.Abs(suruklemeHareekt.x)>Mathf.Abs(suruklemeHareekt.y))
        {
            direction = (suruklemeHareekt.x > 0) ? "sag":"sol";
        }
        else { direction = (suruklemeHareekt.y >= 0) ? "yukari" : "asagi";
        
        }
        return direction;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase==TouchPhase.Began)
            {
                dokunmaHareketi = Vector2.zero;
                tiklamaMaxSure = Time.time + ekranaTiklamaSuresi;
                TaniYazdirFNC("","");
            }
            else if (touch.phase==TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                dokunmaHareketi += touch.deltaPosition;
                if (dokunmaHareketi.magnitude>minDragUzakligi)
                {
                    SurrukleFNC();
                    TaniYazdirFNC("surukleme kontrol", dokunmaHareketi.ToString()+" "+ SuruklemeTaniFNC(dokunmaHareketi));
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if(dokunmaHareketi.magnitude > minSuruklemeUzakligi)
                {
                    SurukleBittiEventFnc();
                }
                else if(Time.time<tiklamaMaxSure)
                {
                    TiklandiFNC();
                }
                
            }
        }
    }
}
