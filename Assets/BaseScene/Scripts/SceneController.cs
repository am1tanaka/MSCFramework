/**
 * シーンを制御するスクリプト
 * ベースシーンのゲームオブジェクトに設定する
 * 
 * @copyright 2017 YuTanaka@AmuseOne
 * 
 */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /** このインスタンス*/
    private static SceneController _controller;
    public static SceneController Controller
    {
        get
        {
            return _controller;
        }
    }
    /** シーンリスト*/
    public enum SCENES
    {
        NONE,
        TITLE,
        GAME,
        GAMEOVER,
        CLEAR,
        ENDING,
        COUNT
    }
    /** フェード秒数*/
    public float FADE_SECONDS = 0.5f;
    /** シーン名*/
    private string[] SCENE_NAMES =
    {
        "",
        "Title",
        "Game",
        "GameOver",
        "Clear",
        "AllClear"
    };
    /** 現在のシーン*/
    private static SCENES _nowScene = SCENES.NONE;
    public static SCENES NowScene
    {
        get
        {
            return _nowScene;
        }
    }
    /** 次のシーン*/
    private static SCENES _nextScene;
    /** シーンインスタンス*/
    private static BaseScene[] Scenes;

    /** シーン変更中*/
    private bool _isSceneChanging = false;
    public bool IsSceneChanging
    {
        get
        {
            return _isSceneChanging;
        }
    }

    /**
     * 次のシーンを設定する
     */
    public static void SetNextScene(SCENES sc)
    {
        _nextScene = sc;
    }

    // Use this for initialization
    void Start()
    {
        _controller = this;

        // シーンインスタンスの作成
        Scenes = new BaseScene[(int)SCENES.COUNT];
        for (int i = 0; i < (int)SCENES.COUNT; i++)
        {
            Scenes[i] = null;
        }
        Scenes[(int)SCENES.TITLE] = ScriptableObject.CreateInstance<TitleScene>() as BaseScene;

        // 最初のシーンを起動に設定
        _nextScene = SCENES.TITLE;

        _isSceneChanging = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 初期化処理
        StartCoroutine("_initScene");

        // シーンの初期化中でなければ、更新処理を呼び出す
        if (!_isSceneChanging && (Scenes[(int)NowScene] != null))
        {
            // 更新処理
            Scenes[(int)NowScene].Update();
        }
    }

    /** シーンの初期化処理*/
    IEnumerator _initScene()
    {
        if ((_nextScene == SCENES.NONE) || _isSceneChanging)
        {
            yield break;
        }

        // シーンの切り替え発生
        _isSceneChanging = true;
        yield return StartCoroutine(Fade.StartFade(Fade.FADE.OUT, _nowScene == SCENES.NONE ? 0f : FADE_SECONDS));

        // 現在のシーンの後処理を呼び出す
        if (Scenes[(int)_nowScene] != null)
        {
            Scenes[(int)_nowScene].Destroy();
        }
        // 現在のシーンをアンロード
        yield return UnloadScene();

        // 移行処理
        _nowScene = _nextScene;
        _nextScene = SCENES.NONE;

        // 次のシーンを読み込む
        yield return LoadScene();
        // 次のシーンの更新処理を実行
        if (Scenes[(int)_nowScene] != null)
        {
            Scenes[(int)_nowScene].Init();
        }

        // フェードイン
        yield return StartCoroutine(Fade.StartFade(Fade.FADE.IN, FADE_SECONDS));

        // 初期化完了
        _isSceneChanging = false;
    }

    /**
     * 現在のシーンがあったら案ロードする
     */
    private IEnumerator LoadScene()
    {
        // ゲームシーンは読み込まない
        if ((_nowScene != SCENES.GAME)
            && (_nowScene != SCENES.NONE))
        {
            // 読み込みを開始して、完了するのを待つ
            yield return SceneManager.LoadSceneAsync(SCENE_NAMES[(int)_nowScene], LoadSceneMode.Additive);
        }
    }

    /**
     * 現在のシーンを解放する
     */
    private IEnumerator UnloadScene()
    {
        // ゲームシーンは解放しない
        if ((_nowScene != SCENES.GAME)
            && (_nowScene != SCENES.NONE))
        {
            // 解放を開始して、完了するのを待つ
            yield return SceneManager.UnloadSceneAsync(SCENE_NAMES[(int)_nowScene]);
        }
    }

}

