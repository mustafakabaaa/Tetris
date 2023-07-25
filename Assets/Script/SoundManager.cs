using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    //Soundmakera heryerden ulaþabilmek için yaoýlan iþlem.
   public static SoundManager instance;
    GameMAnager gameM=new GameMAnager();
    [SerializeField] private AudioClip[] musiccklips;
    [SerializeField]private AudioSource musicSource;
    [SerializeField] private AudioSource[] vocalClips;
    
    [SerializeField] AudioSource[] sesEfektleri;
    private AudioClip rastgeleMusicClip;

    public bool musicPlaying = true;
    public bool EfectPlaying = true;
   
    public IconOpenClose musicIcon;
    public IconOpenClose fxIcon;

    private UIManager UIManager;
    private void Awake()
    {
        instance = this;
    }
    AudioClip RastgeleClipSec(AudioClip[] clips)
    {
        AudioClip rastgeleClips = clips[Random.Range(0,clips.Length)];
        return rastgeleClips;
    }
    private void Start()
    {
        rastgeleMusicClip = RastgeleClipSec(musiccklips);
        BackgroundMusicCal(rastgeleMusicClip);

    }
    ShapeManager takipShape;

   
    public void SesEfektiCilar(int hangiSes)
    {
        if (EfectPlaying&& hangiSes<sesEfektleri.Length)
        {
            sesEfektleri[hangiSes].Stop();

            if (gameM.gameOver == false)
            {
                sesEfektleri[hangiSes].Play();

            }
            else
            {
                sesEfektleri[hangiSes].Stop();

            }
        }
    }

    public void BackgroundMusicCal(AudioClip musicClip)
    {
        if (!musicClip ||!musicSource||!musicPlaying)
        {
            return;
        }
        musicSource.clip = musicClip;

        musicSource.Play();

       
    }
    void MusicGuncelle()
    {
        if (musicSource.isPlaying!=musicPlaying)
        {
            if(musicPlaying)
            {
                rastgeleMusicClip = RastgeleClipSec(musiccklips);
                BackgroundMusicCal(rastgeleMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }
   
    
    public void AcKapaFNC() { 
    
        musicPlaying=!musicPlaying;
        MusicGuncelle();

        musicIcon.iconKapatma(musicPlaying);
    }
    public void EfektCalsinMi()
    {
        EfectPlaying = !EfectPlaying;
        fxIcon.iconKapatma(EfectPlaying);

    }
    public void VocalSesiCikar() {
        
        if(EfectPlaying)
        {
            AudioSource source = vocalClips[Random.Range(0, vocalClips.Length)];
            source.Stop();
            source.Play();
        }
        
        
    }
}
