using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DA_QLKS
{
    public partial class ThanhToan : Form
    {
        public ThanhToan()
        {
            InitializeComponent();
        }

        string sql;
        string ChuoiKetNoi = @"Data Source=DESKTOP-46S7SCM\MINHPHAT;Initial Catalog=DA_QLKS;Persist Security Info=True;User ID=sa;Password=1234";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataReader DocDuLieu;
        int i = 0;

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.PHONG' table. You can move, or remove it, as needed.
            this.pHONGTableAdapter.Fill(this.dataSet1.PHONG);
            KetNoi = new SqlConnection(ChuoiKetNoi);
            HienThi();
        }

        //hien thi thong tin KH
        public void HienThi()   
        {
            listView1.Items.Clear();
            KetNoi.Open();
            sql = @"Select makh, tenkh, socmnd, ngayden, maphong From KHACHHANG";
            ThucHien = new SqlCommand(sql, KetNoi);
            DocDuLieu = ThucHien.ExecuteReader();
            i = 0;
            while (DocDuLieu.Read())
            {
                listView1.Items.Add(DocDuLieu[0].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[1].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[2].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[3].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[4].ToString());
                i++;
            }
            KetNoi.Close();
        }

        //tim khach hang theo ma phong
        private void btnTimPhong_Click(object sender, EventArgs e) 
        {
            listView1.Items.Clear();
            KetNoi.Open();
            sql = "Select makh, tenkh, socmnd, ngayden, maphong From KHACHHANG Where(maphong like '%" + cbMaPhong.Text +"%')";
            ThucHien = new SqlCommand(sql, KetNoi);
            DocDuLieu = ThucHien.ExecuteReader();
            i = 0;
            while (DocDuLieu.Read())
            {
                listView1.Items.Add(DocDuLieu[0].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[1].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[2].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[3].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[4].ToString());
                i++;
            }
            KetNoi.Close();
        }

        //tim khach hang theo CMND
        private void btnTimCmnd_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            KetNoi.Open();
            sql = "Select makh, tenkh, socmnd, ngayden, maphong From KHACHHANG Where(socmnd like '%" + txtCmnd.Text + "%')";
            ThucHien = new SqlCommand(sql, KetNoi);
            DocDuLieu = ThucHien.ExecuteReader();
            i = 0;
            while (DocDuLieu.Read())
            {
                listView1.Items.Add(DocDuLieu[0].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[1].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[2].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[3].ToString());
                listView1.Items[i].SubItems.Add(DocDuLieu[4].ToString());
                i++;
            }
            KetNoi.Close();
        }

        //tinh tien phong
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            KetNoi.Open();
            sql = @"Select PHONG.maphong, KHACHHANG.tenkh, Datediff(day, KHACHHANG.ngayden, GETDATE()), Datediff(day, KHACHHANG.ngayden, GETDATE()) * PHONG.dongia 
                    From PHONG Inner Join KHACHHANG
                    On PHONG.maphong = KHACHHANG.maphong
                    Where (KHACHHANG.maphong = N'" + cbMaPhong.Text + @"') 
                    And   (KHACHHANG.tenkh = N'" + txtHoTen.Text + @"')";
            ThucHien = new SqlCommand(sql, KetNoi);
            DocDuLieu = ThucHien.ExecuteReader();
            i = 0;
            while (DocDuLieu.Read())
            {
                txtTenPhong.Text = DocDuLieu[0].ToString();
                txtSoNgayO.Text = DocDuLieu[2].ToString();
                txtTongTienTra.Text = DocDuLieu[3].ToString();
                i++;
            }
            KetNoi.Close();
        }

        //hien thi thong tin KH ra listview
        private void listView1_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = listView1.SelectedItems[0].SubItems[4].Text;
            txtHoTen.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtCmnd.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtNgayDen.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtMaKH.Text = listView1.SelectedItems[0].SubItems[0].Text;
        }

        //thoat chuong trinh
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        //KH dat phong
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            KetNoi.Open();
            sql = @"Insert Into KHACHHANG(makh, tenkh, socmnd, ngayden, maphong)
                    Values(N'" +txtMaKH.Text + @"', N'"+txtHoTen.Text+@"', N'"+txtCmnd.Text+@"', N'"+txtNgayDen.Text+@"', N'"+cbMaPhong.Text+@"')";
            ThucHien = new SqlCommand(sql, KetNoi);
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThi();
        }

        //KH tra phong
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            KetNoi.Open();
            sql = @"Delete From KHACHHANG Where(makh = N'"+txtMaKH.Text+@"')";
            ThucHien = new SqlCommand(sql, KetNoi);
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThi();
        }
    }
}
