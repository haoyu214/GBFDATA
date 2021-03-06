// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Place.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Place {

  /// <summary>Holder for reflection information generated from Place.proto</summary>
  public static partial class PlaceReflection {

    #region Descriptor
    /// <summary>File descriptor for Place.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PlaceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtQbGFjZS5wcm90bxIFcGxhY2UiDgoMUGxhY2VMaXN0UmVxIrkBCgxQbGFj",
            "ZUxpc3RSc3ASLQoKcGxhY2VfbGlzdBgBIAMoCzIZLnBsYWNlLlBsYWNlTGlz",
            "dFJzcC5QbGFjZRp6CgVQbGFjZRISCgpjb21wYW55X2lkGAEgASgJEhQKDGNv",
            "bXBhbnlfbmFtZRgCIAEoCRIOCgZzdGF0dXMYAyABKAUSEQoJYXJlYV9jb2Rl",
            "GAQgASgJEhMKC2NyZWF0ZV90aW1lGAUgASgJEg8KB2FkZHJlc3MYBiABKAky",
            "QwoFUGxhY2USOgoMR2V0UGxhY2VMaXN0EhMucGxhY2UuUGxhY2VMaXN0UmVx",
            "GhMucGxhY2UuUGxhY2VMaXN0UnNwIgBCKAoSY29tLnJhaW5zb2Z0LnByb3Rv",
            "QgpQbGFjZVByb3RvUAGiAgNITFdiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Place.PlaceListReq), global::Place.PlaceListReq.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Place.PlaceListRsp), global::Place.PlaceListRsp.Parser, new[]{ "PlaceList" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Place.PlaceListRsp.Types.Place), global::Place.PlaceListRsp.Types.Place.Parser, new[]{ "CompanyId", "CompanyName", "Status", "AreaCode", "CreateTime", "Address" }, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PlaceListReq : pb::IMessage<PlaceListReq> {
    private static readonly pb::MessageParser<PlaceListReq> _parser = new pb::MessageParser<PlaceListReq>(() => new PlaceListReq());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlaceListReq> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Place.PlaceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListReq() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListReq(PlaceListReq other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListReq Clone() {
      return new PlaceListReq(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlaceListReq);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlaceListReq other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlaceListReq other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class PlaceListRsp : pb::IMessage<PlaceListRsp> {
    private static readonly pb::MessageParser<PlaceListRsp> _parser = new pb::MessageParser<PlaceListRsp>(() => new PlaceListRsp());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlaceListRsp> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Place.PlaceReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListRsp() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListRsp(PlaceListRsp other) : this() {
      placeList_ = other.placeList_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceListRsp Clone() {
      return new PlaceListRsp(this);
    }

    /// <summary>Field number for the "place_list" field.</summary>
    public const int PlaceListFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Place.PlaceListRsp.Types.Place> _repeated_placeList_codec
        = pb::FieldCodec.ForMessage(10, global::Place.PlaceListRsp.Types.Place.Parser);
    private readonly pbc::RepeatedField<global::Place.PlaceListRsp.Types.Place> placeList_ = new pbc::RepeatedField<global::Place.PlaceListRsp.Types.Place>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Place.PlaceListRsp.Types.Place> PlaceList {
      get { return placeList_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlaceListRsp);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlaceListRsp other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!placeList_.Equals(other.placeList_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= placeList_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      placeList_.WriteTo(output, _repeated_placeList_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += placeList_.CalculateSize(_repeated_placeList_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlaceListRsp other) {
      if (other == null) {
        return;
      }
      placeList_.Add(other.placeList_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            placeList_.AddEntriesFrom(input, _repeated_placeList_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the PlaceListRsp message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class Place : pb::IMessage<Place> {
        private static readonly pb::MessageParser<Place> _parser = new pb::MessageParser<Place>(() => new Place());
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Place> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Place.PlaceListRsp.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Place() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Place(Place other) : this() {
          companyId_ = other.companyId_;
          companyName_ = other.companyName_;
          status_ = other.status_;
          areaCode_ = other.areaCode_;
          createTime_ = other.createTime_;
          address_ = other.address_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Place Clone() {
          return new Place(this);
        }

        /// <summary>Field number for the "company_id" field.</summary>
        public const int CompanyIdFieldNumber = 1;
        private string companyId_ = "";
        /// <summary>
        /// 网吧编号
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string CompanyId {
          get { return companyId_; }
          set {
            companyId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "company_name" field.</summary>
        public const int CompanyNameFieldNumber = 2;
        private string companyName_ = "";
        /// <summary>
        /// 网吧名称
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string CompanyName {
          get { return companyName_; }
          set {
            companyName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "status" field.</summary>
        public const int StatusFieldNumber = 3;
        private int status_;
        /// <summary>
        ///营业状�?        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int Status {
          get { return status_; }
          set {
            status_ = value;
          }
        }

        /// <summary>Field number for the "area_code" field.</summary>
        public const int AreaCodeFieldNumber = 4;
        private string areaCode_ = "";
        /// <summary>
        ///区域编码
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string AreaCode {
          get { return areaCode_; }
          set {
            areaCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "create_time" field.</summary>
        public const int CreateTimeFieldNumber = 5;
        private string createTime_ = "";
        /// <summary>
        /// 通讯时间
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string CreateTime {
          get { return createTime_; }
          set {
            createTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "address" field.</summary>
        public const int AddressFieldNumber = 6;
        private string address_ = "";
        /// <summary>
        ///地址
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Address {
          get { return address_; }
          set {
            address_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as Place);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(Place other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (CompanyId != other.CompanyId) return false;
          if (CompanyName != other.CompanyName) return false;
          if (Status != other.Status) return false;
          if (AreaCode != other.AreaCode) return false;
          if (CreateTime != other.CreateTime) return false;
          if (Address != other.Address) return false;
          return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (CompanyId.Length != 0) hash ^= CompanyId.GetHashCode();
          if (CompanyName.Length != 0) hash ^= CompanyName.GetHashCode();
          if (Status != 0) hash ^= Status.GetHashCode();
          if (AreaCode.Length != 0) hash ^= AreaCode.GetHashCode();
          if (CreateTime.Length != 0) hash ^= CreateTime.GetHashCode();
          if (Address.Length != 0) hash ^= Address.GetHashCode();
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (CompanyId.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(CompanyId);
          }
          if (CompanyName.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(CompanyName);
          }
          if (Status != 0) {
            output.WriteRawTag(24);
            output.WriteInt32(Status);
          }
          if (AreaCode.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(AreaCode);
          }
          if (CreateTime.Length != 0) {
            output.WriteRawTag(42);
            output.WriteString(CreateTime);
          }
          if (Address.Length != 0) {
            output.WriteRawTag(50);
            output.WriteString(Address);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (CompanyId.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(CompanyId);
          }
          if (CompanyName.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(CompanyName);
          }
          if (Status != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Status);
          }
          if (AreaCode.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(AreaCode);
          }
          if (CreateTime.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(CreateTime);
          }
          if (Address.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Address);
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Place other) {
          if (other == null) {
            return;
          }
          if (other.CompanyId.Length != 0) {
            CompanyId = other.CompanyId;
          }
          if (other.CompanyName.Length != 0) {
            CompanyName = other.CompanyName;
          }
          if (other.Status != 0) {
            Status = other.Status;
          }
          if (other.AreaCode.Length != 0) {
            AreaCode = other.AreaCode;
          }
          if (other.CreateTime.Length != 0) {
            CreateTime = other.CreateTime;
          }
          if (other.Address.Length != 0) {
            Address = other.Address;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                input.SkipLastField();
                break;
              case 10: {
                CompanyId = input.ReadString();
                break;
              }
              case 18: {
                CompanyName = input.ReadString();
                break;
              }
              case 24: {
                Status = input.ReadInt32();
                break;
              }
              case 34: {
                AreaCode = input.ReadString();
                break;
              }
              case 42: {
                CreateTime = input.ReadString();
                break;
              }
              case 50: {
                Address = input.ReadString();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
