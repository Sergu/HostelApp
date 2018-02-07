using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Impl;
using DAL.Repositories.Interface;

namespace HostelApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["HostelDbConnection"].ConnectionString;
			IRoomsRepository roomRepository = new RoomsRepository(connectionString);
			IGuestsRepository guestsRepository = new GuestsRepository(connectionString);
			var id = 2;
			var res = roomRepository.GetById(id);
			var roomsCollection = roomRepository.GetAll();
			var romById = roomRepository.GetById(3);
			var guests = guestsRepository.GetAll();

			var activeGuests = guestsRepository.GetGuestsFromTimePeriod(DateTime.Now, DateTime.Now);
			var guestById = guestsRepository.GetById(2);

			Console.WriteLine($"RoomNumber: {res.RoomNumber} {res.RoomLetter ?? String.Empty}");

			Console.ReadLine();
		}
	}
}
