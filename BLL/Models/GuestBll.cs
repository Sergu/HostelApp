using System;

namespace BLL.Models
{
	public class GuestBll
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SurName { get; set; }
		public string FatherName { get; set; }
		public string Nationality { get; set; }
		public PaymentTypesBll PaymentType { get; set; }
		public string Organization { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public RoomsBll Room { get; set; }
	}
}
