# LAPORAN STUDENT ATTENDANCE APPLICATION

![image](https://github.com/notdoppi/PBKK-C/blob/3d3bf7626599fae420d036156bf6ee26d6f5f5c2/pertemuan4/WhatsApp%20Image%202026-10-19%20at%2017.05.01.jpeg)

### 1. Pendahuluan

Aplikasi ini adalah **Student Attendance App** (Aplikasi Presensi Mahasiswa) berbasis desktop yang dibangun menggunakan **WPF (Windows Presentation Foundation)** dan bahasa pemrograman **C#**.

Aplikasi ini berfungsi untuk mencatatkan dan merekap data kehadiran mahasiswa secara interaktif. Pengguna dapat memasukkan data NIM, Nama, Mata Kuliah, Tanggal, serta Status Kehadiran (Hadir, Izin, Sakit, Alpa), lalu menyimpannya ke dalam daftar rekap. Selain itu, aplikasi dilengkapi fungsi validasi input agar tidak ada data kosong yang tersimpan, serta fitur untuk menghapus catatan presensi dan membersihkan form.

### 2. Spesifikasi dan Kontrol UI

#### Control UI yang Digunakan:

* **`TextBox`**: Mengambil input NIM dan Nama Mahasiswa.

* **`ComboBox`**: Memilih Mata Kuliah dari daftar drop-down.

* **`DatePicker`**: Memilih tanggal presensi menggunakan kalender.

* **`RadioButton`**: Memilih status kehadiran (Hadir, Izin, Sakit, Alpa).

* **`ListBox`**: Menampilkan daftar rekapitulasi data presensi.

* **`Button`**: Memicu aksi simpan, reset, dan hapus data.

#### Event Handling:

* **`BtnSimpan_Click`**: Menjalankan validasi input, memformat ringkasan presensi, dan memasukkannya ke `ListBox`.

* **`BtnReset_Click`**: Mengosongkan seluruh form input.

* **`BtnHapus_Click`**: Menghapus baris presensi yang dipilih dari `ListBox`.

### 3. Implementasi

#### 3.1 Layout & Component XAML (`MainWindow.xaml`)

Berikut adalah struktur komponen form dan layout utama pada XAML:

```
<!-- Form Input Presensi (Kolom Kiri) -->
<StackPanel Grid.Column="0">
    <TextBlock Text="INPUT PRESENSI" FontSize="20" FontWeight="Bold" Margin="0,0,0,15"/>

    <TextBlock Text="NIM"/>
    <TextBox x:Name="txtNim" Height="30" Margin="0,5,0,10"/>

    <TextBlock Text="Mata Kuliah"/>
    <ComboBox x:Name="cmbMatkul" Height="30" Margin="0,5,0,10">
        <ComboBoxItem Content="Pemrograman .NET"/>
        <ComboBoxItem Content="Jaringan Komputer"/>
    </ComboBox>

    <TextBlock Text="Tanggal Presensi"/>
    <DatePicker x:Name="dpTanggal" Height="30" Margin="0,5,0,10"/>

    <TextBlock Text="Status Kehadiran"/>
    <StackPanel Orientation="Horizontal" Margin="0,5,0,15">
        <RadioButton x:Name="rbHadir" Content="Hadir" IsChecked="True"/>
        <RadioButton x:Name="rbIzin" Content="Izin"/>
        <RadioButton x:Name="rbSakit" Content="Sakit"/>
        <RadioButton x:Name="rbAlpa" Content="Alpa"/>
    </StackPanel>

    <Button Content="Simpan" Click="BtnSimpan_Click"/>
</StackPanel>

<!-- List Rekap Presensi (Kolom Kanan) -->
<StackPanel Grid.Column="2">
    <ListBox x:Name="lstPresensi" Height="400"/>
    <Button Content="Hapus Presensi Terpilih" Click="BtnHapus_Click"/>
</StackPanel>

```

#### 3.2 Logika & Validasi C# (`MainWindow.xaml.cs`)

**a. Validasi & Event Simpan Data (`BtnSimpan_Click`):**

```
private void BtnSimpan_Click(object sender, RoutedEventArgs e)
{
    // Validasi input wajib terisi
    if (string.IsNullOrWhiteSpace(txtNim.Text) || string.IsNullOrWhiteSpace(txtNama.Text))
    {
        MessageBox.Show("NIM dan Nama tidak boleh kosong!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    if (cmbMatkul.SelectedItem == null || dpTanggal.SelectedDate == null)
    {
        MessageBox.Show("Pilih mata kuliah dan tanggal presensi!", "Validasi Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    // Ambil data dari form
    string matkul = (cmbMatkul.SelectedItem as ComboBoxItem)?.Content.ToString();
    string tanggal = dpTanggal.SelectedDate.Value.ToString("dd/MM/yyyy");
    string status = rbIzin.IsChecked == true ? "Izin" : (rbSakit.IsChecked == true ? "Sakit" : (rbAlpa.IsChecked == true ? "Alpa" : "Hadir"));

    // Tambahkan ke ListBox
    lstPresensi.Items.Add($"[{tanggal}] {txtNim.Text} - {txtNama.Text} | {matkul} | Status: {status}");
    MessageBox.Show("Data presensi berhasil dicatat!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

    ResetForm();
}

```

**b. Event Hapus Data Terpilih (`BtnHapus_Click`):**

```
private void BtnHapus_Click(object sender, RoutedEventArgs e)
{
    if (lstPresensi.SelectedItem != null)
    {
        lstPresensi.Items.Remove(lstPresensi.SelectedItem);
        MessageBox.Show("Data presensi berhasil dihapus.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    else
    {
        MessageBox.Show("Pilih data presensi yang ingin dihapus!", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}

```
