# LAPORAN PRAKTIKUM: STUDENT MANAGEMENT SYSTEM

![image](https://github.com/notdoppi/PBKK-C/blob/8a18461697b15a0f386ab98fddcec1bb13881808/pertemuan5/WhatsApp%20Image%202026-10-19%20at%2017.23.23.jpeg)

## 1. Pendahuluan

### 1.1 Latar Belakang
Pengembangan aplikasi desktop modern menuntut pemisahan yang jelas antara antarmuka pengguna (*User Interface*) dan logika bisnis (*Business Logic*). Framework **WPF (Windows Presentation Foundation)** pada .NET 8 menyediakan kapabilitas UI yang kaya melalui **XAML** dan mekanisme **Data Binding**. Dipadukan dengan pola arsitektur **MVVM (Model-View-ViewModel)** serta akses data **ADO.NET**, aplikasi yang dihasilkan menjadi lebih terstruktur, modular, dan mudah dikembangkan.

### 1.2 Tujuan Proyek
* Membangun aplikasi pengelolaan data mahasiswa (*Student Management System*) berbasis desktop.
* Mengimplementasikan arsitektur MVVM (Model, Data/Repository, ViewModel, View).
* Memahami fitur *Two-Way Data Binding* dan *ICommand* pada WPF.
* Menghubungkan aplikasi WPF ke SQL Server LocalDB menggunakan ADO.NET (`Microsoft.Data.SqlClient`).
* Mengimplementasikan fungsi CRUD lengkap (Create, Read, Update, Delete), pencarian data, dan statistik mahasiswa.


## 2. Buat Layer Model dan Akses Database (Data)

Layer ini bertanggung jawab atas representasi entitas data serta interaksi langsung ke database SQL Server menggunakan ADO.NET.

### 2.1 Database Script (`StudentDB`)
Struktur tabel utama `Students` pada database:

```sql
CREATE TABLE Students
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NIM VARCHAR(20) NOT NULL,
    Nama VARCHAR(100) NOT NULL,
    Jurusan VARCHAR(100) NOT NULL,
    Gender VARCHAR(20) NOT NULL,
    Email VARCHAR(100)
);
```

### 2.2 Model (`Models/Student.cs`)
Merepresentasikan entitas mahasiswa sesuai struktur kolom di tabel database:

```csharp
namespace StudentManager.Models;

public class Student
{
    public int Id { get; set; }
    public string NIM { get; set; } = "";
    public string Nama { get; set; } = "";
    public string Jurusan { get; set; } = "";
    public string Gender { get; set; } = "";
    public string Email { get; set; } = "";
}
```

### 2.3 Repository (`Data/StudentRepository.cs`)
Menangani query SQL dasar untuk operasi CRUD dan pencarian menggunakan `SqlConnection` & `SqlCommand`:

```csharp
// Connection String ke LocalDB
private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

// Read / Get All Data
public List<Student> GetAll() {
    // SELECT Id, NIM, Nama, Jurusan, Gender, Email FROM Students ORDER BY Id DESC
}

// Create / Insert Data
public void Insert(Student student) {
    // INSERT INTO Students (NIM, Nama, Jurusan, Gender, Email) VALUES (@NIM, @Nama, @Jurusan, @Gender, @Email)
}

// Update Data
public void Update(Student student) {
    // UPDATE Students SET NIM=@NIM, Nama=@Nama, Jurusan=@Jurusan, Gender=@Gender, Email=@Email WHERE Id=@Id
}

// Delete Data
public void Delete(int id) {
    // DELETE FROM Students WHERE Id=@Id
}

// Search Data
public List<Student> Search(string keyword) {
    // SELECT ... WHERE NIM LIKE @Keyword OR Nama LIKE @Keyword OR Jurusan LIKE @Keyword
}
```

---

## 3. Buat Layer Logika Aplikasi (ViewModels)

Layer ViewModel mengelola state aplikasi, koleksi data (*ObservableCollection*), perintah (*ICommand*), serta perhitungan statistik.

### 3.1 RelayCommand (`ViewModels/RelayCommand.cs`)
Pemicu eksekusi perintah dari tombol UI ke metode di ViewModel:

```csharp
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    public RelayCommand(Action execute) => _execute = execute;
    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => _execute();
    public event EventHandler? CanExecuteChanged;
}
```

### 3.2 StudentViewModel (`ViewModels/StudentViewModel.cs`)
Bagian inti logika aplikasi yang mengimplementasikan `INotifyPropertyChanged`:

```csharp
public class StudentViewModel : INotifyPropertyChanged
{
    private readonly StudentRepository _repository = new();
    public ObservableCollection<Student> Students { get; } = new();
    
    // Model yang sedang dipilih / diisi pada form
    private Student? _selectedStudent;
    public Student? SelectedStudent {
        get => _selectedStudent;
        set { _selectedStudent = value; OnPropertyChanged(); }
    }

    // Kalkulasi Statistik Real-time
    public int TotalStudents => Students.Count;
    public int TotalInformatika => Students.Count(x => x.Jurusan == "Informatika");
    public int TotalSistemInformasi => Students.Count(x => x.Jurusan == "Sistem Informasi");

    // Command Handlers
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ResetCommand { get; }
    public ICommand SearchCommand { get; }

    public StudentViewModel() {
        LoadData();
        SaveCommand = new RelayCommand(Save);
        DeleteCommand = new RelayCommand(Delete);
        ResetCommand = new RelayCommand(Reset);
        SearchCommand = new RelayCommand(Search);
        
        Reset(); // Inisialisasi awal form
    }

    private void Save() {
        if (SelectedStudent == null) return;
        if (SelectedStudent.Id == 0) _repository.Insert(SelectedStudent);
        else _repository.Update(SelectedStudent);
        LoadData();
        Reset();
    }

    private void Reset() => SelectedStudent = new Student();
}
```

---

## 4. Hubungkan dan Desain Antarmuka (View)

Layer View disusun menggunakan XAML dan dihubungkan ke ViewModel melalui `DataContext` dan *Data Binding*.

### 4.1 Binding DataContext (`MainWindow.xaml.cs`)
Inisialisasi ViewModel sebagai DataContext antarmuka utama:

```csharp
public MainWindow()
{
    InitializeComponent();
    DataContext = new StudentViewModel();
}
```

### 4.2 Desain Layout XAML (`MainWindow.xaml`)
Potongan elemen XAML kunci untuk form input, statistik, dan tabel data:

```xml
<!-- Form Input Text & ComboBox -->
<TextBox Text="{Binding SelectedStudent.NIM, UpdateSourceTrigger=PropertyChanged}"/>
<ComboBox Text="{Binding SelectedStudent.Jurusan, UpdateSourceTrigger=PropertyChanged}">
    <ComboBoxItem>Informatika</ComboBoxItem>
    <ComboBoxItem>Sistem Informasi</ComboBoxItem>
</ComboBox>

<!-- Tombol Simpan & Reset -->
<Button Content="Simpan" Command="{Binding SaveCommand}"/>
<Button Content="Reset" Command="{Binding ResetCommand}"/>

<!-- Indikator Statistik -->
<TextBlock Text="{Binding TotalStudents}" FontSize="32" FontWeight="Bold"/>
<TextBlock Text="{Binding TotalInformatika}" FontSize="24"/>

<!-- DataGrid Tabel Mahasiswa -->
<DataGrid ItemsSource="{Binding Students}" SelectedItem="{Binding SelectedStudent, Mode=TwoWay}" AutoGenerateColumns="False" IsReadOnly="True">
    <DataGrid.Columns>
        <DataGridTextColumn Header="NIM" Binding="{Binding NIM}"/>
        <DataGridTextColumn Header="Nama" Binding="{Binding Nama}"/>
        <DataGridTextColumn Header="Jurusan" Binding="{Binding Jurusan}"/>
        <DataGridTextColumn Header="Gender" Binding="{Binding Gender}"/>
        <DataGridTextColumn Header="Email" Binding="{Binding Email}"/>
    </DataGrid.Columns>
</DataGrid>
```

---
