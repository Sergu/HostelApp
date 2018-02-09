using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Models;

namespace BLL.Mappers
{
	public static class Mapper
	{
		public static GuestsDal ToDal(this GuestBll guestsBll)
		{
			return new GuestsDal()
			{
				Id = guestsBll.Id,
				Name = guestsBll.Name,
				SurName = guestsBll.SurName,
				FatherName = guestsBll.FatherName,
				DateFrom = guestsBll.DateFrom,
				DateTo = guestsBll.DateTo,
				Organization = guestsBll.Organization,
				Nationality = guestsBll.Nationality,
				Room = guestsBll.Room.ToDal()
			};
		}

		public static GuestBll ToBll(this GuestsDal guestsDal)
		{
			return new GuestBll()
			{
				Id = guestsDal.Id,
				Name = guestsDal.Name,
				SurName = guestsDal.SurName,
				FatherName = guestsDal.FatherName,
				DateFrom = guestsDal.DateFrom,
				DateTo = guestsDal.DateTo,
				Organization = guestsDal.Organization,
				Nationality = guestsDal.Nationality,
				Room = guestsDal.Room.ToBll()
			};
		}

		public static RoomsDal ToDal(this RoomsBll roomsBll)
		{
			return new RoomsDal()
			{
				Id = roomsBll.Id,
				RoomNumber = roomsBll.RoomNumber,
				RoomLetter = roomsBll.RoomLetter,
				Floor = roomsBll.Floor,
				RoomType = roomsBll.RoomType.ToDal()
			};
		}

		public static RoomsBll ToBll(this RoomsDal roomsDal)
		{
			return new RoomsBll()
			{
				Id = roomsDal.Id,
				RoomNumber = roomsDal.RoomNumber,
				RoomLetter = roomsDal.RoomLetter,
				Floor = roomsDal.Floor,
				RoomType = roomsDal.RoomType.ToBll()
			};
		}

		public static RoomTypesDal ToDal(this RoomTypesBll roomTypesBll)
		{
			return new RoomTypesDal()
			{
				Id = roomTypesBll.Id,
				ShortName = roomTypesBll.ShortName,
				Type = roomTypesBll.Type
			};
		}

		public static RoomTypesBll ToBll(this RoomTypesDal roomTypesDal)
		{
			return new RoomTypesBll()
			{
				Id = roomTypesDal.Id,
				ShortName = roomTypesDal.ShortName,
				Type = roomTypesDal.Type
			};
		}

		public static PaymentTypesDal ToDal(this PaymentTypesBll paymentTypesBll)
		{
			return new PaymentTypesDal()
			{
				Id = paymentTypesBll.Id,
				Type = paymentTypesBll.Type
			};
		}

		public static PaymentTypesBll ToBll(this PaymentTypesDal paymentTypesDal)
		{
			return new PaymentTypesBll()
			{
				Id = paymentTypesDal.Id,
				Type = paymentTypesDal.Type
			};
		}
	}
}
