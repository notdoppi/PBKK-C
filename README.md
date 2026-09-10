# LATIHAN PERTEMUAN 2

![menu]()

Membuat aplikasi console sederhana berbasis C# untuk mengelola data mahasiswa

## Fitur yang ada:

- Tambah Mahasiswa: Menambahkan data mahasiswa baru (NIM, Nama, Program Studi, IPK) dengan validasi input IPK (0.0 - 4.0)
- Tampilkan Mahasiswa: Menampilkan seluruh daftar mahasiswa dalam bentuk tabel yang tersusun
- Cari Mahasiswa: Mencari data spesifik mahasiswa berdasarkan NIM
- Hapus Mahasiswa: Menghapus data mahasiswa dari sistem berdasarkan NIM

## Penjelasan Kode 

### 1. `class Mahasiswa`
![1]()
Class ini memiliki 4 properti utama (NIM, Nama, Prodi, IPK) dan sebuah constructor untuk menerima parameter dan menginisialisasi nilai awal properti saat awal objek baru dibuat

### 2. `Main(string[] args)`
![2]()
Fungsi ini adalah fungsi utama ketika sistem dijalankan pertama kali. Mengatur alur program menggunakan `do-while` agar menu terus tampil sampai user memilih untuk keluar dari menu

### 3. `TampilkanMenu()`
![3]()
Membersihkan layar terminal (`Console.Clear()`) dan mencetak tampilan antarmuka (UI) menu utama berisi daftar opsi 1–5 ke layar

### 4. `TambahMahasiswa()`
![4]()
Meminta masukan NIM, Nama, dan Program Studi dari user. Memiliki perulangan `while(true)` khusus untuk validasi IPK agar user wajib memasukkan angka di rentang **0 hingga 4** (tidak boleh koma). Setelah data valid, fungsi membuat objek `Mahasiswa` baru dan menyimpannya ke dalam `daftarMahasiswa`

### 5. `TampilkanMahasiswa()`
![5]()
Mengecek apakah `daftarMahasiswa` masih kosong. Jika berisi data, fungsi ini mencetak seluruh daftar mahasiswa ke konsol menggunakan perulangan `foreach`

### 6. `CariMahasiswa()`
![6]()
Menerima input NIM yang ingin dicari, lalu melakukan pencarian pada `daftarMahasiswa`. Jika NIM cocok, data mahasiswa tersebut akan dicetak ke layar

### 7. `HapusMahasiswa()`
![7]()
Menerima input NIM yang ingin dicari. Jika cocok, objek tersebut langsung dihapus dari daftar menggunakan method bawaan `daftarMahasiswa.Remove(mahasiswaDitemukan)`

Untuk menjalankan programnya, ketik `dotnet run` pada directory (folder) yang sedang di kerjakan.
