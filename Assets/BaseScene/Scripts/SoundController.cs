using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    /** 自分のインスタンス*/
    public static SoundController me;

    /** オーディオソース*/
    private AudioSource []sources;

    /** 効果音リスト*/
    public enum SE
    {
        CLICK,
        COUNT
    };
    private AudioClip[] _ses;
    private string[] _seNames =
    {
        "Audio/click",
    };
    /** BGMリスト*/
    public enum BGM
    {
        FAMIPOP4,
        COUNT
    }
    private AudioClip[] _bgms;
    private string[] _bgmNames =
    {
        "Audio/famipop4",
    };

	// Use this for initialization
	void Start () {
        me = this;
        sources = GetComponents<AudioSource>();

        // サウンドリソース読み込み
        _ses = new AudioClip[(int)SE.COUNT];
        for (int i=0; i<(int)SE.COUNT;i++)
        {
            _ses[i] = Resources.Load<AudioClip>(_seNames[i]);
        }
        _bgms = new AudioClip[(int)BGM.COUNT];
        for (int i=0; i<(int)BGM.COUNT; i++)
        {
            _bgms[i] = Resources.Load<AudioClip>(_bgmNames[i]);
        }
	}
	
    /**
     * 指定の効果音を鳴らす
     */
    public static void Play(SE snd)
    {
        me.sources[0].PlayOneShot(me._ses[(int)snd]);
    }

    /**
     * BGMを再生
     */
     public static void PlayBGM(BGM bgm)
    {
        // 違う曲の場合、先の曲を停止してから、再生
        if (me.sources[1].clip != me._bgms[(int)bgm])
        {
            me.sources[1].Stop();
            me.sources[1].clip = me._bgms[(int)bgm];
            me.sources[1].Play();
        }
        else
        {
            // 設定済みの曲の場合、再生中のときは何もしない
            if (!me.sources[1].isPlaying)
            {
                me.sources[1].Play();
            }
        }
    }

    /**
     * BGMを停止
     */
    public static void StopBGM()
    {
        me.sources[1].Stop();
    }

    /**
     * BGMのボリュームを設定
     */
    public static void SetBGMVolume(float vol)
    {
        me.sources[1].volume = vol;
    }

    /**
     * 音を停止
     */
    public static void Stop()
    {
        me.sources[0].Stop();
    }
}
