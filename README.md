Instructions to Install Unity
-----------------------------
This project is developed in Unity 2019.4.19f1. Its recommended to use the same version. To install Unity, the steps to be followed are
1) Download Unity Hub from https://unity3d.com/get-unity/download
2) After Unity Hub is installed, Download Unity Editor 2019.4.19f1 from [unityhub://2019.4.19f1/ca5b14067cec](unityhub://2019.4.19f1/ca5b14067cec)

While Unity editor installation, we will need to choose extra packages. These includes (WebGL Package to compile & Optionally Visual Studio 2019 to edit code)

After all above are installed successfully, we are now good to open this project through Unity HUB.


Instructions to Compile a Build
-------------------------------
1) Select File > Build Settings
2) Select WebGL in the Build window opened.
3) If switch platform button is highlighted, then need to click on that so that project will switch the platform to WebGL
4) Click on the Build button and give the output directory
5) Once the Build is finished, the output directory will open

Instructions to Host/Serve a Build
---------------------------------------------
After project built successfuly, The output directory given in previous step will contain the files which can be hosted on the server. All these files now can be transferred to the public_html folder in the hosting server through an FTP or similar ways.
