# LATIHAN PERTEMUAN 2

Membuat aplikasi console sederhana berbasis C# untuk mengelola data mahasiswa

## Fitur yang ada:

- Tambah Mahasiswa: Menambahkan data mahasiswa baru (NIM, Nama, Program Studi, IPK) dengan validasi input IPK (0.0 - 4.0).
- Tampilkan Mahasiswa: Menampilkan seluruh daftar mahasiswa dalam bentuk tabel yang tersusun rapi.
- Cari Mahasiswa: Mencari data spesifik mahasiswa berdasarkan NIM (*case-insensitive*).
- Hapus Mahasiswa: Menghapus data mahasiswa dari sistem berdasarkan NIM.

## Penjelasan Kode 

### 1. `class Mahasiswa`
(isi bagian ini)

### 2. `Main(string[] args)`
(isi bagian ini

### 3. `TampilkanMenu()`
Membersihkan layar terminal (`Console.Clear()`) dan mencetak tampilan antarmuka (UI) menu utama berisi daftar opsi 1–5 ke layar

### 4. `TambahMahasiswa()`
Meminta masukan NIM, Nama, dan Program Studi dari user. Memiliki perulangan `while(true)` khusus untuk validasi IPK agar user wajib memasukkan angka di rentang **0 hingga 4**. Setelah data valid, fungsi membuat objek `Mahasiswa` baru dan menyimpannya ke dalam `daftarMahasiswa`

### 5. `TampilkanMahasiswa()`
Mengecek apakah `daftarMahasiswa` masih kosong. Jika berisi data, fungsi ini mencetak seluruh daftar mahasiswa ke konsol menggunakan perulangan `foreach`

### 6. `CariMahasiswa()`
Menerima input NIM yang ingin dicari, lalu melakukan pencarian pada `daftarMahasiswa`. Jika NIM cocok, data mahasiswa tersebut akan dicetak ke layar.

### 7. `HapusMahasiswa()`
Menerima input NIM yang ingin dicari. Jika cocok, objek tersebut langsung dihapus dari daftar menggunakan method bawaan `daftarMahasiswa.Remove(mahasiswaDitemukan)`.
