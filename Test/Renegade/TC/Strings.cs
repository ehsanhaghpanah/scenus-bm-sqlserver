/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Renegade.TC
{
	/// <summary>
	/// beginging-from = 0
	/// total-1-digits-length = 10
	/// total-2-digits-length = (90 * 2) = 180
	/// total-3-digits-length = (900 * 3) = 2700
	/// total-4-digits-length = (9000 * 4) = 36000
	/// total-5-digits-length = 20000 (up-to : 30000) = (20000 * 5) = 100000
	/// total = 138890
	/// </summary>
	static public class Strings
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		/// <summary>
		/// 
		/// </summary>
		static public long Method1()
		{
			var sw = Stopwatch.StartNew();
			var ss = string.Empty;

			for (int i = 0; i < 30000; i++)
			{
				ss = ss + i;
			}

			sw.Stop();

			logger.Trace(ss.Length);
			logger.Trace($"TimeElapsed={sw.ElapsedMilliseconds}");

			return sw.ElapsedMilliseconds;
		}

		/// <summary>
		/// wrong usage of a method!
		/// </summary>
		static public long Method2()
		{
			var sw = Stopwatch.StartNew();
			var ss = string.Empty;

			for (int i = 0; i < 30000; i++)
			{
				ss = String.Concat(ss, i);
			}

			sw.Stop();

			logger.Trace(ss.Length);
			logger.Trace($"TimeElapsed={sw.ElapsedMilliseconds}");

			return sw.ElapsedMilliseconds;
		}

		/// <summary>
		/// 
		/// </summary>
		static public long Method3()
		{
			var sw = Stopwatch.StartNew();
			var sl = new List<string>();

			for (int i = 0; i < 30000; i++)
			{
				sl.Add(i.ToString());
			}
			var ss = String.Concat(sl);

			sw.Stop();

			logger.Trace(ss.Length);
			logger.Trace($"TimeElapsed={sw.ElapsedMilliseconds}");

			return sw.ElapsedMilliseconds;
		}

		/// <summary>
		/// 
		/// </summary>
		static public long Method4()
		{
			var sw = Stopwatch.StartNew();
			var sl = new StringBuilder();

			for (int i = 0; i < 30000; i++)
			{
				sl.Append(i.ToString());
			}

			sw.Stop();

			logger.Trace(sl.Length);
			logger.Trace($"TimeElapsed={sw.ElapsedMilliseconds}");

			return sw.ElapsedMilliseconds;
		}
	}
}
