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
	static public class Books
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		#region GetOne

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ky"></param>
		/// <returns></returns>
		static public Modeling.Book GetOne(ukey ky)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				return cn.Query<Modeling.Book>(@"
					SELECT * FROM [Samples].[Books] 
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
		/// <returns></returns>
		static public IEnumerable<Modeling.Book> GetAll()
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				return cn.Query<Modeling.Book>(@"
					SELECT * FROM [Samples].[Books]
					WITH (NOLOCK)",
					new
					{
					},
					commandType: CommandType.Text
				).ToList();
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "GetAll",
					a = new { },
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		static public IEnumerable<Modeling.Book> GetAll(Modeling.User item)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				return cn.Query<Modeling.Book>(@"
					SELECT * FROM [Samples].[Books] bs
					WITH (NOLOCK)
					INNER JOIN [Samples].[UserBooks] ub 
					WITH (NOLOCK)
					ON bs.Id = ub.BookId
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
		static public Modeling.Book AddOne(Modeling.Book item)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				var ar = cn.Execute(@"
					INSERT INTO [Samples].[Books]
					([ISBN], [Name], [Description])
					VALUES
					(@ISBN, @Name, @Description)",
					new
					{
						item.Name,
						item.ISBN,
						item.Description
					},
					commandType: CommandType.Text
				);
				if (ar != 1)
					throw new Exception(string.Format("affected-rows = {0}", ar));

				//
				// there must be one single book here,
				return cn.Query<Modeling.Book>(@"
					SELECT * 
					FROM [Samples].[Books] 
					WITH (NOLOCK)
					WHERE ([ISBN] = @ISBN)",
					new
					{
						item.ISBN
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="u"></param>
		/// <param name="g"></param>
		static public void Join(Modeling.User u, Modeling.Book g)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				var ar = cn.Execute(@"
					INSERT INTO [Samples].[UserBooks]
					([UserId], [BookId])
					VALUES
					(@UserId, @BookId)",
					new
					{
						UserId = u.Id,
						BookId = g.Id
					},
					commandType: CommandType.Text
				);
				if (ar != 1)
					throw new Exception(string.Format("affected-rows = {0}", ar));
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "Join",
					a = new { u, g },
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="u"></param>
		/// <param name="g"></param>
		static public void Drop(Modeling.User u, Modeling.Book g)
		{
			var cn = (SqlConnection) null;

			try
			{
				var cs = ConfigurationManager.ConnectionStrings["scenus"].ConnectionString;
				cn = new SqlConnection(cs);
				cn.Open();

				cn.Execute(@"
					DELETE FROM [Samples].[UserBooks]
					WHERE (([UserId] = @UserId) AND ([BookId] = @BookId))",
					new
					{
						UserId = u.Id,
						BookId = g.Id
					},
					commandType: CommandType.Text
				);
			}
			catch (Exception p)
			{
				logger.Error(JsonConvert.SerializeObject(new
				{
					m = "Drop",
					a = new { u, g },
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
	}
}