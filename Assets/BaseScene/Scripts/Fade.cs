/**
 * 画面のフェードを管理する
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    /** 自分のインスタンス*/
    public static Fade fade;
    /** フェードの色*/
    public static Color Color { get; set; }
    /** フェードの状態*/
    public enum FADE
    {
        NONE,   // 何もしていない
        OUT,    // フェードアウト中
        IN      // フェードイン中
    }
    /** フェード用のイメージ*/
    private static Image fadeImage;
    /** フェード中フラグ*/
    public static bool IsFading { get; set; }

    // Use this for initialization
    void Start()
    {
        fade = this;
        fadeImage = GetComponent<Image>();
        Color = fadeImage.color;
        fadeImage.enabled = false;
        IsFading = false;
    }

    /**
     * フェードインかフェードアウトを、指定の秒数を実行
     */
    public static IEnumerator StartFade(FADE type, float time)
    {
        IsFading = true;
        float startTime = Time.time;
        Color nowColor = Color;
        nowColor.a = type == FADE.IN ? 1f : 0f;
        SoundController.SetBGMVolume(1f - nowColor.a);
        fadeImage.color = nowColor;

        fadeImage.enabled = true;
        while ((Time.time - startTime) <= time)
        {
            // フェードイン中
            float keika = (Time.time - startTime) / time;
            if (type == FADE.IN)
            {
                nowColor.a = 1f - keika;
            }
            else
            {
                // フェードアウト中
                nowColor.a = keika;
            }
            fadeImage.color = nowColor;
            // BGMのボリューム設定
            SoundController.SetBGMVolume(1f - nowColor.a);

            yield return null;
        }

        nowColor.a = type == FADE.IN ? 0f : 1f;
        fadeImage.color = nowColor;
        if (type == FADE.IN)
        {
            fadeImage.enabled = false;
        }

        IsFading = false;
    }

}
