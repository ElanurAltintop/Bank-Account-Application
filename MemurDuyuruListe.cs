using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace BankaHesapUygulaması
{
    public partial class MemurDuyuruListe : Form
    {
        public MemurDuyuruListe()
        {
            InitializeComponent();
        }
       readonly SQLBağlanma bgln = new SQLBağlanma();
        private void MemurDuyuruListe_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from TblDuyuru ", bgln.Baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LstBxDuyuru.Items.Add(dr[0] + " " + dr[1]);
            }
            bgln.Baglanti().Close();
        }
    }
}
