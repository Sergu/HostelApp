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
	public class PaymentTypeRepository : IPaymentTypeRepository
	{
		private readonly string _connectionString;

		public PaymentTypeRepository(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("connectionString is Null or Whitespace");
			}

			_connectionString = connectionString;
		}

		public IList<PaymentTypesDal> GetAll()
		{
			var sql = @"SELECT * FROM dbo.PaymentTypes;";

			IList<PaymentTypesDal> paymentTypes = new List<PaymentTypesDal>();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				paymentTypes = connection.Query<PaymentTypesDal>(sql).ToList();
			}

			return paymentTypes;
		}

		public PaymentTypesDal GetById(int id)
		{
			var sql = @"SELECT * FROM dbo.PaymentTypes WHERE Id = @identifier";

			PaymentTypesDal paymentType = new PaymentTypesDal();

			using (IDbConnection connection = new SqlConnection(_connectionString))
			{
				paymentType = connection.Query<PaymentTypesDal>(sql, new
				{
					identifier = id
				}).ToList().FirstOrDefault();
			}

			return paymentType;
		}

		public bool Create(PaymentTypesDal item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(PaymentTypesDal item)
		{
			throw new NotImplementedException();
		}
	}
}
