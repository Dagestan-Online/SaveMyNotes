using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Save_My_Notes.Classes;
using Save_My_Notes.Pages;

namespace Save_My_Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ApplicationContext db = new ApplicationContext(); //создания нового контекста базы данных
        
        public MainWindow()
        {
            InitializeComponent();
            framePages.Navigate(new NotesListPage(framePages)); //открытие страницы со списком заметок
        }

        //добавление новых данных
        private void AddInfo_Click(object sender, RoutedEventArgs e)
        {
            var item = new Note(); //создание новой переменной для добавлнеия данных
            framePages.Navigate(new NoteInfoPage(db, framePages, item, "add")); //перемещение на страницу добавления
        }

        //загрузка данных в список из бд при запуске программы
        private void LoadDataBase()
        {
            db.Database.EnsureCreated(); //гарантия того, что бд точно создана
            db.Notes.Load(); //загрузка данных из бд
            DataContext = db.Notes.Local.ToObservableCollection(); //установка данных контекста
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataBase();
        }
    }
}