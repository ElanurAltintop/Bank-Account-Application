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
    public partial class Mesajlar : Form
    {
        void GelenMesajlar()                                        // müsteri kısmındada kullanılacak metottur.
        {                                                          // Gelen mesajlar tutulacaktır.
            SqlCommand komut = new SqlCommand("select * from TblMesajlar where AlıcıNo=@par1", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", MskGonderenNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void GidenMesajlar()
        {                                                         // Giden mesajlar tutulacaktır.
            SqlCommand komut = new SqlCommand("select * from TblMesajlar where GönderenNo=@par1", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", MskAlıcıNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        readonly SQLBağlanma bgln = new SQLBağlanma();

        public Mesajlar()
        {
            InitializeComponent();
        }
        public string numara;
        private void Mesajlar_Load(object sender, EventArgs e)
        {
            MskGonderenNo.Text = numara;    //FrmBankaMemur formunda mesajlara tıkladığımızda oradaki numarayı buraya aktardık.
            GelenMesajlar();
            GidenMesajlar();
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblMesajlar (GönderenNo,AlıcıNo,KonuBaslık,İçerik)values (@par1,@par2,@par3,@par4)", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", MskGonderenNo.Text);
            komut.Parameters.AddWithValue("@par2", MskAlıcıNo.Text);
            komut.Parameters.AddWithValue("@par3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@par4", RchTxtBxIcerik.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Mesajınız İletildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgln.Baglanti().Close();
            GelenMesajlar();
            GidenMesajlar();

        }
    }
}
