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
    public partial class FrmMüsteri : Form
    {
        public FrmMüsteri()
        {
            InitializeComponent();
        }
        public string numara;
    
        readonly SQLBağlanma bgln = new SQLBağlanma();
        private void FrmMüsteri_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            SqlCommand komut = new SqlCommand("select * from TblMusteri where MüşteriNo=@par1", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", LblNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
            }
            bgln.Baglanti().Close();

            SqlCommand komut2 = new SqlCommand("select * from TblHesapBilgi where ID=" +
            "(Select MüşteriID from TblMusteri where MüşteriNo=@par1)", bgln.Baglanti());
            komut2.Parameters.AddWithValue("@par1", numara);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
               LblParaMiktar.Text = dr2[1].ToString();
               LblBorcMiktar.Text = dr2[2].ToString();
               LblGun.Text = dr2[3].ToString();
               LblFaiz.Text = dr2[4].ToString();
               LblEmeklilik.Text = dr2[5].ToString();
            }
            bgln.Baglanti().Close();
        }
        
        private void BtnMusteriMesaj_Click(object sender, EventArgs e)
        {
            Mesajlar yeni = new Mesajlar();
            yeni.numara = LblNumara.Text;
            yeni.Show();
        }

        private void BtnMusteriDuyuyru_Click(object sender, EventArgs e)
        {
            MemurDuyuruListe yeni = new MemurDuyuruListe();
            yeni.Show();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Kapatmak İstiyor Musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dr==DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
