using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
    public float baslangicAlpha = 1f;
    public float bitisAlpha = 0f;

    public float beklemeSuresi = 0f;
    public float fadeSuresi = 1f;

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = beklemeSuresi;
        StartCoroutine(FadeRotutineFNC());
    }
    IEnumerator FadeRotutineFNC()
    {
        yield return new WaitForSeconds(beklemeSuresi);
        GetComponent<CanvasGroup>().DOFade(bitisAlpha, fadeSuresi);

    }
}
