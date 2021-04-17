using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankaHesapUygulaması
{
    class SQLBağlanma
    {
        public SqlConnection Baglanti()
        {
             SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-OPF4R1QF\SQLEXPRESS;Initial Catalog=BankaHesabıKayıtDB;Integrated Security=True");
             baglan.Open();
             return baglan;
        }
    }
}

