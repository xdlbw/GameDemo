using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip CoinPick;
    public static AudioClip hello;
    public static AudioClip CaiXuKun;
    public static AudioClip Jump;
    public static AudioClip Basketball;
    public static AudioClip Habit;
    public static AudioClip JiNiTaiMei;
    public static AudioClip Baby;
    public static AudioClip NiGanMa;
    public static AudioClip Aiyo;
    public static AudioClip LiHai;
    public static AudioClip Ji;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CoinPick = Resources.Load<AudioClip>("CoinPick");
        hello = Resources.Load<AudioClip>("hello");
        CaiXuKun = Resources.Load<AudioClip>("caixukun");
        Jump = Resources.Load<AudioClip>("jump");
        Basketball = Resources.Load<AudioClip>("basketball");
        Habit = Resources.Load<AudioClip>("habit");
        JiNiTaiMei = Resources.Load<AudioClip>("jinitaimei");
        Baby = Resources.Load<AudioClip>("baby");
        NiGanMa = Resources.Load<AudioClip>("niganma");
        Aiyo = Resources.Load<AudioClip>("aiyo");
        LiHai = Resources.Load<AudioClip>("lihai");
        Ji = Resources.Load<AudioClip>("ji");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StopAudioClip()
    {
        audioSource.Stop();
    }

    public static void PlayCoinPickClip()
    {
        audioSource.PlayOneShot(CoinPick);
    }

    public static void PlayhelloClip()
    {
        audioSource.PlayOneShot(hello);
    }

    public static void PlayCaiXuKunClip()
    {
        audioSource.PlayOneShot(CaiXuKun);
    }

    public static void PlayJumpClip()
    {
        audioSource.PlayOneShot(Jump);
    }

    public static void PlayBasketBallClip()
    {
        audioSource.PlayOneShot(Basketball);
    }

    public static void PlayHabitClip()
    {
        audioSource.PlayOneShot(Habit);
    }

    public static void PlayJiNiTaiMeiClip()
    {
        audioSource.PlayOneShot(JiNiTaiMei);
    }

    public static void PlayBabyClip()
    {
        audioSource.PlayOneShot(Baby);
    }

    public static void PlayNiGanMaClip()
    {
        audioSource.PlayOneShot(NiGanMa);
    }

    public static void PlayAiyoClip()
    {
        audioSource.PlayOneShot(Aiyo);
    }

    public static void PlayLiHaiClip()
    {
        audioSource.PlayOneShot(LiHai);
    }

    public static void PlayJiClip()
    {
        audioSource.PlayOneShot(Ji);
    }

}
