using FrameServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Network
{
    public class TcpService : TcpListener
    {
        private NetworkService mService;

        private int mPort;

        Thread mAcceptThread, mReceiveThread, mSendThread;

        Queue<MessageInfo> mSendMessageQueue = new Queue<MessageInfo>();


        public bool IsActive { get { return Active; } }

        
        public event OnAcceptHandler onAccept;

        public TcpService(NetworkService service, int port) : base(IPAddress.Any, port)
        {
            mService = service;
            mPort = port;
        }

        public bool Listen()
        {
            if (IsActive)
            {
                return true;
            }

            Start();

            mAcceptThread = new Thread(AcceptThread);
            //mReceiveThread = new Thread(ReceiveThread);
            mSendThread = new Thread(SendThread);

            mAcceptThread.Start();
            //mReceiveThread.Start();
            mSendThread.Start();


            return true;
        }

        public void Close()
        {
            Stop();

            if (mAcceptThread != null)
            {
                mAcceptThread.Abort();
                mAcceptThread = null;
            }

            if (mReceiveThread != null)
            {
                mReceiveThread.Abort();
                mReceiveThread = null;
            }
            if (mSendThread != null)
            {
                mSendThread.Abort();
                mSendThread = null;
            }
        }

        public void Send(MessageInfo message)
        {
            if (message == null)
            {
                return;
            }
            lock (mSendMessageQueue)
            {
                mSendMessageQueue.Enqueue(message);
            }
        }

        void AcceptThread()
        {
            while (IsActive)
            {
                try
                {
                    Socket s = AcceptSocket();
                    if (s != null)
                    {
                        if (onAccept != null)
                        {
                            onAccept(s);
                        }
                    }

                    Thread.Sleep(1);
                }
                catch (Exception e)
                {
                    mService.CatchException(e);
                    throw e;
                }

            }
        }
        
        void SendThread()
        {
            while (IsActive)
            {
                lock (mSendMessageQueue)
                {
                    while (mSendMessageQueue.Count > 0)
                    {
                        MessageInfo message = mSendMessageQueue.Dequeue();

                        if (message == null) continue;
                        try
                        {
                            if (message.session!=null&& message.session.socket!=null)
                            {
                                message.session.socket.Send(message.buffer.buffer);
                            }
                            
                        }
                        catch (SocketException e)
                        {
                            mService.Debug(e.Message);
                            message.session.Disconnect();
                        }
                        catch (Exception e)
                        {
                            mService.CatchException(e);
                            throw e;
                        }
                    }
                }
                Thread.Sleep(1);

            }
        }

      
    }
}
