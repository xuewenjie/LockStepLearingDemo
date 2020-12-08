using System;
using System.Collections.Generic;
using System.Threading;
using PBMessage;
using Network;
using System.Timers;

namespace FrameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.ENABLE_ERROR = true;

            Program p = new Program();
            while (true)
            {
                p.ReceiveMessage();
            }
        }

        public const int TCP_PORT = 1255;

        public const int FRAME_INTERVAL = 66; //帧时间 每隔多少毫秒发送驱动帧

        NetworkService mService;

        List<User> mUserList = new List<User>();
        Dictionary<long, Dictionary<int, List<Command>>> mFrameDic = new Dictionary<long, Dictionary<int, List<Command>>>();//关键帧

        private bool mBegin = false;    //游戏是否开始

        private long mCurrentFrame = 1; //当前帧数
        private long mFrameTime = 0;


        public Program()
        {
            

            MessageBuffer.MESSAGE_MAX_VALUE = (int)MessageID.MaxValue;
            MessageBuffer.MESSAGE_MIN_VALUE = (int)MessageID.MinValue;
            
            mService = new NetworkService(TCP_PORT);
            
            mService.onStart += OnStart;
            mService.onAccept += OnAccept;
            mService.onMessage += OnMessage;
            mService.onDisconnect += OnDisconnect;
            mService.onDebug += OnDebug;

            mService.Start();

            StartTimer();
        }

        void StartTimer()
        {
            //实例化Timer类，设置间隔时间为100毫秒
            System.Timers.Timer t = new System.Timers.Timer(FRAME_INTERVAL);
            //到达时间的时候执行事件；
            t.Elapsed += new System.Timers.ElapsedEventHandler(LogicFrame);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        /// <summary>
        /// 发送驱动帧和关键帧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogicFrame(object sender, ElapsedEventArgs e)
        {
            if (mBegin)
            {
                mFrameTime += 1;
                SendFrame();
            }
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msaageId"></param>
        /// <param name="data"></param>
        /// <param name="ready">是否只发给已经准备好的人</param>
        void BroadCast<T>(MessageID msaageId, T data, bool ready = false) 
        {
            for (int i = 0; i < mUserList.Count; ++i)
            {
                if (ready == false || (ready == true && mUserList[i].ready))
                {
                    mUserList[i].SendTcp(msaageId, data);
                }
            }
        }

        User GetUser(int roleid)
        {
            for(int i = 0; i < mUserList.Count; ++i)
            {
                if(mUserList[i].roleid == roleid)
                {
                    return mUserList[i];
                }
            }
            return null;
        }
        

        private void OnStart()
        {
            Debug.Log(string.Format("Server start success, ip={0} tcp port={1} ",NetworkService.GetLocalIP(),TCP_PORT),ConsoleColor.Green);
        }

        private void OnAccept(Session c)
        {
            GM_Accept sendData = new GM_Accept();
            sendData.conv = c.id;
            
            byte[] data = JsonSerializerUtil.ToJsonByte(sendData);
            MessageBuffer message = new MessageBuffer((int)MessageID.GM_ACCEPT_SC, data, c.id);

            c.SendTcp(message);
        }

        private void OnConnect(Session c,int roleId)
        {
            User user = new User(roleId, c);
            mUserList.Add(user);

            Debug.Log(string.Format("{0} roleid={1} tcp={2}, udp={3} connected! count={4}", c.id, user.roleid, c.tcpAdress, c.udpAdress, mUserList.Count),ConsoleColor.Yellow);

            GM_Connect sendData = new GM_Connect();
            sendData.roleId = user.roleid;
            
            sendData.frameinterval = FRAME_INTERVAL; //告诉客户端帧时长 毫秒
            //sendData.player = ProtoTransfer.Get(user.mPlayerInfo);

            user.SendTcp(MessageID.GM_CONNECT_SC, sendData);

            //告诉别人有人连接了
            for (int i = 0; i < mUserList.Count; ++i)
            {
                var u = mUserList[i]; //别人
                if (c != u.client)
                {
                    u.SendTcp(MessageID.GM_CONNECT_BC, sendData);
                }
            }

            //告诉他有哪些人已经连接了
            for (int i = 0; i < mUserList.Count; ++i)
            {
                var u = mUserList[i];//别人
                if (c != u.client)
                {
                    sendData.roleId = u.roleid;
                    //sendData.player = ProtoTransfer.Get(u.mPlayerInfo);
                    //发给自己
                    user.SendTcp(MessageID.GM_CONNECT_BC, sendData);
                }
            }

        }
        
        
        private void OnDisconnect(Session c)
        {
            if (c == null) return;

            int id = c.id;
           

            int roleId = 0;

            for (int i = 0; i < mUserList.Count; ++i)
            {
                if (mUserList[i].client == c)
                {
                    roleId = mUserList[i].roleid;
                    mUserList.RemoveAt(i);
                    break;
                }
            }
            Debug.Log(string.Format("{0} roleid={1}  disconnected!", id, roleId), ConsoleColor.Red);
            
            //如果列表内的准备人数为零了 就表示游戏没人玩，那么结束该局游戏


            GM_Disconnect sendData = new GM_Disconnect();
            sendData.roleId = roleId;

            BroadCast(MessageID.GM_DISCONNECT_BC, sendData);
        }

        #region 消息接收处理

        public void ReceiveMessage()
        {
            mService.Update();
            Thread.Sleep(1);
        }


        private void OnMessage(Session client, MessageBuffer msg)
        {
            MessageID messageId = (MessageID)msg.id();
            switch (messageId)
            {
                case MessageID.GM_HEART_CS:
                    client.RefreshHeartTime();
                    Debug.Log("收到心跳包！");
                    break;
                case MessageID.GM_ACCEPT_CS:
                    {
                        GM_Accept recvData = JsonSerializerUtil.FromJsonByte<GM_Accept>(msg.body());
                        //ProtoTransfer.DeserializeProtoBuf<GM_Accept>(msg);
                        if (recvData.conv == client.id)
                        {
                            OnConnect(client, recvData.roleId);
                        }
                    }
                    break;
                case MessageID.GM_READY_CS:
                    {
                        GM_Ready recvData = JsonSerializerUtil.FromJsonByte<GM_Ready>(msg.body());
                        //ProtoTransfer.DeserializeProtoBuf<GM_Ready>(msg);
                        OnReceiveReady(client, recvData);

                    }
                    break;

                case MessageID.GM_FRAME_CS:
                    {
                        GM_Frame recvData = JsonSerializerUtil.FromJsonByte<GM_Frame>(msg.body());
                        OnOptimisticFrame(client, recvData);
                    }
                    break;
                case MessageID.GM_PING_CS:
                    {
                        GM_Request recvData = JsonSerializerUtil.FromJsonByte<GM_Request>(msg.body());
                        //ProtoTransfer.DeserializeProtoBuf<GM_Request>(msg);
                        User u = GetUser(recvData.id);
                        if (u != null)
                        {
                            GM_Return sendData = new GM_Return();
                            sendData.id = recvData.id;
                            u.SendTcp(MessageID.GM_PING_SC, sendData);
                        }
                    }
                    break;

            }
        }
        #endregion

        #region 收到准备后的处理
        /// <summary>
        /// 收到准备后的处理
        /// </summary>
        /// <param name="client"></param>
        /// <param name="recvData"></param>
        private void OnReceiveReady(Session client, GM_Ready recvData)
        {
            if (mBegin)
            {
                //游戏已经开始，不能中途进入游戏，后面再这里加断线重连逻辑看看
                return;
            }
            if (recvData == null || client == null) return;
            int readyCount = 0;
            for (int i = 0; i < mUserList.Count; ++i)
            {
                var user = mUserList[i];
                if (recvData.roleId == user.roleid && client == user.client)
                {
                    
                    user.SetReady();
                }

                BroadCast(MessageID.GM_READY_BC, recvData, true);
                //广播玩家准备（包括自己）
                //BroadCast(MessageID.GM_BEGIN_BC, recvData, true);
                //user.SendTcp(MessageID.GM_READY_BC, recvData);

                if (user.ready)
                {
                    readyCount++;
                }
            }

            Debug.Log(string.Format("{0} roleid={1} ready, ready count={2} user count={3}", client.id, recvData.roleId, readyCount, mUserList.Count), ConsoleColor.Blue);

            if (mBegin == false)
            {
                //所有的玩家都准备好了，可以开始同步
                if (readyCount == 2)
                //if (readyCount >= mUserList.Count)
                {
                    GM_Begin sendData = new GM_Begin();
                    sendData.roleIdList = new List<int>();
                    sendData.randSeed = 12345;
                    for (int i = 0; i < mUserList.Count; ++i)
                    {
                        if (mUserList[i].ready)
                        {
                            sendData.roleIdList.Add(mUserList[i].roleid);
                        }
                        
                        var user = mUserList[i];
                        GM_Ready gm_Ready = new GM_Ready();
                        gm_Ready.roleId = user.roleid;
                        //if (recvData.roleId == user.roleid && client == user.client)
                        //{
                        //    user.position = ProtoTransfer.Get(recvData.position);
                        //    user.direction = ProtoTransfer.Get(recvData.direction);
                        //    user.SetReady();
                        //}
                        //广播玩家准备（包括自己）
                        
                        //user.SendTcp(MessageID.GM_READY_BC, gm_Ready);

                        //if (user.ready)
                        //{
                        //    readyCount++;
                        //}
                    }

                    

                    mFrameDic = new Dictionary<long, Dictionary<int, List<Command>>>();

                    //GM_Begin sendData = new GM_Begin();
                    //sendData.result = 0;

                    BroadCast(MessageID.GM_BEGIN_BC, sendData, true);

                    //BroadCast(MessageID.GM_BEGIN_BC, sendData, true);

                    BeginGame();

                }
            }
            
            else //断线重连
            {           
                //User user = GetUser(recvData.roleId);
                //if(user!=null)
                //{
                //    GM_Begin sendData = new GM_Begin();
                    

                //    user.SendTcp(MessageID.GM_BEGIN_BC, sendData);

                //    /*
                //    GM_Frame_BC frameData = new GM_Frame_BC();
                    
                //    //给他发送当前帧之前的数据
                //    for (long frame = 1; frame < mCurrentFrame - 1; ++frame)
                //    {
                //        if (mFrameDic.ContainsKey(frame))
                //        {
                //            frameData.frame = frame;
                //            frameData.frametime = 0;
                //            var it = mFrameDic[frame].GetEnumerator();
                //            while (it.MoveNext())
                //            {
                //                for (int i = 0, count = it.Current.Value.Count; i < count; ++i)
                //                {
                //                    GMCommand cmd = ProtoTransfer.Get(it.Current.Value[i]);

                //                    frameData.command.Add(cmd);
                //                }
                //            }
                //            user.SendUdp(MessageID.GM_FRAME_BC, frameData);
                //        }
                //    }
                //    */
                //}
            }
            
        }
        

        private void BeginGame()
        {
            mCurrentFrame = 1;

            mBegin = true; //游戏开始

            mFrameTime = 0;
            
        }

        private void StopGame()
        {
            mCurrentFrame = 1;

            mBegin = false; //游戏结束

            mFrameTime = 0;
        }
        #endregion

        #region 乐观模式

        /// <summary>
        /// 按固定频率向客户端广播帧
        /// </summary>
        private void SendFrame()
        {
            long frame = mCurrentFrame ++;
            int userCount = 0; //当前帧有多少个客户端发了命令
            GM_Frame_BC sendData = new GM_Frame_BC();

            sendData.frame = frame;
            sendData.frametime = mFrameTime;

            if (mFrameDic.ContainsKey(frame))
            {
                var frames = mFrameDic[frame];

                userCount = frames.Count;

                var it = frames.GetEnumerator();
                while (it.MoveNext())
                {
                    for (int i = 0, count = it.Current.Value.Count; i < count; ++i)
                    {
                        GMCommand cmd = ProtoTransfer.Get(it.Current.Value[i]);
                        cmd.eventType = it.Current.Value[i].eventType;
                        sendData.command.Add(cmd);
                    }
                }
            }

           
            //不显示那么多log
            if (frame % 30 == 0 || sendData.command.Count > 0)
            {
                Debug.Log(string.Format("Send frame:{0} user count:{1} command count:{2}", frame, userCount, sendData.command.Count), ConsoleColor.Gray);
            }

            BroadCast(MessageID.GM_FRAME_BC, sendData,true);         
        }

        private void OnOptimisticFrame(Session client, GM_Frame recvData)
        {

            int roleId = recvData.roleId;

            long frame = recvData.frame;

            Debug.Log(string.Format("Receive roleid={0} serverframe:{1} clientframe:{2} command:{3}", roleId, mCurrentFrame, frame,recvData.command.Count),ConsoleColor.DarkYellow);
            
            if (mFrameDic.ContainsKey(mCurrentFrame) == false)
            {
                mFrameDic[mCurrentFrame] = new Dictionary<int, List<Command>>();
            }
            for (int i = 0; i < recvData.command.Count; ++i)
            {
                //乐观模式以服务器收到的时间为准
                Command frameData = new Command(recvData.command[i].frame, recvData.command[i].type, recvData.command[i].data, mFrameTime);
                frameData.eventType = recvData.command[i].eventType;
                if (mFrameDic[mCurrentFrame].ContainsKey(roleId) == false)
                {
                    mFrameDic[mCurrentFrame].Add(roleId, new List<Command>());
                }
                mFrameDic[mCurrentFrame][roleId].Add(frameData);
            }
        }

        #endregion

        private void OnDebug(string s)
        {
            Debug.Log(s, ConsoleColor.Red);
        }

    }
}
