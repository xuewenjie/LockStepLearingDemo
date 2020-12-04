using System;
using PBMessage;
using Network;
namespace FrameServer
{
    public class Command
    {
        private long mID;
        private long mFrame;
        private int mType;
        private string mData;
        private long mTime = 0;

        public string eventType;

        public long id { get { return mID; } }
        public long frame { get { return mFrame;} }
        public int type { get { return mType; } }
        public string data { get { return mData; } }
        public long time { get { return mTime; } }

        public Command() { }
        public Command(long frame, int type, string data,long time)
        {
            mID = GUID.Int64();
            mFrame = frame;
            mType = type;
            mData = data;
            mTime = time;
        }

        

        public void SetFrame(long frame, long time)
        {
            mFrame = frame;
            mTime = time;
        }

        //public T Get<T>() where T : class, ProtoBuf.IExtensible
        //{
        //    T t = JsonSerializerUtil.FromJsonByte<T>(mData);
        //    //T t = ProtoTransfer.DeserializeProtoBuf<T>(mData);
        //    return t;
        //}

        //public object Get(Type type)
        //{
        //    return ProtoTransfer.DeserializeProtoBuf(mData, type);
        //}
    }
}
