using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Models;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Impl
{
	public class RoomTypeRepository : IRoomTypeRepository
	{
		private readonly string _connectionString;

		public RoomTypeRepository(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("connectionString is Null or Whitespace");
			}

			_connectionString = connectionString;
		}

		public IList<RoomTypesDal> GetAll()
		{
			var sql = @"SELECT * FROM dbo.RoomTypes;";

			IList<RoomTypesDal> roomTypes = new List<RoomTypesDal>();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				roomTypes = connection.Query<RoomTypesDal>(sql).ToList();
			}

			return roomTypes;
		}

		public RoomTypesDal GetById(int id)
		{
			var sql = @"SELECT * FROM dbo.RoomTypes WHERE Id = @identifier";

			RoomTypesDal roomTypes = new RoomTypesDal();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				roomTypes = connection.Query<RoomTypesDal>(sql, new
				{
					identifier = id
				}).ToList().FirstOrDefault();
			}

			return roomTypes;
		}

		public bool Create(RoomTypesDal item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(RoomTypesDal item)
		{
			throw new NotImplementedException();
		}
	}
}
