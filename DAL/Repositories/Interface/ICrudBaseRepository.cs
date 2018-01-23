using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories.Interface
{
	public interface ICrudBaseRepository<T> where T: IDalEntity
	{
		T GetById(int id);
		//void Create(T item);
		//bool Delete(int id);
		//bool Update(T item);
	}
}
