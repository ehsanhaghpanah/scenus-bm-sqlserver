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
	public class User
	{
		public long Id { get; set; }
		public string NameFrst { get; set; }
		public string NameLast { get; set; }
		public string MobileNumber { get; set; }
		public string EmailAddress { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid RI { get; set; }
	}
}