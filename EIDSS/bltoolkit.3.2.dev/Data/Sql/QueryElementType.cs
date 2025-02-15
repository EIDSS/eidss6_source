﻿using System;

namespace BLToolkit.Data.Sql
{
	public enum QueryElementType
	{
		SqlField,
		SqlFunction,
		SqlParameter,
		SqlExpression,
		SqlBinaryExpression,
		SqlValue,
		SqlDataType,
		SqlTable,
			Join,
				JoinOn,

		ExprPredicate,
		NotExprPredicate,
		ExprExprPredicate,
		LikePredicate,
		BetweenPredicate,
		IsNullPredicate,
		InSubqueryPredicate,
		InListPredicate,
		FuncLikePredicate,

		SqlQuery,
			Column,
			SearchCondition,
				Condition,
			TableSource,
				JoinedTable,

			SelectClause,
			SetClause,
				SetExpression,
			FromClause,
			WhereClause,
			GroupByClause,
			OrderByClause,
				OrderByItem,
			Union,
	}
}
