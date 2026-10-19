# LAPORAN APLIKASI KALKULATOR (WINDOWS FORM)


## 1. Pendahuluan

Laporan ini disusun untuk mendokumentasikan hasil pembuatan aplikasi **Kalkulator Desktop** menggunakan bahasa pemrograman **C#** dan framework **.NET Windows Forms**. 

Aplikasi ini dirancang untuk menangani operasi aritmatika dasar (penjumlahan, pengurangan, perkalian, dan pembagian) serta mendukung kalkulasi bilangan desimal. Aplikasi disajikan dalam antarmuka grafis (GUI) yang responsif dan mudah digunakan.



## 2. Desain Antarmuka Pengguna (GUI)

Antarmuka dirancang pada file `Form1.Designer.cs` dengan menyusun komponen-komponen berikut:

| Nama Kontrol | Tipe | Properti Utama / Text | Fungsi |
| :--- | :--- | :--- | :--- |
| `lblTitle` | `Label` | `Text = "Calculator"` | Judul aplikasi |
| `txtDisplay` | `TextBox` | `Text = "0"`, `ReadOnly = true` | Layar tampilan input & hasil |
| `btn0` – `btn9` | `Button` | `"0"` hingga `"9"` | Tombol input angka |
| `btnPlus`, `btnMinus`, `btnMultiply`, `btnDivide` | `Button` | `+`, `-`, `×`, `÷` | Tombol operator aritmatika |
| `btnDecimal` | `Button` | `.` | Tombol koma/desimal |
| `btnClear` | `Button` | `C` | Mengosongkan/reset nilai kalkulator |
| `btnEquals` | `Button` | `=` | Menghitung dan menampilkan hasil |

---

## 3. Analisis Implementasi Kode (`Form1.cs`)

### A. Deklarasi Variabel Utama
Empat variabel bertipe `double` dan `string` digunakan untuk menyimpan status perhitungan:
```csharp
double firstNumber = 0;   // Menyimpan angka pertama
double secondNumber = 0;  // Menyimpan angka kedua
double result = 0;        // Menyimpan hasil perhitungan
string operation = "";    // Menyimpan simbol operator (+, -, ×, ÷)
```

### B. Handling Input Angka (`NumberButton_Click`)
Satu *event handler* digunakan secara bersama-sama (*shared handler*) oleh tombol angka 0 hingga 9. Method ini memeriksa apakah display bernilai `"0"`, lalu mengganti atau menambahkan karakter sesuai tombol yang diklik.
```csharp
private void NumberButton_Click(object sender, EventArgs e)
{
    Button button = (Button)sender;

    if (txtDisplay.Text == "0")
        txtDisplay.Text = button.Text;
    else
        txtDisplay.Text += button.Text;
}
```

### C. Handling Operator & Kultur Angka (`OperatorButton_Click`)
Saat operator diklik, angka pertama dikonversi menggunakan `CultureInfo.InvariantCulture` agar pemisah desimal berupa titik (`.`) tetap terbaca konsisten di semua konfigurasi sistem operasi.
```csharp
private void OperatorButton_Click(object sender, EventArgs e)
{
    Button button = (Button)sender;

    firstNumber = double.Parse(txtDisplay.Text, CultureInfo.InvariantCulture);
    operation = button.Text;
    txtDisplay.Clear();
}
```

### D. Eksekusi Perhitungan & Handling Pembagian Nol (`btnEquals_Click`)
Eksekusi kalkulasi dilakukan menggunakan struktur `switch-case`. Dilengkapi juga dengan pengecekan khusus pada pembagian dengan nol menggunakan pembentukan `DivideByZeroException` yang ditangkap oleh blok `try-catch`.
```csharp
private void btnEquals_Click(object sender, EventArgs e)
{
    try
    {
        secondNumber = double.Parse(txtDisplay.Text, CultureInfo.InvariantCulture);

        switch (operation)
        {
            case "+":
                result = firstNumber + secondNumber;
                break;
            case "-":
                result = firstNumber - secondNumber;
                break;
            case "x":
            case "×":
                result = firstNumber * secondNumber;
                break;
            case "÷":
            case "/":
                if (secondNumber == 0)
                    throw new DivideByZeroException("Tidak boleh membahagi dengan kosong (zero).");
                result = firstNumber / secondNumber;
                break;
        }

        txtDisplay.Text = result.ToString();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## 4. Pengujian & Skenario Uji (Testing)

Berikut adalah ringkasan hasil pengujian fitur pada aplikasi Kalkulator Desktop:

| No | Skenario Pengujian | Input | Ekspektasi Output | Hasil Pengujian |
| :---: | :--- | :--- | :--- | :---: |
| 1 | Penjumlahan | `15 + 25 =` | `40` | **BERHASIL** |
| 2 | Pengurangan | `50 - 18 =` | `32` | **BERHASIL** |
| 3 | Perkalian | `7 × 8 =` | `56` | **BERHASIL** |
| 4 | Pembagian Normal | `100 ÷ 4 =` | `25` | **BERHASIL** |
| 5 | Pembagian Desimal | `2.5 × 4 =` | `10` | **BERHASIL** |
| 6 | Pembagian dengan Nol | `10 ÷ 0 =` | Pesan Error Pop-up | **BERHASIL** |
| 7 | Reset Layar (Clear) | Tekan `C` | Display kembali ke `0` | **BERHASIL** |

---
