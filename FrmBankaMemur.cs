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
    public partial class FrmBankaMemur : Form
    {
        public FrmBankaMemur()
        {
            InitializeComponent();
        }
        public string numara;                        
                                                            //Veri taşıma işlemi yapabilmek için public ve global olarak değişken tanımlıyoruz.
        readonly SQLBağlanma bgln = new SQLBağlanma();
        void musteriListe()                                 //Metot Müsterileri datagridview nesnesine aktarmak içindir.
        {
            try
            {
                SqlCommand komut = new SqlCommand("select * from TblMusteri", bgln.Baglanti());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception hatamesaji)
            {
                MessageBox.Show(hatamesaji.Message);
            }
        }
    
        void hesapBilgileriListe()                         //Metot hesapbilgilerini datagridview nesnesine aktarmak içindir.
        {
            SqlCommand komut = new SqlCommand("execute Musteri", bgln.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void FrmBankaMemur_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;                                    //Taşınan numaraya göre ad ve soyad verilerini taşıdık.
            SqlCommand komut = new SqlCommand("select * from TblBankaMemur where MemurNo=@parametre1", bgln.Baglanti());
            komut.Parameters.AddWithValue("@parametre1", numara);
            SqlDataReader verioku = komut.ExecuteReader();
            while (verioku.Read())
            {
                LblAd.Text = verioku[1] + " " + verioku[2];
            }
            bgln.Baglanti().Close();

            musteriListe();
            hesapBilgileriListe();
        }

        string fotograf;
        private void BtnFotograf_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fotograf = openFileDialog1.FileName;
            pictureBox1.ImageLocation = fotograf;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
 
            SqlCommand komut = new SqlCommand("insert into TblMusteri (MüşteriAd, MüşteriSoyad, MüşteriNo, MüşteriŞifre)" +
               "values (@par1,@par2,@par3,@par4)", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", TxtAdı.Text);
            komut.Parameters.AddWithValue("@par2", TxtSoyadı.Text);
            komut.Parameters.AddWithValue("@par3", MskNumara.Text);
            komut.Parameters.AddWithValue("@par4", MskSifre.Text);

            komut.ExecuteNonQuery();
            MessageBox.Show("Müşteri Kayıt İşleminiz gerçekleştirilmiştir.", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgln.Baglanti().Close();
            musteriListe();
            hesapBilgileriListe();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;               //Bir hücreye tıklandığında hücredeki verileri araçlara aktarma işlemi
            TxtAdı.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            TxtSoyadı.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            MskNumara.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            MskSifre.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secim].Cells[5].Value.ToString();

           SqlCommand komut = new SqlCommand("select * from TblHesapBilgi where ID=" +  //Numaraya göre hesap bilgilerini araçlara aktarması işlemi
                "(select MüşteriID from TblMusteri where MüşteriNo=@par1)", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", MskNumara.Text);
            SqlDataReader verioku = komut.ExecuteReader();
            while (verioku.Read())
            {
                TxtBxParaMiktar.Text = verioku[1].ToString();
                TxtBxBorçMiktar.Text = verioku[2].ToString();
                TxtBxGün.Text = verioku[3].ToString();
                TxtFaiz.Text = verioku[4].ToString();
                TxtEmeklilik.Text = verioku[5].ToString();
            }
            bgln.Baglanti().Close(); 
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
 
            SqlCommand komut = new SqlCommand("update TblMusteri set MüşteriAd=@par1, MüşteriSoyad=@par2, MüşteriŞifre=@par3 " +
                " where MüşteriNo=@par4", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", TxtAdı.Text);
            komut.Parameters.AddWithValue("@par2",TxtSoyadı.Text);
            komut.Parameters.AddWithValue("@par3", MskSifre.Text);
            komut.Parameters.AddWithValue("@par4", MskNumara.Text);
            bgln.Baglanti().Close();
         
            //Hesap Bilgilerini güncelleme işlemi
            SqlCommand komut2 = new SqlCommand(" update TblHesapBilgi set HspParaMiktarı=@par1, BorçMiktar=@par2, ÖdenmeyenGün=@par3, " +
                "faiz=@par4, EmeklilikHesap=@par5 where ID = (Select TOP 1 MüşteriID from TblMusteri where MüşteriNo=@par6)", bgln.Baglanti());
            komut2.Parameters.AddWithValue("@par1", TxtBxParaMiktar.Text);
            komut2.Parameters.AddWithValue("@par2", TxtBxBorçMiktar.Text);
            komut2.Parameters.AddWithValue("@par3", TxtBxGün.Text);
            komut2.Parameters.AddWithValue("@par4", TxtFaiz.Text);
            komut2.Parameters.AddWithValue("@Par5", TxtEmeklilik.Text);
            komut2.Parameters.AddWithValue("@par6", MskNumara.Text);
            komut2.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işlemi gerçekleşmiştir.", "Güncelleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bgln.Baglanti().Close();
            hesapBilgileriListe();
            musteriListe();
        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                double paramiktar, borçmiktar;
                int gün, faiz;
                borçmiktar = Convert.ToDouble(TxtBxBorçMiktar.Text);
                paramiktar = Convert.ToDouble(TxtBxParaMiktar.Text);
                gün = Convert.ToInt32(TxtBxGün.Text);
                faiz = Convert.ToInt32(TxtFaiz.Text);
                if(paramiktar!=0 && borçmiktar==0)
                {
                    TxtEmeklilik.Text = "true";
                }
                else
                {
                    TxtEmeklilik.Text = "false";
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Girilen değerleri kontrol ediniz");
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {  
          int selectedIndex = dataGridView1.CurrentCell.RowIndex; //Silme işlemini denedik ve sildik fakat ID numaralarıda aynı şekilde gitti.
            if (selectedIndex > -1)
            {
                dataGridView1.Rows.RemoveAt(selectedIndex);
                dataGridView1.Refresh();
            }
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            MemurDuyuru yeni = new MemurDuyuru();
            yeni.Show();
        }

        private void BtnDuyuruListe_Click(object sender, EventArgs e)
        {
            MemurDuyuruListe yeni = new MemurDuyuruListe();
            yeni.Show();
        }
     
        private void BtnMesaj_Click(object sender, EventArgs e)
        {
            Mesajlar yeni = new Mesajlar();
            yeni.numara = LblNumara.Text;
            yeni.Show();
        }
    }
}
