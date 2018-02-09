using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
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
			var sql = @"SELECT * FROM Guests AS g
							JOIN PaymentTypes as pt ON pt.Id = g.PaymentTypeId
							JOIN Rooms as r ON r.Id = g.RoomId
							JOIN RoomTypes as rt ON rt.Id = r.RoomTypeId";

			IList<GuestsDal> guests = GetGuests(sql);
			return guests;
		}

		public GuestsDal GetById(int id)
		{
			var sql = $@"SELECT * FROM dbo.Guests AS g 
							JOIN PaymentTypes as pt ON pt.Id = g.PaymentTypeId
							JOIN Rooms as r ON r.Id = g.RoomId
							JOIN RoomTypes as rt ON rt.Id = r.RoomTypeId
						WHERE g.Id = {id}";

			GuestsDal guest = GetGuest(sql);

			return guest;
		}

		public IList<GuestsDal> GetGuestsFromRoomWithTimePeriod(int roomId, DateTime dateFrom, DateTime dateTo)
		{
			var sql = $@"SELECT * FROM dbo.Guests as g 
							JOIN PaymentTypes as pt on pt.Id = g.PaymentTypeId
							JOIN Rooms as r on r.Id = g.RoomId
							JOIN RoomTypes as rt on rt.Id = r.RoomTypeId
						WHERE g.DateFrom < '{dateTo}' and g.DateTo > '{dateFrom}' and g.RoomId = {roomId}";

			IList<GuestsDal> guests = GetGuests(sql);

			return guests;
		}

		public IList<GuestsDal> GetGuestsFromTimePeriod(DateTime dateFrom, DateTime dateTo)
		{
			var sql = $@"SELECT * FROM dbo.Guests AS g 
							JOIN PaymentTypes as pt ON pt.Id = g.PaymentTypeId
							JOIN Rooms as r ON r.Id = g.RoomId
							JOIN RoomTypes as rt ON rt.Id = r.RoomTypeId
						WHERE g.DateFrom < '{dateTo}' AND g.DateTo > '{dateFrom}';";

			IList<GuestsDal> guests = GetGuests(sql);

			return guests;
		}

		private IList<GuestsDal> GetGuests(string sql)
		{
			IList<GuestsDal> guests = new List<GuestsDal>();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				guests = connection.Query<GuestsDal, PaymentTypesDal, RoomsDal, RoomTypesDal, GuestsDal>(sql,
					(guest, paymentType, room, roomType) =>
					{
						guest.Room = room;
						guest.PaymentType = paymentType;
						guest.Room.RoomType = roomType;
						return guest;
					}).ToList();
			}

			return guests;
		}

		private GuestsDal GetGuest(string sql)
		{
			GuestsDal guestDal = new GuestsDal();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				guestDal = connection.Query<GuestsDal, PaymentTypesDal, RoomsDal, RoomTypesDal, GuestsDal>(sql,
					(guest, paymentType, room, roomType) =>
					{
						guest.Room = room;
						guest.PaymentType = paymentType;
						guest.Room.RoomType = roomType;
						return guest;
					}).ToList().FirstOrDefault();
			}

			return guestDal;
		}

		public bool Create(GuestsDal item)
		{
			var sql = @"INSERT dbo.Guests([Name],[Surname],[FathersName],[Organization],[Nationality],[PaymentTypeId],[RoomId],[DateFrom],[DateTo]) 
						values (@name, @surname, @fatherName, @organization, @nationality, @paymentTypeId, @roomId, @dateFrom, @dateTo);";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rowsAffected = connection.Execute(sql, new
				{
					name = item.Name,
					surname = item.SurName,
					fatherName = item.FatherName,
					organization = item.Organization,
					nationality = item.Nationality,
					paymentTypeId = item.PaymentType.Id,
					roomId = item.Room.Id,
					dateFrom = item.DateFrom,
					dateTo = item.DateTo
				});

				return rowsAffected > 0;
			}
		}

		public bool Delete(int id)
		{
			var sql = @"DELETE FROM dbo.Guests WHERE Id = @identifier;";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var rowsAffected = connection.Execute(sql, new
				{
					identifier = id
				});

				return rowsAffected > 0;
			}
		}

		public bool Update(GuestsDal item)
		{
			var sql = @"UPDATE dbo.Guests SET 
							[Name] = @name, 
							[Surname] = @surname, 
							[FathersName] = @fatherName, 
							[Organization] = @organization,
							[Nationality] = @nationality,
							[PaymentTypeId] = @paymentTypeId,
							[RoomId] = @roomId,
							[DateFrom] = @dateFrom,
							[DateTo] = @dateTo
						WHERE Id = @id";

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				var affectedRows = connection.Execute(sql, new
				{
					name = item.Name,
					surname = item.SurName,
					fatherName = item.FatherName,
					organization = item.Organization,
					nationality = item.Nationality,
					paymentTypeId = item.PaymentType.Id,
					roomId = item.Room.Id,
					dateFrom = item.DateFrom,
					dateTo = item.DateTo,
					id = item.Id
				});

				return affectedRows > 0;
			}
		}
	}
}
