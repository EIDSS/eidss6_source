using System;
using System.Diagnostics.CodeAnalysis;

namespace BLToolkit.Mapping
{
	[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	[AttributeUsage(
		AttributeTargets.Class | AttributeTargets.Interface |
		AttributeTargets.Property | AttributeTargets.Field | 
		AttributeTargets.Enum,
		AllowMultiple=true)]
	public class NullValueAttribute : Attribute
	{
		public NullValueAttribute()
		{
		}

		public NullValueAttribute(object value)
		{
			_value = value;
		}

		public NullValueAttribute(Type type, object value)
		{
			_type  = type;
			_value = value;
		}

		private readonly object _value;
		public           object  Value
		{
			get { return _value; }
		}

		private readonly Type _type;
		public           Type  Type
		{
			get { return _type; }
		}
	}
}
