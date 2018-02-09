using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Models;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Impl
{
	public class RoomsRepository : IRoomsRepository
	{
		private readonly string _connectionString;

		public RoomsRepository(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("connectionString is Null or Whitespace");
			} 

			_connectionString = connectionString;
		}

		public IList<RoomsDal> GetAll()
		{
			var sql = @"SELECT * FROM Rooms AS r 
							JOIN RoomTypes AS rt ON rt.Id = r.RoomTypeId";

			IList<RoomsDal> roomsCollection = GetRooms(sql);
			return roomsCollection;
		}

		public RoomsDal GetById(int id)
		{
			var sql = $@"SELECT * FROM Rooms AS r
							JOIN RoomTypes as rt ON rt.Id = r.RoomTypeId
						WHERE r.Id = {id}";

			RoomsDal room = GetRoom(sql);
			return room;
		}

		public bool Create(RoomsDal item)
		{
			var sql = @"INSERT dbo.Rooms([RoomNumber],[RoomTypeId],[RoomLetter],[Floor]) 
						VALUES (@roomNumber, @roomTypeId, @roomLetter, @floor)";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rowsAffected = connection.Execute(sql,
					new
					{
						roomNumber = item.RoomNumber,
						roomTypeId = item.RoomType.Id,
						roomLetter = item.RoomLetter,
						floor = item.Floor
					});

				return rowsAffected > 0;
			}
		}

		public bool Delete(int id)
		{
			var sql = @"DELETE FROM dbo.Rooms WHERE Id = @identifier;";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rowsAffected = connection.Execute(sql, new
				{
					identifier = id
				});

				return rowsAffected > 0;
			}
		}

		public bool Update(RoomsDal item)
		{
			var sql = @"UPDATE dbo.Rooms SET 
							[RoomNumber] = @roomNumber, 
							[RoomLetter] = @roomLetter,
							[RoomTypeId] = @roomTypeId,
							[Floor] = @floor
						WHERE Id = @id";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rowsAffected = connection.Execute(sql, new
				{
					roomNumber = item.RoomNumber,
					roomTypeId = item.RoomType.Id,
					roomLetter = item.RoomLetter,
					floor = item.Floor,
					id = item.Id
				});

				return rowsAffected > 0;
			}
		}

		private IList<RoomsDal> GetRooms(string sql)
		{
			IList<RoomsDal> rooms = new List<RoomsDal>();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				rooms = connection.Query<RoomsDal, RoomTypesDal, RoomsDal>(sql, (roomsDal, roomTypesDal) =>
				{
					roomsDal.RoomType = roomTypesDal;
					return roomsDal;
				}).ToList();
			}

			return rooms;
		}

		private RoomsDal GetRoom(string sql)
		{
			RoomsDal room = new RoomsDal();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				room = connection.Query<RoomsDal, RoomTypesDal, RoomsDal>(sql, (roomsDal, roomTypesDal) =>
				{
					roomsDal.RoomType = roomTypesDal;
					return roomsDal;
				}).ToList().FirstOrDefault();
			}

			return room;
		}
	}
}
