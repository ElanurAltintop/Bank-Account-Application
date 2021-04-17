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
    public partial class FrmGiriş : Form
    {
        public FrmGiriş()
        {
            InitializeComponent();
        }
        readonly SQLBağlanma bgln = new SQLBağlanma();
        
        private void BtnMemurGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TblBankaMemur where MemurNo=@parametre1 and MemurŞifre=@parametre2", bgln.Baglanti());
            komut.Parameters.AddWithValue("@parametre1", MskMemurNo.Text);
            komut.Parameters.AddWithValue("@parametre2", TxtBxMemurSifre.Text);
            SqlDataReader verioku = komut.ExecuteReader();
            if (verioku.Read())
            {
                FrmBankaMemur yeni = new FrmBankaMemur();
                yeni.numara = MskMemurNo.Text;  //Bu blok Banka Memuru Formuna veri taşımak amacıyla yazıldı.
                yeni.Show();
                
            }
            else 
            {
                MessageBox.Show("Girdiğiniz bilgileri lütfen kontrol ediniz.", "Hatalı Giriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            
            bgln.Baglanti().Close();
        }

        private void BtnMusteriGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from TblMusteri where MüşteriNo=@parametre1 and MüşteriŞifre=@parametre2", bgln.Baglanti());
            komut.Parameters.AddWithValue("@parametre1", MskMusteriNO.Text);
            komut.Parameters.AddWithValue("@parametre2", TxtBxMüsteriSifre.Text);
            SqlDataReader verioku = komut.ExecuteReader();
            if(verioku.Read())
            {
                FrmMüsteri yeni = new FrmMüsteri();
                yeni.numara = MskMusteriNO.Text;
                yeni.Show();
                
            }
            else
            {
                MessageBox.Show("Girdiğiniz bilgileri lütfen kontrol ediniz.", "Hatalı Giriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            bgln.Baglanti().Close();
        }
    }
}
