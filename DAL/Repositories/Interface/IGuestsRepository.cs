using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories.Interface
{
	public interface IGuestsRepository : ICrudBaseRepository<GuestsDal>
	{
		IList<GuestsDal> GetAll();
		IList<GuestsDal> GetGuestsFromTimePeriod(DateTime dateFrom, DateTime dateTo);
		IList<GuestsDal> GetGuestsFromRoomWithTimePeriod(int roomId, DateTime dateFrom, DateTime dateTo);
	}
}
