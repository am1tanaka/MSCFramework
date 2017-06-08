# MSCFramework
マルチシーンによるUnity用ゲームフレームワークです。以下のような機能があります。

- Baseシーンを常駐させて、状態に合わせてシーンを読み込む
- 現在の状態ごとの初期化(Init)、更新(Update)、破棄(Destroy)をシーンの切り替えに応じて呼び出す
- サンプルでは`TitleScene`を実装
  - BaseSceneを継承したクラスを作成
  - SceneController内で、シーンのインスタンスをScriptableObject.Createで生成
- シーンの切り替えは、`SceneController.SetNextScene(<次のシーン>)`に引数で切り替えたいシーンを渡して行う
- シーンの切り替えにはフェードイン、フェードアウトが入る
- フェードイン・アウトの時間は、SceneControllerで指定する
- フェードイン・アウトの色は、CanvasScene.Fadeイメージの色で設定できるし、スクリプトでColorを切り替えてプログラムで変更も可能
- BGMは、`SoundController.PlayBGM(<再生したいBGM>)`で再生
- SEは、`SoundController.Play(<再生したいSE>)`で再生
- SEとBGMは、`Resources/Audio`フォルダーに入れて、`SoundController`の初期化時に読み込むようにする


# 組み込んであるリソース
BGMとSEは、以下のものを使わせていただきました。

- [魔王魂 さん](http://maoudamashii.jokersounds.com/)
  - クリック音
- [甘茶の音楽工房 さん](http://amachamusic.chagasi.com/)
  - ファミポップⅣ
