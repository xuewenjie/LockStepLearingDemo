﻿using Network;
using System;
using System.IO;
using UnityEngine;
using PBMessage;

namespace Network
{
    public static class ProtoTransfer
    {


        public static GMPoint3D Get(Vector3 vec)
        {
            GMPoint3D point = new GMPoint3D();

            point.x = (int)(vec.x * 10000);
            point.y = (int)(vec.y * 10000);
            point.z = (int)(vec.z * 10000);

            return point;
        }

        public static Vector3 Get(GMPoint3D point)
        {
            Vector3 vec = Vector3.zero;
            if (point!=null)
            {
                vec.x = point.x / 10000f;
                vec.y = point.y / 10000f;
                vec.z = point.z / 10000f;
            }
            
            
            return vec;
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

        public static PlayerInfo Get(GMPlayerInfo info)
        {
            PlayerInfo o = new PlayerInfo();

            o.roleid = info.roleId;
            o.name = info.name;
            o.moveSpeed = info.moveSpeed;
            o.moveSpeedAddition = info.moveSpeedAddition;
            o.moveSpeedPercent = info.moveSpeedPercent;
            o.attackSpeed = info.attackSpeed;
            o.attackSpeedAddition = info.attackSpeedAddition;
            o.attackSpeedPercent = info.attackSpeedPercent;
            o.maxBlood = info.maxBlood;
            o.nowBlood = info.nowBlood;
            o.type =info.type;

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
