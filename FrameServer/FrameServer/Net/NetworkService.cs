using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;


namespace Network
{
    public delegate void OnStartHandler();
    public delegate void OnConnectHandler(Session c);
    public delegate void OnMessageHandler(Session c, MessageBuffer m);
    public delegate void OnDisconnectHandler(Session c);
    public delegate void OnExceptionHandler(Exception e);
    public delegate void OnPingHandler(Session c, int millis);
    public delegate void OnDebugHandler(string msg);
    public delegate void OnReceiveHandler(MessageInfo message);
    public delegate void OnAcceptHandler(Socket s);

    public class NetworkService
    {
        public static byte pingByte = byte.MaxValue;

        public event OnStartHandler onStart;
        public event OnConnectHandler onAccept;
        public event OnDisconnectHandler onDisconnect;
        public event OnMessageHandler onMessage;
        public event OnExceptionHandler onException;
        public event OnPingHandler onPing;
        public event OnDebugHandler onDebug;

        int numberOfClient = 1;

        private List<Session> mAcceptList = new List<Session>();
        private List<Session> mDisconnectList = new List<Session>();

        private List<Session> mSessionList = new List<Session>();

        private List<Session> mTmpSessionList = new List<Session>();
        public List<Session> sessions
        {
            get
            {
                lock(mSessionList)
                {
                    mTmpSessionList.Clear();
                    mTmpSessionList.AddRange(mSessionList);
                }
                return mTmpSessionList;
            }
        }      

        private List<string> mDebugMessageList = new List<string>();

        private Queue<MessageInfo> mReceiveMessageQueue = new Queue<MessageInfo>();

        private TcpService mTcp;

        public TcpService tcp { get { return mTcp; } }

        public NetworkService(int tcp, int udp, bool kcp)
        {
            mTcp = new TcpService(this, tcp);
        }

        public bool IsActive
        {
            get
            {
                return mTcp.IsActive; 
            }
        }


        public void Start()
        {
            try
            {
                mTcp.Listen();
                
                mTcp.onReceive += OnReceive;
                mTcp.onAccept += OnAccept;
                onStart();
            }
            catch (Exception e)
            {
                CatchException(e);
            }
        }

        public Session GetSession(int id)
        {
            Session s = null;
            lock (mSessionList)
            {
                foreach (Session c in mSessionList)
                {
                    if (c.id == id)
                    {
                        s = c; break;
                    }
                }
            }
            return s;
        }

        public Session GetSession(IPEndPoint ip)
        {
            Session s = null;
            lock (mSessionList)
            {
                foreach (Session c in mSessionList)
                {
                    if (c.udpAdress != null)
                    {
                        if (c.udpAdress.AddressFamily == ip.AddressFamily
                            && c.udpAdress.Address.Equals(ip.Address)
                            && c.udpAdress.Port == ip.Port)
                        {
                            s = c; break;
                        }
                    }
                }
            }
            return s;
        }

    

        public void Debug(string s)
        {
            lock (mDebugMessageList)
            {
                mDebugMessageList.Add(s);
            }
        }

        public void Update()
        {
            lock (mReceiveMessageQueue)
            {
                while (mReceiveMessageQueue.Count > 0)
                {
                    MessageInfo message = mReceiveMessageQueue.Dequeue();
                    onMessage(message.session, message.buffer);
                }
            }

            lock(mAcceptList)
            {
                while (mAcceptList.Count > 0)
                {
                    onAccept(mAcceptList[0]);
                    mAcceptList.RemoveAt(0);
                }
            }
         
            lock (mDisconnectList)
            {
                while (mDisconnectList.Count > 0)
                {
                    onDisconnect(mDisconnectList[0]);
                    mDisconnectList.RemoveAt(0);
                }
            }

            lock (mDebugMessageList)
            {
                while (mDebugMessageList.Count > 0)
                {
                    onDebug(mDebugMessageList[0]);
                    mDebugMessageList.RemoveAt(0);
                }
            }        
        }


        public void OnReceive(MessageInfo message)
        {
            if (message == null)
            {
                return;
            }
            lock (mReceiveMessageQueue)
            {
                mReceiveMessageQueue.Enqueue(message);
            }
        }


        void OnAccept(Socket s)
        {
            Session c = new Session(numberOfClient++, s, this);
            c.onReceive += OnReceive;

            lock (mSessionList)
            {
                mSessionList.Add(c);
            }

            lock(mAcceptList)
            {
                mAcceptList.Add(c);
            }
          
        }



        public void RemoveSession(Session c)
        {
            lock (mSessionList)
            {
                mSessionList.Remove(c);
            }

            lock (mDisconnectList)
            {
                mDisconnectList.Add(c);
            }
        }

        public void Close()
        {
            mTcp.Close();


            List<Session> list = new List<Session>(mSessionList);

            foreach (Session c in list) c.Disconnect();
            mSessionList.Clear();
            list.Clear();
        }

        public void CatchException(Exception e)
        {
            if (onException != null) onException(e);
        }

        public void PingResult(Session c, int millis)
        {
            if (onPing != null) onPing(c, millis);
        }



        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>本机IP地址</returns>
        public static string GetLocalIP()
        {
            //return "103.46.128.45";
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
