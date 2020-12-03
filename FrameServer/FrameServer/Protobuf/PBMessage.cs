//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: command.txt
// Note: requires additional types generated from: scene.txt
namespace PBMessage
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_ReleaseSkill")]
  public partial class CMD_ReleaseSkill : global::ProtoBuf.IExtensible
  {
    public CMD_ReleaseSkill() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _skillId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"skillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int skillId
    {
      get { return _skillId; }
      set { _skillId = value; }
    }
    private PBMessage.GMPoint3D _mouseposition;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"mouseposition", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D mouseposition
    {
      get { return _mouseposition; }
      set { _mouseposition = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_MoveToPoint")]
  public partial class CMD_MoveToPoint : global::ProtoBuf.IExtensible
  {
    public CMD_MoveToPoint() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private PBMessage.GMPoint3D _position;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D position
    {
      get { return _position; }
      set { _position = value; }
    }
    private PBMessage.GMPoint3D _direction;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"direction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D direction
    {
      get { return _direction; }
      set { _direction = value; }
    }
    private PBMessage.GMPoint3D _destination;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"destination", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D destination
    {
      get { return _destination; }
      set { _destination = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_CreateMonster")]
  public partial class CMD_CreateMonster : global::ProtoBuf.IExtensible
  {
    public CMD_CreateMonster() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private PBMessage.GMPlayerInfo _player;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPlayerInfo player
    {
      get { return _player; }
      set { _player = value; }
    }
    private PBMessage.GMPoint3D _position;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D position
    {
      get { return _position; }
      set { _position = value; }
    }
    private PBMessage.GMPoint3D _direction;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"direction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D direction
    {
      get { return _direction; }
      set { _direction = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"CommandID")]
    public enum CommandID
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"RELEASE_SKILL", Value=1)]
      RELEASE_SKILL = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MOVE_TO_POINT", Value=2)]
      MOVE_TO_POINT = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CREATE_MONSTER", Value=3)]
      CREATE_MONSTER = 3
    }
  
}
// Generated from: message.txt
namespace PBMessage
{
    [global::ProtoBuf.ProtoContract(Name=@"MessageID")]
    public enum MessageID
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MinValue", Value=0)]
      MinValue = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_ACCEPT_SC", Value=98)]
      GM_ACCEPT_SC = 98,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_ACCEPT_CS", Value=99)]
      GM_ACCEPT_CS = 99,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_CONNECT_SC", Value=100)]
      GM_CONNECT_SC = 100,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_CONNECT_BC", Value=101)]
      GM_CONNECT_BC = 101,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_DISCONNECT_BC", Value=102)]
      GM_DISCONNECT_BC = 102,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_READY_CS", Value=103)]
      GM_READY_CS = 103,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_READY_BC", Value=104)]
      GM_READY_BC = 104,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_BEGIN_BC", Value=105)]
      GM_BEGIN_BC = 105,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_FRAME_CS", Value=106)]
      GM_FRAME_CS = 106,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_FRAME_BC", Value=107)]
      GM_FRAME_BC = 107,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_PING_CS", Value=108)]
      GM_PING_CS = 108,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GM_PING_SC", Value=109)]
      GM_PING_SC = 109,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MaxValue", Value=1000)]
      MaxValue = 1000
    }
  
}
// Generated from: scene.txt
namespace PBMessage
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GMPoint2D")]
  public partial class GMPoint2D : global::ProtoBuf.IExtensible
  {
    public GMPoint2D() {}
    
    private int _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GMPoint3D")]
  public partial class GMPoint3D : global::ProtoBuf.IExtensible
  {
    public GMPoint3D() {}
    
    private int _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private int _z;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GMPlayerInfo")]
  public partial class GMPlayerInfo : global::ProtoBuf.IExtensible
  {
    public GMPlayerInfo() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private int _moveSpeed;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"moveSpeed", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int moveSpeed
    {
      get { return _moveSpeed; }
      set { _moveSpeed = value; }
    }
    private int _moveSpeedAddition;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"moveSpeedAddition", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int moveSpeedAddition
    {
      get { return _moveSpeedAddition; }
      set { _moveSpeedAddition = value; }
    }
    private int _moveSpeedPercent;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"moveSpeedPercent", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int moveSpeedPercent
    {
      get { return _moveSpeedPercent; }
      set { _moveSpeedPercent = value; }
    }
    private int _attackSpeed;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"attackSpeed", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int attackSpeed
    {
      get { return _attackSpeed; }
      set { _attackSpeed = value; }
    }
    private int _attackSpeedAddition;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"attackSpeedAddition", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int attackSpeedAddition
    {
      get { return _attackSpeedAddition; }
      set { _attackSpeedAddition = value; }
    }
    private int _attackSpeedPercent;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"attackSpeedPercent", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int attackSpeedPercent
    {
      get { return _attackSpeedPercent; }
      set { _attackSpeedPercent = value; }
    }
    private int _maxBlood;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"maxBlood", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int maxBlood
    {
      get { return _maxBlood; }
      set { _maxBlood = value; }
    }
    private int _nowBlood;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"nowBlood", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int nowBlood
    {
      get { return _nowBlood; }
      set { _nowBlood = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(11, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Request")]
  public partial class GM_Request : global::ProtoBuf.IExtensible
  {
    public GM_Request() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Return")]
  public partial class GM_Return : global::ProtoBuf.IExtensible
  {
    public GM_Return() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Accept")]
  public partial class GM_Accept : global::ProtoBuf.IExtensible
  {
    public GM_Accept() {}

        private int _roleId;
        public int roleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        private int _conv;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"conv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int conv
    {
      get { return _conv; }
      set { _conv = value; }
    }
    private int _protocol;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"protocol", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int protocol
    {
      get { return _protocol; }
      set { _protocol = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Connect")]
  public partial class GM_Connect : global::ProtoBuf.IExtensible
  {
    public GM_Connect() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private PBMessage.GMPlayerInfo _player;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPlayerInfo player
    {
      get { return _player; }
      set { _player = value; }
    }
    private int _frameinterval;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"frameinterval", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int frameinterval
    {
      get { return _frameinterval; }
      set { _frameinterval = value; }
    }
    private int _mode;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"mode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mode
    {
      get { return _mode; }
      set { _mode = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Disconnect")]
  public partial class GM_Disconnect : global::ProtoBuf.IExtensible
  {
    public GM_Disconnect() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Ready")]
  public partial class GM_Ready : global::ProtoBuf.IExtensible
  {
    public GM_Ready() {}
    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private PBMessage.GMPoint3D _position;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D position
    {
      get { return _position; }
      set { _position = value; }
    }
    private PBMessage.GMPoint3D _direction;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"direction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public PBMessage.GMPoint3D direction
    {
      get { return _direction; }
      set { _direction = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GMCommand")]
  public partial class GMCommand : global::ProtoBuf.IExtensible
  {
    public GMCommand() {}

        public string eventType;

        private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private string _data;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string data
    {
      get { return _data; }
      set { _data = value; }
    }
    private long _frame;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frame
    {
      get { return _frame; }
      set { _frame = value; }
    }
    private long _frametime;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"frametime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frametime
    {
      get { return _frametime; }
      set { _frametime = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Frame")]
  public partial class GM_Frame : global::ProtoBuf.IExtensible
  {
    public GM_Frame() {}

    
    private int _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private long _frame;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frame
    {
      get { return _frame; }
      set { _frame = value; }
    }
    private long _frametime;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"frametime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frametime
    {
      get { return _frametime; }
      set { _frametime = value; }
    }
    private global::System.Collections.Generic.List<PBMessage.GMCommand> _command = new global::System.Collections.Generic.List<PBMessage.GMCommand>();
    [global::ProtoBuf.ProtoMember(4, Name=@"command", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PBMessage.GMCommand> command
    {
      get { return _command; }
            set { _command = value; }
        }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Frame_BC")]
  public partial class GM_Frame_BC : global::ProtoBuf.IExtensible
  {
    public GM_Frame_BC() {}
    
    private long _frame;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frame
    {
      get { return _frame; }
      set { _frame = value; }
    }
    private long _frametime;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"frametime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long frametime
    {
      get { return _frametime; }
      set { _frametime = value; }
    }
    private readonly global::System.Collections.Generic.List<PBMessage.GMCommand> _command = new global::System.Collections.Generic.List<PBMessage.GMCommand>();
    [global::ProtoBuf.ProtoMember(3, Name=@"command", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PBMessage.GMCommand> command
    {
      get { return _command; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GM_Begin")]
  public partial class GM_Begin : global::ProtoBuf.IExtensible
  {
    public GM_Begin() {}
    
    private int _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}