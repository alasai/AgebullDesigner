using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     数据类型映射表
    /// </summary>
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class DataTypeMapConfig : ConfigBase
    {
        #region 缺省

        /// <summary>
        ///     基本数据类型列表
        /// </summary>
        public static List<DataTypeMapConfig> DataTypeMap = new List<DataTypeMapConfig>
        {
            /* Object*/
            new DataTypeMapConfig
            {
                Name = "Object",
                Caption = "对象",
                CSharp = "object",
                Java = "object",
                Cpp = "-",
                MySql = "-",
                SqlServer = "-",
                Oracle = "-",
                JavaScript = "object"
            },
            /* Boolean*/
            new DataTypeMapConfig
            {
                Name = "Boolean",
                Caption = "布尔",
                CSharp = "bool",
                Java = "bool",
                Cpp = "bool",
                MySql = "BOOL",
                SqlServer = "bit",
                Oracle = "CHAR(1)",

                JavaScript = "bool"
            },
            /* String*/
            new DataTypeMapConfig
            {
                Datalen=200,
                Name = "String",
                Caption = "文本",
                CSharp = "string",
                Java = "string",
                Cpp = "string",
                MySql = "VARCHAR",
                SqlServer = "NVARCHAR",
                Oracle = "VARCHAR",
                JavaScript = "String"
            },
            /* DateTime*/
            new DataTypeMapConfig
            {
                Name = "DateTime",
                Caption = "日期时间",
                CSharp = "DateTime",
                Java = "DateTime",
                Cpp = "time_t",
                MySql = "DateTime",
                SqlServer = "DateTime",
                Oracle = "TIMESTAMP",
                JavaScript = "Date"
            },
            /* Guid*/
            new DataTypeMapConfig
            {
                Name = "Guid",
                Caption = "全局唯一标识符",
                CSharp = "Guid",
                Java = "uuid",
                Cpp = "uuid",
                MySql = "CHAR(36)",
                SqlServer = "UNIQUEIDENTIFIER",
                Oracle = "CHAR(36)",
                JavaScript = "Date"
            },
            /* Guid*/
            new DataTypeMapConfig
            {
                Name = "Enum",
                Caption = "枚举",
                CSharp = "int",
                Java = "int",
                Cpp = "int",
                MySql = "int",
                SqlServer = "int",
                Oracle = "NUMBER(10)",
                JavaScript = "Number"
            },
            /* Byte*/
            new DataTypeMapConfig
            {
                Name = "Byte",
                Caption = "有符号字节",
                CSharp = "byte",
                Java = "byte",
                Cpp = "unsigned char",
                MySql = "VarBinary",
                SqlServer = "VarBinary",
                Oracle = "NUMBER(3)",
                JavaScript = "byte"
            },
            /* SByte*/
            new DataTypeMapConfig
            {
                Name = "SByte",
                Caption = "无符号字节",
                CSharp = "sbyte",
                Java = "sbyte",
                Cpp = "char",
                MySql = "VarBinary",
                SqlServer = "VarBinary",
                Oracle = "NUMBER(3)",
                JavaScript = "byte"
            },
            /* UInt16*/
            new DataTypeMapConfig
            {
                Name = "UInt16",
                Caption = "无符号短整数",
                CSharp = "ushort",
                Java = "ushort",
                Cpp = "unsigned short",
                MySql = "SMALLINT UNSIGNED",
                SqlServer = "smallint",
                Oracle = "NUMBER(5)",
                JavaScript = "Number"
            },
            /* Int16*/
            new DataTypeMapConfig
            {
                Name = "Int16",
                Caption = "有符号短整数",
                CSharp = "short",
                Java = "short",
                Cpp = "short",
                MySql = "SMALLINT",
                SqlServer = "int",
                Oracle = "NUMBER(5)",
                JavaScript = "Number"
            },
            /* UInt32*/
            new DataTypeMapConfig
            {
                Name = "UInt32",
                Caption = "无符号32位整数",
                CSharp = "uint",
                Java = "uint",
                Cpp = "unsigned int",
                MySql = "INT UNSIGNED",
                SqlServer = "BIGINT",
                Oracle = "NUMBER(10)",
                JavaScript = "Number"
            },
            /* Int32*/
            new DataTypeMapConfig
            {
                Name = "Int32",
                Caption = "有符号32位整数",
                CSharp = "int",
                Java = "int",
                Cpp = "int",
                MySql = "INT",
                SqlServer = "INT",
                Oracle = "NUMBER(10)",
                JavaScript = "Number"
            },
            /* UInt64*/
            new DataTypeMapConfig
            {
                Name = "UInt64",
                Caption = "无符号64位整数",
                CSharp = "ulong",
                Java = "ulong",
                Cpp = "unsigned long long",
                MySql = "BIGINT UNSIGNED",
                SqlServer = "real",
                Oracle = "NUMBER(19)",
                JavaScript = "Number"
            },
            /* Int64*/
            new DataTypeMapConfig
            {
                Name = "Int64",
                Caption = "有符号64位整数",
                CSharp = "long",
                Java = "long",
                Cpp = "long long",
                MySql = "BIGINT",
                SqlServer = "BIGINT",
                Oracle = "NUMBER(19)",
                JavaScript = "Number"
            },
            /* Single */
            new DataTypeMapConfig
            {
                Name = "Single",
                Caption = "单精度小数",
                CSharp = "float",
                Java = "float",
                Cpp = "float",
                MySql = "FLOAT",
                SqlServer = "real",
                JavaScript = "Number",
                Oracle = "BINARY_FLOAT"
            },
            /* Double*/
            new DataTypeMapConfig
            {
                Name = "Double",
                Caption = "双精度小数",
                CSharp = "double",
                Java = "double",
                Cpp = "double",
                MySql = "DOUBLE",
                SqlServer = "float",
                JavaScript = "Number",
                Oracle = "BINARY_DOUBLE"
            },
            /* Decimal*/
            new DataTypeMapConfig
            {
                Name = "Decimal",
                Caption = "实数",
                CSharp = "decimal",
                Java = "decimal",
                Cpp = "-",
                MySql = "DECIMAL(18,8)",
                SqlServer = "DECIMAL(18,8)",
                JavaScript = "Number",
                Oracle = "DECIMAL(33,3)"
            }
        };

        #endregion

        #region 设计标识

        /// <summary>
        /// 数据长度
        /// </summary>
        [DataMember, JsonProperty("datalen", NullValueHandling = NullValueHandling.Ignore)]
        internal int _datalen;

        /// <summary>
        /// 数据长度
        /// </summary>
        /// <remark>
        /// 文本或二进制存储的最大长度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据长度"), Description("文本或二进制存储的最大长度")]
        public int Datalen
        {
            get => _datalen;
            set
            {
                if (_datalen == value)
                    return;
                BeforePropertyChanged(nameof(Datalen), _datalen, value);
                _datalen = value;
                OnPropertyChanged(nameof(Datalen));
            }
        }
        /// <summary>
        /// 存储精度
        /// </summary>
        [DataMember, JsonProperty("scale", NullValueHandling = NullValueHandling.Ignore)]
        internal int _scale;

        /// <summary>
        /// 存储精度
        /// </summary>
        /// <remark>
        /// 存储精度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储精度"), Description("存储精度")]
        public int Scale
        {
            get => _scale;
            set
            {
                if (_scale == value)
                    return;
                BeforePropertyChanged(nameof(Scale), _scale, value);
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }
        /// <summary>
        ///     C#
        /// </summary>
        [DataMember]
        [JsonProperty("csharp", NullValueHandling = NullValueHandling.Ignore)]
        private string _csharp;

        /// <summary>
        ///     C#
        /// </summary>
        /// <remark>
        ///     C#
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("C#")]
        public string CSharp
        {
            get => _csharp;
            set
            {
                if (_csharp == value)
                    return;
                BeforePropertyChanged(nameof(CSharp), _csharp, value);
                _csharp = value;
                OnPropertyChanged(nameof(CSharp));
            }
        }

        /// <summary>
        ///     C++
        /// </summary>
        [DataMember]
        [JsonProperty("cpp", NullValueHandling = NullValueHandling.Ignore)]
        private string _cpp;

        /// <summary>
        ///     C++
        /// </summary>
        /// <remark>
        ///     C++
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("C++")]
        public string Cpp
        {
            get => _cpp;
            set
            {
                if (_cpp == value)
                    return;
                BeforePropertyChanged(nameof(Cpp), _cpp, value);
                _cpp = value;
                OnPropertyChanged(nameof(Cpp));
            }
        }


        /// <summary>
        ///     Java
        /// </summary>
        [DataMember]
        [JsonProperty("java", NullValueHandling = NullValueHandling.Ignore)]
        private string _java;

        /// <summary>
        ///     Java
        /// </summary>
        /// <remark>
        ///     Java
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("Java")]
        public string Java
        {
            get => _java;
            set
            {
                if (_java == value)
                    return;
                BeforePropertyChanged(nameof(Java), _java, value);
                _java = value;
                OnPropertyChanged(nameof(Java));
            }
        }

        /// <summary>
        ///     Golang
        /// </summary>
        [DataMember]
        [JsonProperty("golang", NullValueHandling = NullValueHandling.Ignore)]
        private string _golang;

        /// <summary>
        ///     Golang
        /// </summary>
        /// <remark>
        ///     Golang
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("Golang")]
        public string Golang
        {
            get => _golang;
            set
            {
                if (_golang == value)
                    return;
                BeforePropertyChanged(nameof(Golang), _golang, value);
                _golang = value;
                OnPropertyChanged(nameof(Golang));
            }
        }

        /// <summary>
        ///     MySql
        /// </summary>
        [DataMember]
        [JsonProperty("mysql", NullValueHandling = NullValueHandling.Ignore)]
        private string _mysql;

        /// <summary>
        ///     MySql
        /// </summary>
        /// <remark>
        ///     MySql
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("MySql")]
        public string MySql
        {
            get => _mysql;
            set
            {
                if (_mysql == value)
                    return;
                BeforePropertyChanged(nameof(MySql), _mysql, value);
                _mysql = value;
                OnPropertyChanged(nameof(MySql));
            }
        }

        /// <summary>
        ///     SqlServer
        /// </summary>
        [DataMember]
        [JsonProperty("sqlServer", NullValueHandling = NullValueHandling.Ignore)]
        private string _sqlServer;

        /// <summary>
        ///     SqlServer
        /// </summary>
        /// <remark>
        ///     SqlServer
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("SqlServer")]
        public string SqlServer
        {
            get => _sqlServer;
            set
            {
                if (_sqlServer == value)
                    return;
                BeforePropertyChanged(nameof(SqlServer), _sqlServer, value);
                _sqlServer = value;
                OnPropertyChanged(nameof(SqlServer));
            }
        }

        /// <summary>
        ///     Oracle
        /// </summary>
        [DataMember]
        [JsonProperty("oracle", NullValueHandling = NullValueHandling.Ignore)]
        private string _oracle;

        /// <summary>
        ///     Oracle
        /// </summary>
        /// <remark>
        ///     Oracle
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("Oracle")]
        public string Oracle
        {
            get => _oracle;
            set
            {
                if (_oracle == value)
                    return;
                BeforePropertyChanged(nameof(Oracle), _oracle, value);
                _oracle = value;
                OnPropertyChanged(nameof(Oracle));
            }
        }
        /// <summary>
        ///     Oracle
        /// </summary>
        [DataMember]
        [JsonProperty("sqlite", NullValueHandling = NullValueHandling.Ignore)]
        private string _sqlite;

        /// <summary>
        ///     Oracle
        /// </summary>
        /// <remark>
        ///     Oracle
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("Oracle")]
        public string Sqlite
        {
            get => _sqlite;
            set
            {
                if (_sqlite == value)
                    return;
                BeforePropertyChanged(nameof(Sqlite), _sqlite, value);
                _sqlite = value;
                OnPropertyChanged(nameof(Sqlite));
            }
        }
        

        /// <summary>
        ///     JavaScript
        /// </summary>
        [DataMember]
        [JsonProperty("js", NullValueHandling = NullValueHandling.Ignore)]
        private string _js;

        /// <summary>
        ///     JavaScript
        /// </summary>
        /// <remark>
        ///     JavaScript
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("语言")]
        [DisplayName("JavaScript")]
        public string JavaScript
        {
            get => _js;
            set
            {
                if (_js == value)
                    return;
                BeforePropertyChanged(nameof(JavaScript), _js, value);
                _js = value;
                OnPropertyChanged(nameof(JavaScript));
            }
        }

        #endregion
    }
}