﻿using System;
using System.Collections.Generic;

namespace BLToolkit.Data.Sql
{
	public interface ISqlTableSource : ISqlExpression
	{
		SqlField              All      { get; }
		int                   SourceID { get; }
		IList<ISqlExpression> GetKeys(bool allIfEmpty);
	}
}
