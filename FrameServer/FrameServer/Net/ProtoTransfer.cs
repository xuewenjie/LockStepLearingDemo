using System;
using System.IO;
using PBMessage;
using ProtoBuf.Meta;
using FrameServer;

namespace  Network
{
    public static class ProtoTransfer
    {
        public static Point3D Get(GMPoint3D gmpoint)
        {
            Point3D point = new Point3D(0, 0, 0);
            if (gmpoint!=null)
            {
                point = new Point3D(gmpoint.x, gmpoint.y, gmpoint.z);
            }
            return point;
        }

        public static GMPoint3D Get(Point3D point)
        {
            GMPoint3D gmpoint = new GMPoint3D();
            gmpoint.x = point.x;
            gmpoint.y = point.y;
            gmpoint.z = point.z;
            return gmpoint;
        }

        public static GMPlayerInfo Get(PlayerInfo info)
        {
            GMPlayerInfo o = new GMPlayerInfo();

            o.roleId = info.roleid;
            o.name = info.name;
            o.moveSpeed = info.moveSpeed;
            o.moveSpeedAddition = info.moveSpeedAddition;
            o.moveSpeedPercent = info.moveSpeedPercent;
            o.attackSpeed = info.attackSpeed;
            o.attackSpeedAddition = info.attackSpeedAddition;
            o.attackSpeedPercent = info.attackSpeedPercent;
            o.maxBlood = info.maxBlood;
            o.nowBlood = info.nowBlood;
            o.type = info.type;
            return o;
        }
        
        public static GMCommand Get(Command cmd)
        {
            GMCommand o = new GMCommand();
            o.id = cmd.id;
            o.frame = cmd.frame;
            o.type = cmd.type;
            o.frametime = cmd.time;
            o.data = cmd.data;
            return o;
        }

      
    }
}
