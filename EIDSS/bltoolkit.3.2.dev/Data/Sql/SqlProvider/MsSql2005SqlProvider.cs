﻿using System;
using System.Data;
using BLToolkit.Reflection;

namespace BLToolkit.Data.Sql.SqlProvider
{
	using DataProvider;

	public class MsSql2005SqlProvider : MsSqlSqlProvider
	{
		public MsSql2005SqlProvider(DataProviderBase dataProvider) : base(dataProvider)
		{
		}

		public override ISqlExpression ConvertExpression(ISqlExpression expr)
		{
			expr = base.ConvertExpression(expr);

			if (expr is SqlFunction)
			{
				SqlFunction func = (SqlFunction)expr;

				switch (Type.GetTypeCode(TypeHelper.GetUnderlyingType(func.SystemType)))
				{
					case TypeCode.DateTime :

						if (func.Name == "Convert")
						{
							Type type1 = TypeHelper.GetUnderlyingType(func.Parameters[1].SystemType);

							if (IsTimeDataType(func.Parameters[0]))
							{
								if (type1 == typeof(DateTime) || type1 == typeof(DateTimeOffset))
									return new SqlExpression(
										func.SystemType, "Cast(Convert(Char, {0}, 114) as DateTime)", Precedence.Primary, func.Parameters[1]);

								if (func.Parameters[1].SystemType == typeof(string))
									return func.Parameters[1];

								return new SqlExpression(
									func.SystemType, "Convert(Char, {0}, 114)", Precedence.Primary, func.Parameters[1]);
							}

							if (type1 == typeof(DateTime) || type1 == typeof(DateTimeOffset))
							{
								if (IsDateDataType(func.Parameters[0], "Datetime"))
									return new SqlExpression(
										func.SystemType, "Cast(Floor(Cast({0} as Float)) as DateTime)", Precedence.Primary, func.Parameters[1]);
							}

							if (func.Parameters.Length == 2 && func.Parameters[0] is SqlDataType && func.Parameters[0] == SqlDataType.DateTime)
								return new SqlFunction(func.SystemType, func.Name, func.Precedence, func.Parameters[0], func.Parameters[1], new SqlValue(120));
						}

						break;
				}
			}

			return expr;
		}

		protected override void BuildDataType(System.Text.StringBuilder sb, SqlDataType type)
		{
			switch (type.DbType)
			{
				case SqlDbType.DateTimeOffset :
				case SqlDbType.Time           :
				case SqlDbType.Date           :
				case SqlDbType.DateTime2      : sb.Append("DateTime");        break;
				default                       : base.BuildDataType(sb, type); break;
			}
		}
	}
}
