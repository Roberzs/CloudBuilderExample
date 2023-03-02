using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private AudioClip backgroupClip;

    [SerializeField]
    private AudioClip doorbellClip;

    [SerializeField]
    private AudioClip dub1Clip;

    [SerializeField]
    private AudioClip dub2Clip;

    [SerializeField]
    private AudioClip world3dClip;

    [SerializeField]
    private GameObject worldGol;

    private void Awake()
    {
        
    }

    public void Update()
    {
        // 第一组测试
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 背景音乐
            audioManager.PlayBackgroundMusic(backgroupClip);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            audioManager.PauseBackgroundMusic();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioManager.StopBackgroundMusic();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            audioManager.UnPauseBackgroundMusic();
        }
        

        // 第二组测试
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 门铃声
            audioManager.PlayMultipleSound(doorbellClip);
        }

        // 第三组测试e
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioManager.PlaySingleSound(dub2Clip);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            audioManager.PlaySingleSound(dub1Clip);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            audioManager.PlayWorldSound(worldGol, world3dClip, true);
        }

    }
}
