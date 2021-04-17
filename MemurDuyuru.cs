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
    public partial class MemurDuyuru : Form
    {
        public MemurDuyuru()
        {
            InitializeComponent();
        }
       readonly SQLBağlanma bgln = new SQLBağlanma();

        void  Listeleme()
        {
            SqlCommand komut = new SqlCommand("select * from TblDuyuru", bgln.Baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void MemurDuyuru_Load(object sender, EventArgs e)
        {
            Listeleme();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblDuyuru (İçerik) values (@par1)", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", RichTxtBxDuyuru.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Duyurunuz oluşturuldu", "Duyuru", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bgln.Baglanti().Close();
            Listeleme();

        }
        string id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secim].Cells[0].Value.ToString();         //Güncelleme işlemini id'e göre yapacağız.
            RichTxtBxDuyuru.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TblDuyuru where ID=@par1", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", id);
            komut.ExecuteNonQuery();
            bgln.Baglanti().Close();
            Listeleme();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {                                                               // global tanımladığımız id alanını kullandık güncellemede
            SqlCommand komut = new SqlCommand("update TblDuyuru set içerik=@par1 where ID=@par2", bgln.Baglanti());
            komut.Parameters.AddWithValue("@par1", RichTxtBxDuyuru.Text);
            komut.Parameters.AddWithValue("@par2", id);
            komut.ExecuteNonQuery();
            MessageBox.Show("Duyuru Güncellenmiştir", "Duyur Güncelleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bgln.Baglanti().Close();
            Listeleme();
        }
    }
}
