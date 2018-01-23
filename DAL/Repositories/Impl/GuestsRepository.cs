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
	public class GuestsRepository : IGuestsRepository
	{
		private readonly string _connectionString;
		public GuestsRepository(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("connectionString is null or empty"); 
			}

			_connectionString = connectionString;
		}

		public IList<GuestsDal> GetAll()
		{
			var sql = @"SELECT * FROM Guests as g
						JOIN PaymentTypes as pt on pt.Id = g.PaymentTypeId
						JOIN Rooms as r on r.Id = g.RoomId
						JOIN RoomTypes as rt on rt.Id = r.RoomTypeId";
			IList<GuestsDal> rooms = new List<GuestsDal>();
			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				rooms = connection.Query<GuestsDal, PaymentTypesDal, RoomsDal, RoomTypesDal, GuestsDal>(sql, (guest, paymentType, room, roomType) =>
				{
					guest.Room = room;
					guest.PaymentType = paymentType;
					guest.Room.RoomType = roomType;
					return guest;
				}).ToList();
			}

			return rooms;
		}

		public GuestsDal GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
