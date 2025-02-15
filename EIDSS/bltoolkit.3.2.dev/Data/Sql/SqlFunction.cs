﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BLToolkit.Data.Sql
{
	public class SqlFunction : ISqlTableSource
	{
		[Obsolete]
		public SqlFunction(string name, params ISqlExpression[] parameters)
			: this(null, name, Sql.Precedence.Primary, parameters)
		{
		}

		[Obsolete]
		public SqlFunction(string name, int precedence, params ISqlExpression[] parameters)
			: this(null, name, precedence, parameters)
		{
		}

		public SqlFunction(Type systemType, string name, params ISqlExpression[] parameters)
			: this(systemType, name, Sql.Precedence.Primary, parameters)
		{
		}

		public SqlFunction(Type systemType, string name, int precedence, params ISqlExpression[] parameters)
		{
			_sourceID = Interlocked.Increment(ref SqlQuery.SourceIDCounter);

			if (parameters == null) throw new ArgumentNullException("parameters");

			foreach (ISqlExpression p in parameters)
				if (p == null) throw new ArgumentNullException("parameters");

			_systemType = systemType;
			_name       = name;
			_precedence = precedence;
			_parameters = parameters;
		}

		readonly Type             _systemType; public Type             SystemType { get { return _systemType; } }
		readonly string           _name;       public string           Name       { get { return _name;       } }
		readonly int              _precedence; public int              Precedence { get { return _precedence; } }
		readonly ISqlExpression[] _parameters; public ISqlExpression[] Parameters { get { return _parameters; } }

		public static SqlFunction CreateCount (Type type, ISqlTableSource table) { return new SqlFunction(type, "Count",  table.All); }

		public static SqlFunction CreateAll   (SqlQuery subQuery) { return new SqlFunction(typeof(bool), "ALL",    Sql.Precedence.Comparison, subQuery); }
		public static SqlFunction CreateSome  (SqlQuery subQuery) { return new SqlFunction(typeof(bool), "SOME",   Sql.Precedence.Comparison, subQuery); }
		public static SqlFunction CreateAny   (SqlQuery subQuery) { return new SqlFunction(typeof(bool), "ANY",    Sql.Precedence.Comparison, subQuery); }
		public static SqlFunction CreateExists(SqlQuery subQuery) { return new SqlFunction(typeof(bool), "EXISTS", Sql.Precedence.Comparison, subQuery); }

		#region Overrides

#if OVERRIDETOSTRING

		public override string ToString()
		{
			return ((IQueryElement)this).ToString(new StringBuilder(), new Dictionary<IQueryElement,IQueryElement>()).ToString();
		}

#endif

		#endregion

		#region ISqlExpressionWalkable Members

		[Obsolete]
		ISqlExpression ISqlExpressionWalkable.Walk(bool skipColumns, Func<ISqlExpression,ISqlExpression> action)
		{
			for (int i = 0; i < _parameters.Length; i++)
				_parameters[i] = _parameters[i].Walk(skipColumns, action);

			return action(this);
		}

		#endregion

		#region IEquatable<ISqlExpression> Members

		bool IEquatable<ISqlExpression>.Equals(ISqlExpression other)
		{
			if (this == other)
				return true;

			SqlFunction func = other as SqlFunction;

			if (func == null || _name != func._name || _parameters.Length != func._parameters.Length && _systemType != func._systemType)
				return false;

			for (int i = 0; i < _parameters.Length; i++)
				if (!_parameters[i].Equals(func._parameters[i]))
					return false;

			return true;
		}

		#endregion

		#region ISqlTableSource Members

		private int _sourceID;
		public  int  SourceID { get { return _sourceID; } }

		SqlField _all;
		SqlField  ISqlTableSource.All
		{
			get
			{
				if (_all == null)
				{
					_all = new SqlField(null, "*", "*", true, -1, null, null);
					((IChild<ISqlTableSource>)_all).Parent = this;
				}

				return _all;
			}
		}

		IList<ISqlExpression> ISqlTableSource.GetKeys(bool allIfEmpty)
		{
			return null;
		}

		#endregion

		#region ISqlExpression Members

		public bool CanBeNull()
		{
			return true;
		}

		#endregion

		#region ICloneableElement Members

		public ICloneableElement Clone(Dictionary<ICloneableElement, ICloneableElement> objectTree, Predicate<ICloneableElement> doClone)
		{
			if (!doClone(this))
				return this;

			ICloneableElement clone;

			if (!objectTree.TryGetValue(this, out clone))
			{
				objectTree.Add(this, clone = new SqlFunction(
					_systemType,
					_name,
					_precedence,
					Array.ConvertAll<ISqlExpression,ISqlExpression>(_parameters, delegate(ISqlExpression e) { return (ISqlExpression)e.Clone(objectTree, doClone); })));
			}

			return clone;
		}

		#endregion

		#region IQueryElement Members

		public QueryElementType ElementType { get { return QueryElementType.SqlFunction; } }

		StringBuilder IQueryElement.ToString(StringBuilder sb, Dictionary<IQueryElement,IQueryElement> dic)
		{
			sb
				.Append(Name)
				.Append("(");

			foreach (ISqlExpression p in Parameters)
			{
				p.ToString(sb, dic);
				sb.Append(", ");
			}

			if (Parameters.Length > 0)
				sb.Length -= 2;

			return sb.Append(")");
		}

		#endregion
	}
}
