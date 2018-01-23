using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class RoomsDal : IDalEntity
	{
		public int Id { get; set; }
		public string RoomNumber { get; set; }
		public RoomTypesDal RoomType { get; set; }
		public string RoomLetter { get; set; }
	}
}
