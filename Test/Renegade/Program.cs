/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Renegade
{
	/// <summary>
	/// https://docs.microsoft.com/en-us/archive/blogs/ricom/
	/// 
	/// </summary>
	static public class Program
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public delegate long Foo();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="n"></param>
		/// <param name="foo"></param>
		static public long Calculate(int n, Foo foo)
		{
			var results = new List<long>(n);
			for (int i = 0; i < n; i++)
			{
				results.Add(foo());
			}

			return (long) results.Average();
		}

		[STAThread]
		static public void Main()
		{
			logger.Info($"String.Method1 : {Calculate(10, TC.Strings.Method1)}");
			logger.Info($"String.Method2 : {Calculate(10, TC.Strings.Method2)}");
			logger.Info($"String.Method3 : {Calculate(10, TC.Strings.Method3)}");
			logger.Info($"String.Method4 : {Calculate(10, TC.Strings.Method4)}");
		}
	}
}