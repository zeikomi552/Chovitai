# Chovitai

![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/zeikomi552/CvSeachTool)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/zeikomi552/CvSeachTool)
![GitHub](https://img.shields.io/github/license/zeikomi552/CvSeachTool)


[CIVITAI](https://civitai.com/)の検索を効率化することを目的としたツールです。
CIVITAIを調べていると同じところをぐるぐる回ってしまうので作りました。

例えば以下のことができます。

- Checkpointを人気順に表示する
- LoRAを人気順に表示する
- 投稿画像を反応順に表示する
- AUTOMATIC1111/stable-diffusion-webuiで過去に生成した画像のPromptを確認する

など

Civitai上にはNSFWな画像も多く存在するため年齢制限X指定のツールです。
ご使用の際は十分にご注意ください。

画面イメージ

![](img-README/README-00.png)

<!--more-->

## インストール手順

### ダウンロード

以下の場所からzipファイルをダウンロードします。
(最新のバージョンのものを取得してください)

https://github.com/zeikomi552/CvSeachTool/releases

![](README-IMG/cvseachtool-01-01.png)

### インストール

ダウンロードしたzipファイルを展開します。

![](README-IMG/cvseachtool-01-02.png)

展開後の様子。

![](README-IMG/cvseachtool-01-03.png)

フォルダの中に入っていきます。

![](README-IMG/cvseachtool-01-04.png)

setup.exeを実行します。
(.NET6が必要なためインストーラーでエラーが出た場合は.NET6のインストールが必要です。)


![](README-IMG/cvseachtool-01-05.png)

以下の表示が出た人は詳細情報を開きます。

![](README-IMG/cvseachtool-01-06.png)

実行

![](README-IMG/cvseachtool-01-07.png)

次へ

![](README-IMG/cvseachtool-01-08.png)

次へ

![](README-IMG/cvseachtool-01-09.png)

次へ

![](README-IMG/cvseachtool-01-10.png)

はい

![](README-IMG/cvseachtool-01-11.png)

閉じる

![](README-IMG/cvseachtool-01-12.png)


## 基本的な使い方

### 起動
少し気持ち悪いセンスのアイコンがデスクトップに登場します。
（やっつけでつくったので、その内変えます。たぶん。）
ダブルクリック！！

![](README-IMG/cvseachtool-01-20.png)


### モデル検索

何の説明もない愛想のない画面が登場します。
まずは検索ボタンを押してみます。

![](README-IMG/cvseachtool-01-21.png)

そこそこ(数秒)検索に時間がかかります。

![](README-IMG/cvseachtool-01-22.png)

CIVITAIを触る人にはなじみのある画像が出てきます。

![](README-IMG/cvseachtool-01-23.png)

選択を切り替えていくことで画像イメージが切り替わります。

![](README-IMG/cvseachtool-01-24.png)

検索条件はこの辺りをいじってみてください。

![](README-IMG/cvseachtool-01-25.png)

ランキング順に検索するには以下のようにすればできます。

![](README-IMG/cvseachtool-01-26-2.png)

### ブックマーク

☆マークを押すとブックマーク画面に移動します。

![](README-IMG/cvseachtool-01-60.png)

先ほどのモデル検索の画面のこいつです。

![](README-IMG/cvseachtool-01-61.png)

チェックマークにすると以下のように保存されていきます。

![](README-IMG/cvseachtool-01-62.png)

アイコンと操作感がいまいちなのでその内変更すると思います。

### イメージ検索

CIVITAI Imageのタブをクリックします。
画面全体が切り替わります。

![](README-IMG/cvseachtool-01-30.png)

検索ボタンをクリックします。

![](README-IMG/cvseachtool-01-31.png)

画像データを取得してきます。

![](README-IMG/cvseachtool-01-32.png)

検索条件はこの辺りを適当に弄ってみてください。

![](README-IMG/cvseachtool-01-33.png)

## 過去につくった画像のプロンプト確認

Folderのタブをクリックします。

![](README-IMG/cvseachtool-01-40.png)

フォルダのアイコンをクリックします。

![](README-IMG/cvseachtool-01-41.png)

AUTOMATIC1111/stable-diffusion-webuiで生成した画像が保管されているフォルダを選択します。

![](README-IMG/cvseachtool-01-42.png)

画像が読み込まれます。

![](README-IMG/cvseachtool-01-43.png)

画像上に埋め込まれている生成した時点の情報が表示されます。

![](README-IMG/cvseachtool-01-44.png)

変なアイコンをクリックするとクリップボードにコピーされます。

![](README-IMG/cvseachtool-01-45.png)

メモ帳等に貼り付けが可能です。

![](README-IMG/cvseachtool-01-46.png)

## フィルター

ここと

![](README-IMG/cvseachtool-01-50.png)

ここに

![](README-IMG/cvseachtool-01-51-2.png)

フィルターがあります。

![](README-IMG/cvseachtool-01-52.png)

Noneが一番健全でXがこの世の性癖すべてを見させられます。
一番上の空白はフィルタ無しなのでXと同じ扱いです。
また、NoneでフィルタしていてもCIVITAI側が自動判定しているからか結構X指定が出てきます。
ご使用の際はお気をつけください。

## 余談

アプリ名を変更しました。
CvSeachTool → Chovitai
ずっとスペルミスをしてた。
