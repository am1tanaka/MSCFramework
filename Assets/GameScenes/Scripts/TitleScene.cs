using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SoundController.PlayBGM(SoundController.BGM.FAMIPOP4);
    }

    public override void Update()
    {
        base.Update();

        // シーン切り替えテスト
        if (Input.GetMouseButtonDown(0))
        {
            SceneController.SetNextScene(SceneController.SCENES.TITLE);
            SoundController.Play(SoundController.SE.CLICK);
        }
    }
}
