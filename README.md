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
```csharp
    class Mahasiswa
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Prodi { get; set; }
        public double IPK { get; set; }

        // Constructor
        public Mahasiswa(string nim, string nama, string prodi, double ipk)
        {
            NIM = nim;
            Nama = nama;
            Prodi = prodi;
            IPK = ipk;
        }
    }
```
Class ini memiliki 4 properti utama (NIM, Nama, Prodi, IPK) dan sebuah constructor untuk menerima parameter dan menginisialisasi nilai awal properti saat awal objek baru dibuat

### 2. `Main(string[] args)`
```csharp
static void Main(string[] args)
        {
            int pilihan;

            do
            {
                TampilkanMenu();

                Console.Write("Pilihan: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out pilihan))
                {
                    pilihan = 0;
                }

                Console.WriteLine();

                switch (pilihan)
                {
                    case 1:
                        TambahMahasiswa();
                        break;

                    case 2:
                        TampilkanMahasiswa();
                        break;

                    case 3:
                        CariMahasiswa();
                        break;

                    case 4:
                        HapusMahasiswa();
                        break;

                    case 5:
                        Console.WriteLine("Terima kasih telah menggunakan program.");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak tersedia!");
                        break;
                }

                if (pilihan != 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Tekan ENTER untuk melanjutkan...");
                    Console.ReadLine();
                }
            } while (pilihan != 5);
        }
```
Fungsi ini adalah fungsi utama ketika sistem dijalankan pertama kali. Mengatur alur program menggunakan `do-while` agar menu terus tampil sampai user memilih untuk keluar dari menu

### 3. `TampilkanMenu()`
```csharp
        static void TampilkanMenu()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine(" SISTEM DATA MAHASISWA");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Tambah Mahasiswa");
            Console.WriteLine("2. Tampilkan Mahasiswa");
            Console.WriteLine("3. Cari Mahasiswa");
            Console.WriteLine("4. Hapus Mahasiswa");
            Console.WriteLine("5. Keluar");
            Console.WriteLine("========================================");
        }
```
Membersihkan layar terminal (`Console.Clear()`) dan mencetak tampilan antarmuka (UI) menu utama berisi daftar opsi 1–5 ke layar

### 4. `TambahMahasiswa()`
```csharp
        static void TambahMahasiswa()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine(" TAMBAH MAHASISWA");
            Console.WriteLine("========================================");

            Console.Write("NIM : ");
            string nim = Console.ReadLine();

            Console.Write("Nama : ");
            string nama = Console.ReadLine();

            Console.Write("Program Studi : ");
            string prodi = Console.ReadLine();

            double ipk;

            while (true)
            {
                Console.Write("IPK : ");

                if (double.TryParse(Console.ReadLine(), out ipk))
                {
                    if (ipk >= 0 && ipk <= 4)
                    {
                        break;
                    }
                }

                Console.WriteLine("IPK harus berupa angka 0 - 4.");
            }

            Mahasiswa mahasiswa = new Mahasiswa(nim, nama, prodi, ipk);
            daftarMahasiswa.Add(mahasiswa);

            Console.WriteLine();
            Console.WriteLine("Data mahasiswa berhasil ditambahkan.");
        }
```
Meminta masukan NIM, Nama, dan Program Studi dari user. Memiliki perulangan `while(true)` khusus untuk validasi IPK agar user wajib memasukkan angka di rentang **0 hingga 4** (tidak boleh koma). Setelah data valid, fungsi membuat objek `Mahasiswa` baru dan menyimpannya ke dalam `daftarMahasiswa`

### 5. `TampilkanMahasiswa()`
```csharp
        static void TampilkanMahasiswa()
        {
            Console.Clear();

            Console.WriteLine("==========================================================");
            Console.WriteLine(" DAFTAR MAHASISWA");
            Console.WriteLine("==========================================================");

            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }

            Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,5}", "NIM", "Nama", "Prodi", "IPK");
            Console.WriteLine("----------------------------------------------------------");

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,5:F2}", m.NIM, m.Nama, m.Prodi, m.IPK);
            }

            Console.WriteLine("==========================================================");
        }
```
Mengecek apakah `daftarMahasiswa` masih kosong. Jika berisi data, fungsi ini mencetak seluruh daftar mahasiswa ke konsol menggunakan perulangan `foreach`

### 6. `CariMahasiswa()`
```csharp
        static void CariMahasiswa()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine(" CARI MAHASISWA");
            Console.WriteLine("========================================");

            Console.Write("Masukkan NIM: ");
            string nimCari = Console.ReadLine();

            Mahasiswa mahasiswaDitemukan = null;

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            Console.WriteLine();

            if (mahasiswaDitemukan != null)
            {
                Console.WriteLine("Data ditemukan!");
                Console.WriteLine("NIM : " + mahasiswaDitemukan.NIM);
                Console.WriteLine("Nama : " + mahasiswaDitemukan.Nama);
                Console.WriteLine("Prodi : " + mahasiswaDitemukan.Prodi);
                Console.WriteLine("IPK : " + mahasiswaDitemukan.IPK.ToString("F2"));
            }
            else
            {
                Console.WriteLine("Mahasiswa dengan NIM tersebut tidak ditemukan.");
            }
        }
```
Menerima input NIM yang ingin dicari, lalu melakukan pencarian pada `daftarMahasiswa`. Jika NIM cocok, data mahasiswa tersebut akan dicetak ke layar

### 7. `HapusMahasiswa()`
```csharp
        static void HapusMahasiswa()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine(" HAPUS MAHASISWA");
            Console.WriteLine("========================================");

            Console.Write("Masukkan NIM: ");
            string nimHapus = Console.ReadLine();

            Mahasiswa mahasiswaDitemukan = null;

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimHapus, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            if (mahasiswaDitemukan != null)
            {
                daftarMahasiswa.Remove(mahasiswaDitemukan);

                Console.WriteLine();
                Console.WriteLine("Data mahasiswa berhasil dihapus.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Data mahasiswa tidak ditemukan.");
            }
        }
```
Menerima input NIM yang ingin dicari. Jika cocok, objek tersebut langsung dihapus dari daftar menggunakan method bawaan `daftarMahasiswa.Remove(mahasiswaDitemukan)`

---

Untuk menjalankan programnya, ketik `dotnet run` pada directory (folder) yang sedang di kerjakan.
