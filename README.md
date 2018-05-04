# UnityTestableFrameWork
Tastable frame work for Unity.

Unity上で TDD(テスト駆動開発)に適した MVP設計のコードを作成するためのフレームワークです。

まだ製作途中の段階のためコードは抜本的な改修が行われます。（互換性は維持されません。）  
あくまで、設計を改善するための意見募集のための公開です。  
また　コードルールはRiderのUnity用標準のものを使います。namespaceも同様です。  

このフレームワークに則って作成すれば、コンパイル時間がかかるような大規模プロジェクトになった場合も、
作業対象パッケージにのみAssembly-Deffinitionを設定することで、少ないコンパイル時間でTDDを実施できるはずである。

# ====利用の為に====
このフレームワークは IDEとしてRiderを利用することを前提としています。 （基本的にUnitTestはRider上で行います。） また、サンプルはNSubstituteプラグインを利用しています。 Plugins/Editor内にNSubstitute.dllを配置してください。 NSubstitute.dllはUnity Test Toolsに同封されているようです。
# ====フレームワークの概要====
①MVP設計のための各View・Presenter・Modelの基底クラス、  

②規約に沿って作成されたPrefabを元に、  
その基底クラスを継承したView・Presenter・Model並びにModelのTestコードを自動生成する機能  

③生成したViewをPrefabに配置し、必要な参照を自動的にインスペクタに設定する機能  

④依存性を注入しやすいSystem層・Infra層のコード・テストコード雛形を自動生成する機能

⑤Assembly-Definitionと共に、MVP層・System層・Infra層のフォルダを自動生成する機能

# ====フォルダ構造====  
- TastableFrameFrameWork・・・フォルダやスクリプトの自動生成コードが入っています。  
- Plugins  
- My  
-- MVP  
--- _Assembly-My-MVP.asmdef    
--- Foo  
---- FooModel.cs  
---- FooModelTest.cs  
---- FooView.cs  
---- FooPresenter.cs  
---- Resources.cs  
----- Foo.prefab
-- System 
--- _Assembly-My-System.asmdef     
--- MySystem.asmdef  
--- Hoge  
---- Interface  
----- HogeLocater.cs  
----- IHogeManager.cs  
---- Impl  
----- HogeManager.cs  
----- HogeManagerTest.cs  
-- Infra  
--- _Assembly-My-Infra.asmdef  
--- Boo  
---- Interface  
----- BooLocater.cs  
----- IBooManager.cs  
---- Impl  
----- BooManager.cs  
----- BooManagerTest.cs  


# ====自動化コードの使い方====  
プロジェクトフォルダで右クリックするとMVPというメニューが追加されているので、  
そこから実施対象を選んで実行します。  
Generate Folder・・・Assembly-Definitionと共に、MVP層・System層・Infra層のフォルダを自動生成します。  
Generate MVP Scripts for Prefab・・・規約に沿って作成・配置されたPrefabを右クリックして実行すると、MVPのコードが自動生成されます。  
Generate System Folder and Scripts・・・任意のフォルダを右クリックして実行すると、System層の雛形フォルダ・コードが自動生成されます。  
Generate Infra Folder and Scripts・・・任意のフォルダを右クリックして実行すると、Infra層の雛形フォルダ・コードが自動生成されます。  
Generate Infra Folder and Scripts・・・任意のフォルダを右クリックして実行すると、Infra層の雛形フォルダ・コードが自動生成されます。  
Add View Script for Prefab・・・規約に沿って作成・配置されたPrefabを右クリックして実行すると、対応するViewコードが自動設定されます。  

EscapeForTest・・・プロジェクトに含まれるTest.csで終わるコードに#if Unity_Editor endif を追加します。  

# ====Viewの自動生成を行うためのPrefabの規約====
現在、uGUIのText,Image,Button,ToggleGroupをサポートしています。
Prefab名はモジュール名と同一とする。   スクリプトから変更する必要があるTextは、"識別名"Text スクリプトから変更する必要があるImageは、"識別名"Image スクリプトから変更する必要があるButtonは、"識別名"Button スクリプトから変更する必要があるToggleGroupは、"識別名"ToggleGroup と命名してください。
なお、Buttonへのイベントの設定は、 インスペクタからではなく、スクリプトのAwake内で行う。


# ====Viewの動的生成方法====  
  var fooPresenter = new FooPresenter();
  var fooModel = fooPresenter.CreateModel();
  var fooView = CreateView<FooView>();
  fooPresenter.ShowView(fooView);

こんな感じで、Viewを設定します。

# ====System層・Infra層の使い方===  


# ====前提となるレイヤ構成====  
このフレームワークには、

## 【Public層】  
System層への依存性の注入と、画面の呼び出しを行う。
全ての層へ依存する。
また、MVP層の生成もこの層で行う。

## 【MVP層】  
各画面毎にModel、Presenter、Viewに分けて管理するスクリプト
System/Infra層のinterfaceに依存する。

## 【System層】  
各画面のModelから呼び出される、複数の画面から共通で使われるシステム。
各パッケージ毎にImplとInterfaceに分けられる。
各パッケージ間は依存しないのが望ましい。もしする場合でも相互依存をしてはいけないし、
Implパッケージに依存してはいけない。
Infra層のInterfaceに依存する。

## 【Infra層】  
各画面やシステムから呼び出される汎用パッケージ
各パッケージ毎にImplとInterfaceに分けられる。
各パッケージは、独立して利用可能であり、相互に依存しない。
また、別のプロジェクトに対してもそのまま再利用できるのが望ましい。


依存関係について重要な点は
MVP層とSystem層のImpl、Infra層のImplは、Public層からしか依存されていない。
という点である。
これにより、各パッケージに対して、
作業時にAssembly-Definitionを設定する事が可能になる。

System層のInterface、Infra層のInterfaceは、他の層から依存されているため、
Assembly-Definitionを設定することは難しいが、相互参照をしていなければ、可能ではあり、
そもそもInterfaceの修正は頻繁には起こらないと考えられるため、
設定する必要性も薄い。



# ====各層の依存関係について====  
【Public層】  
⇒全ての層に依存する。  

【Module層】  
⇒【System層】のInterface  
⇒【Infra層】のInterface  

【System層】  
⇒その【System層】のInterface  
⇒他の【Infra】のInterface  
場合によっては、他の【System層】のInterfaceにも依存するが、相互参照しないように気をつける  

【Infra】  
⇒その【Infra】のInterface  
※他の何にも依存しないように気をつける。
ただし、外部プラグインによっては、Assemblyを分ける事ができないものが存在するため、
そのような場合はInfra層の実装部分のみ、Public層に配置する。
