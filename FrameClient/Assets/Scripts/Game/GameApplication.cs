using UnityEngine;
using System.Collections.Generic;
using Util;
using Network;
/// <summary>
/// Current game scene
/// </summary>


public class GameApplication :SingletonMono<GameApplication> {

    //public Mode mode = Mode.LockStep;
    [HideInInspector]
    public Mode mode = Mode.Optimistic;
    public string ip = "127.0.0.1";
    public int tcpPort = 1255;
    public int udpPort = 1337;
    //public Protocol protocol = Protocol.UDP;
    [HideInInspector]
    public Protocol protocol = Protocol.KCP;
    void Awake()
	{
		DontDestroyOnLoad (gameObject);
	

		WindowManager.GetSingleton();

		SceneMachine.GetSingleton().Init();

        SceneMachine.GetSingleton().ChangeScene(GameSceneType.FrameScene);

    }
    // Use this for initialization
    void Start () {

      
    }


    // Update is called once per frame
    void Update () {

        TimerHeap.Tick();

        ClientService.GetSingleton().Update();

        SceneMachine.GetSingleton().OnUpdate ();

        PlayerManager.GetSingleton().OnUpdate();

      

    }
	void OnApplicationQuit()
	{
        PlayerManager.GetSingleton().Clear();
        ClientService.GetSingleton().Disconnect();
        SceneMachine.GetSingleton().Destroy();
		Application.Quit ();
	}


	


}
