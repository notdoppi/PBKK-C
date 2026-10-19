using System;
using System.Windows;
using System.Windows.Controls;

namespace StudentAttendanceApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // EVENT 1: SIMPAN DATA PRESENSI
        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            // 1. Validasi Input
            if (string.IsNullOrWhiteSpace(txtNim.Text))
            {
                MessageBox.Show("NIM tidak boleh kosong!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNim.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama mahasiswa tidak boleh kosong!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNama.Focus();
                return;
            }

            if (cmbMatkul.SelectedItem == null)
            {
                MessageBox.Show("Pilih mata kuliah terlebih dahulu!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dpTanggal.SelectedDate == null)
            {
                MessageBox.Show("Pilih tanggal presensi!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. Ambil Nilai Input
            string nim = txtNim.Text;
            string nama = txtNama.Text;
            string matkul = (cmbMatkul.SelectedItem as ComboBoxItem)?.Content.ToString();
            string tanggal = dpTanggal.SelectedDate.Value.ToString("dd/MM/yyyy");

            string status = "Hadir";
            if (rbIzin.IsChecked == true) status = "Izin";
            else if (rbSakit.IsChecked == true) status = "Sakit";
            else if (rbAlpa.IsChecked == true) status = "Alpa";

            // 3. Tambahkan ke ListBox
            string itemData = $"[{tanggal}] {nim} - {nama} | {matkul} | Status: {status}";
            lstPresensi.Items.Add(itemData);

            MessageBox.Show("Data presensi berhasil dicatat!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

            // 4. Reset Form
            ResetForm();
        }

        // EVENT 2: RESET FORM INPUT
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        // EVENT 3: HAPUS PRESENSI TERPILIH
        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (lstPresensi.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus catatan presensi ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    lstPresensi.Items.Remove(lstPresensi.SelectedItem);
                    MessageBox.Show("Data presensi berhasil dihapus.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Pilih data presensi yang ingin dihapus pada daftar sebelah kanan!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // HELPER METHOD RESET
        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            cmbMatkul.SelectedIndex = -1;
            dpTanggal.SelectedDate = null;
            rbHadir.IsChecked = true;
            txtNim.Focus();
        }
    }
}