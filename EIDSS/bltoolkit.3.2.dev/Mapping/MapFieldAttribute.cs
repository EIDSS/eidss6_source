using System;
using System.Diagnostics;

namespace BLToolkit.Mapping
{
	[AttributeUsage(
		AttributeTargets.Field | AttributeTargets.Property |
		AttributeTargets.Class | AttributeTargets.Interface,
		AllowMultiple=true)]
	public sealed class MapFieldAttribute : Attribute
	{
		public MapFieldAttribute()
		{
		}

		public MapFieldAttribute(string mapName)
		{
			_mapName = mapName;
		}

		public MapFieldAttribute(string mapName, string origName)
		{
			_mapName  = mapName;
			_origName = origName;
		}

		private string _mapName;
		public  string  MapName
		{
			[DebuggerStepThrough]
			get { return _mapName;  }
			set { _mapName = value; }
		}

		private string _origName;
		public  string  OrigName
		{
			[DebuggerStepThrough]
			get { return _origName;  }
			set { _origName = value; }
		}

		private string _format;
		public  string  Format
		{
			[DebuggerStepThrough]
			get { return _format;  }
			set { _format = value; }
		}

		private string _storage;
		public  string  Storage
		{
			[DebuggerStepThrough]
			get { return _storage;  }
			set { _storage = value; }
		}

		private bool _isInheritanceDiscriminator;
		public  bool  IsInheritanceDiscriminator
		{
			[DebuggerStepThrough]
			get { return _isInheritanceDiscriminator;  }
			set { _isInheritanceDiscriminator = value; }
		}
	}
}
