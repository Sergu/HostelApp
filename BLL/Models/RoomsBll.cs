
namespace BLL.Models
{
	public class RoomsBll
	{
		public int Id { get; set; }
		public int RoomNumber { get; set; }
		public RoomTypesBll RoomType { get; set; }
		public string RoomLetter { get; set; }
		public int Floor { get; set; }
	}
}
