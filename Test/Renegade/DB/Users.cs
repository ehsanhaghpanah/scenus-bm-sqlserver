/**
 * Copyright (C) scenüs, 2021.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@scenus.com
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Newtonsoft.Json;
using ukey = System.String;

namespace Renegade.DB
{
	/// <summary>
	/// 
	/// </summary>
	static public class Users
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		#region GetOne

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ky"></param>
		/// <returns></returns>
		static public Modeling.User GetOne(ukey ky)
		{
			var cn = (SqlConnection)null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				return cn.Query<Modeling.User>(@"
					SELECT * FROM [Samples].[Users] 
					WITH (NOLOCK)
					WHERE (([Name] = @Ky) OR ([RI] = @Ky))",
					new
					{
						Ky = ky
					},
					commandType: CommandType.Text
				).SingleOrDefault();
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "GetOne",
					a = new { ky },
					p
				}, Formatting.Indented));
				throw;
			}
			finally
			{
				if (cn != null)
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
				}
			}
		}

		#endregion

		#region GetAll

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		static public IEnumerable<Modeling.User> GetAll(Modeling.User item)
		{
			var cn = (SqlConnection)null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				return cn.Query<Modeling.User>(@"
					SELECT * FROM [Samples].[Users] bs
					WITH (NOLOCK)
					INNER JOIN [Samples].[UserUsers] ub 
					WITH (NOLOCK)
					ON bs.Id = ub.UserId
					WHERE (ub.UserId = @UserId)",
					new
					{
						UserId = item.Id
					},
					commandType: CommandType.Text
				).ToList();
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "GetAll",
					a = new { item },
					p
				}, Formatting.Indented));
				throw;
			}
			finally
			{
				if (cn != null)
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
				}
			}
		}

		#endregion

		#region AddOne|Change|Remove|Delete

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		static public Modeling.User AddOne(Modeling.User item)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				var ar = cn.Execute(@"
					INSERT INTO [Samples].[Users]
					([NameFrst], [NameLast], [MobileNumber], [EmailAddress])
					VALUES
					(@NameFrst, @NameLast, @MobileNumber, @EmailAddress)",
					new
					{
						item.NameFrst,
						item.NameLast,
						item.MobileNumber,
						item.EmailAddress
					},
					commandType: CommandType.Text
				);
				if (ar != 1)
					throw new Exception(string.Format("affected-rows = {0}", ar));

				//
				// there must be one single user here,
				return cn.Query<Modeling.User>(@"
					SELECT * 
					FROM [Samples].[Users] 
					WITH (NOLOCK)
					WHERE ([MobileNumber] = @MobileNumber)",
					new
					{
						item.MobileNumber
					},
					commandType: CommandType.Text
				).Single();
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "AddOne",
					a = new { item },
					p
				}, Formatting.Indented));
				throw;
			}
			finally
			{
				if (cn != null)
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
				}
			}
		}

		#endregion
	}
}