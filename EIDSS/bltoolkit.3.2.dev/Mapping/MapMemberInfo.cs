using System;
using System.Data;
using System.Diagnostics;

using BLToolkit.Reflection;
using BLToolkit.Reflection.Extension;

namespace BLToolkit.Mapping
{
	[DebuggerStepThrough]
	public class MapMemberInfo
	{
		private MemberAccessor _memberAccessor;
		public  MemberAccessor  MemberAccessor
		{
			get { return _memberAccessor;  }
			set { _memberAccessor = value; }
		}

		private MemberAccessor _complexMemberAccessor;
		public  MemberAccessor  ComplexMemberAccessor
		{
			get { return _complexMemberAccessor;  }
			set { _complexMemberAccessor = value; }
		}

		private string _name;            public string  Name                       { get { return _name;            } set { _name         = value;    } }
		private string _memberName;      public string  MemberName                 { get { return _memberName;      } set { _memberName   = value;    } }
		private string _storage;         public string  Storage                    { get { return _storage;         } set { _storage      = value;    } }
		private bool   _isDiscriminator; public bool    IsInheritanceDiscriminator { get { return _isDiscriminator; } set { _isDiscriminator = value; } }
		private bool   _trimmable;       public bool    Trimmable                  { get { return _trimmable;       } set { _trimmable    = value;    } }
		private bool   _sqlIgnore;       public bool    SqlIgnore                  { get { return _sqlIgnore;       } set { _sqlIgnore    = value;    } }
		private bool   _nullable;        public bool    Nullable                   { get { return _nullable;        } set { _nullable     = value;    } }
		private object _nullValue;       public object  NullValue                  { get { return _nullValue;       } set { _nullValue    = value;    } }
		private object _defaultValue;    public object  DefaultValue               { get { return _defaultValue;    } set { _defaultValue = value;    } }
		private Type   _type;            public Type    Type                       { get { return _type;            } set { _type         = value;    } }
	    private int    _dbSize;          public int     DbSize                     { get { return _dbSize;          } set { _dbSize       = value;    } }
        private bool   _isDbTypeSet;     public bool    IsDbTypeSet                { get { return _isDbTypeSet;     } set { _isDbTypeSet  = value;    } }
        private bool   _isDbSizeSet;     public bool    IsDbSizeSet                { get { return _isDbSizeSet;     } set { _isDbSizeSet  = value;    } }

		private MappingSchema _mappingSchema;
		public  MappingSchema  MappingSchema
		{
			get { return _mappingSchema;  }
			set { _mappingSchema = value; }
		}

		private MapValue[] _mapValues;
		public  MapValue[]  MapValues
		{
			get { return _mapValues;  }
			set { _mapValues = value; }
		}

		private MemberExtension _memberExtension;
		public  MemberExtension  MemberExtension
		{
			get { return _memberExtension;  }
			set { _memberExtension = value; }
		}

		private DbType _dbType = DbType.Object;
		public  DbType  DbType
		{
			get { return _dbType; }
			set { _dbType = value; }
		}
	}
}
