using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class GuestsDal : IDalEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SurName { get; set; }
		public string FatherName { get; set; }
		public string Nationality { get; set; }
		public PaymentTypesDal PaymentType { get; set; }
		public string Organization { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public RoomsDal Room { get; set; }
	}
}
