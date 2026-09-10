# LATIHAN PERTEMUAN 2

Membuat aplikasi console sederhana berbasis C# untuk mengelola data mahasiswa

## Fitur yang ada:

- Tambah Mahasiswa: Menambahkan data mahasiswa baru (NIM, Nama, Program Studi, IPK) dengan validasi input IPK (0.0 - 4.0).
- Tampilkan Mahasiswa: Menampilkan seluruh daftar mahasiswa dalam bentuk tabel yang tersusun rapi.
- Cari Mahasiswa: Mencari data spesifik mahasiswa berdasarkan NIM (*case-insensitive*).
- Hapus Mahasiswa: Menghapus data mahasiswa dari sistem berdasarkan NIM.

## Penjelasan Kode 

### 1. `Class Mahasiswa`
Method khusus yang dipanggil saat pembuatan objek baru dari class `Mahasiswa`. Bertugas menerima 4 nilai parameter (`nim`, `nama`, `prodi`, `ipk`) dan memasukkannya langsung ke dalam masing-masing properti class.

### 2. `Main(string[] args)`
Fungsi utama (*entry point*) yang pertama kali dijalankan oleh sistem. Mengatur alur program menggunakan perulangan `do-while` agar menu terus tampil sampai user memilih angka `5` (Keluar). Menggunakan `int.TryParse()` untuk membaca input angka secara aman dan `switch-case` untuk mengarahkan eksekusi ke fungsi yang sesuai.

### 3. `TampilkanMenu()`
Membersihkan layar terminal (`Console.Clear()`) dan mencetak tampilan antarmuka (UI) menu utama berisi daftar opsi 1–5 ke layar.

### 4. `TambahMahasiswa()`
Meminta masukan NIM, Nama, dan Program Studi dari user. Memiliki perulangan `while(true)` khusus untuk validasi IPK agar user wajib memasukkan angka di rentang **0 hingga 4**. Setelah data valid, fungsi membuat objek `Mahasiswa` baru dan menyimpannya ke dalam `daftarMahasiswa`.

### 5. `TampilkanMahasiswa()`
Mengecek apakah `daftarMahasiswa` masih kosong. Jika berisi data, fungsi ini mencetak seluruh daftar mahasiswa ke konsol menggunakan perulangan `foreach` dengan format tabel yang rata dan rapi (`{0,-12}`, `{1,-20}`, dsb.).

### 6. `CariMahasiswa()`
Menerima input NIM yang ingin dicari, lalu melakukan pencarian linear menyusuri `daftarMahasiswa`. Pencarian bersifat *case-insensitive* (`OrdinalIgnoreCase`). Jika NIM cocok, detail data mahasiswa tersebut akan dicetak ke layar.

### 7. `HapusMahasiswa()`
Mencari objek mahasiswa berdasarkan NIM yang diinputkan user. Jika cocok, objek tersebut langsung dihapus dari daftar menggunakan method bawaan `daftarMahasiswa.Remove(mahasiswaDitemukan)`.
   ```bash
   dotnet new console
