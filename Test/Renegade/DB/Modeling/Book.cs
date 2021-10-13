/**
 * Copyright (C) scenüs, 2021.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

using System;

namespace Renegade.DB.Modeling
{
	/// <summary>
	/// 
	/// </summary>
	public class Book
	{
		public long Id { get; set; }
		public string ISBN { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid RI { get; set; }
	}
}