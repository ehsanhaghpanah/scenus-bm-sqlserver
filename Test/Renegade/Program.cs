/**
 * Copyright (C) scenüs, 2008.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

using System;
using System.Linq;
using Newtonsoft.Json;

namespace Renegade
{
	/// <summary>
	/// 
	/// </summary>
	static public class Program
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		[STAThread]
		static public void Main()
		{
			try
			{
				var book = DB.Books.AddOne(new DB.Modeling.Book
				{
					Description = "test",
					ISBN = Guid.NewGuid().ToString().Replace("-", ""),
					Name = scenus.Test.Core.Generator.GetString(16)
				});
				Console.WriteLine(JsonConvert.SerializeObject(book, Formatting.Indented));

				var ls = DB.Books.GetAll();
				Console.WriteLine(ls.Count());
			}
			catch (Exception p)
			{
				logger.Error(p);
			}
		}
	}
}