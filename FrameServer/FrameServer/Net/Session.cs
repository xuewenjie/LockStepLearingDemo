using FrameServer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Network
{
    public class Session
    {
        private int mID;
        public int id { get { return mID; } }
        public IPEndPoint tcpAdress, udpAdress;

        private NetworkService mService;
        private Socket mSocket;
        private Thread mActiveThread;
        private Thread mReceiveThread;

        public Socket socket { get { return mSocket; } }
        public NetworkService service { get { return mService; } }

        public event OnReceiveHandler onReceive;

        public DateTime lastOnLine;

        public Session(int id, Socket sock, OnReceiveHandler onReceive, NetworkService serv)
        {
            mID = id;
            mService = serv;
            mSocket = sock;

            tcpAdress = (IPEndPoint)sock.RemoteEndPoint;

            this.onReceive = onReceive;
            RefreshHeartTime();

            mActiveThread = new Thread(ActiveThread);
            mActiveThread.Start();

            mReceiveThread = new Thread(ReceiveThread);
            mReceiveThread.Start();

            
        }

        private void ReceiveThread()
        {
            while (IsConnected)
            {
                try
                {
                    int receiveSize = socket.Receive(MessageBuffer.head, MessageBuffer.MESSAGE_HEAD_SIZE, SocketFlags.None);
                    //收到的字节数不是预定的消息头的长度的话，或者消息头的结构定义不正确的话，那么这条消息是不正确的
                    if (receiveSize == 0|| receiveSize != MessageBuffer.MESSAGE_HEAD_SIZE|| MessageBuffer.IsValid(MessageBuffer.head) == false)
                    {
                        continue;
                    }

                    //获取要获取的消息长度 ，bodySize返回
                    int bodySize = 0;
                    if (MessageBuffer.Decode(MessageBuffer.head, MessageBuffer.MESSAGE_BODY_SIZE_OFFSET, ref bodySize) == false)
                    {
                        continue;
                    }
                    MessageBuffer message = new MessageBuffer(MessageBuffer.MESSAGE_HEAD_SIZE + bodySize);

                    //将接收的包头拷贝到message里
                    Array.Copy(MessageBuffer.head, 0, message.buffer, 0, MessageBuffer.head.Length);

                    //接收包头
                    if (bodySize > 0)
                    {
                        int receiveBodySize = socket.Receive(message.buffer, MessageBuffer.MESSAGE_BODY_OFFSET, bodySize, SocketFlags.None);
                        if (receiveBodySize != bodySize)
                        {
                            continue;
                        }
                    }
                    
                    if (onReceive != null)
                    {
                        onReceive(new MessageInfo(message, this));
                    }
                }
                catch (SocketException e)
                {
                    mService.Debug(e.Message);
                    Disconnect();
                }
                catch (Exception e)
                {
                    mService.CatchException(e);
                    throw e;
                }

                Thread.Sleep(1);
            }
        }

        void ActiveThread()
        {
            
            while (IsConnected)
            {
                if ((DateTime.Now - lastOnLine).TotalMinutes > 0.1)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            Disconnect();
        }

        public bool IsConnected
        {
            get
            {
                try
                {
                    

                    if (mSocket != null && mSocket.Connected)
                    {
                        return true;
                    }
                    return false;

                }
                catch (SocketException e)
                {
                    mService.CatchException(e);
                    return false;
                }
                catch (Exception e)
                {
                    mService.CatchException(e);
                    return false;
                }
            }
        }

        public void SendTcp(MessageBuffer message) 
        {
            if(mService!=null && mService.tcp !=null)
            {
                mService.tcp.Send(new MessageInfo(message, this));
            }
        }

        public void RefreshHeartTime()
        {
            lastOnLine= DateTime.Now;
        }


        public void Disconnect()
        {
            if (mSocket == null) return;

            mSocket.Close();
            mSocket = null;
            mService.RemoveSession(this);

            mActiveThread.Abort();
            mActiveThread = null;

            mReceiveThread.Abort();
            mReceiveThread = null;

            //this.onReceive -= onReceive;
        }
    }
}
