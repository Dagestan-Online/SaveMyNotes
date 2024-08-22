using Save_My_Notes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Save_My_Notes.Pages
{
    /// <summary>
    /// Логика взаимодействия для NotesListPage.xaml
    /// </summary>
    public partial class NotesListPage : Page
    {
        ApplicationContext db = new ApplicationContext();
        Frame masterFrame; //создания переменной типа frame, необходимой для навигации
        Note[] note; //создания массива данных
        public NotesListPage(Frame frame)
        {
            InitializeComponent();
            masterFrame = frame; //присвоение переменной текущей страницы
            LoadData(); //обновление списка
        }

        //редактирование данных
        private void EditInfoClick(object sender, RoutedEventArgs e)
        {
            masterFrame.Navigate(new NoteInfoPage(db, masterFrame, (sender as Button).DataContext as Note, "edit")); //переход на стараницу изменения данных с указанием бд, страницы, откуда совершается переход, конкретного элемента из списка и "действия", необходимого для понимания нужной операции
            LoadData();//обновление списка
        }

        //удаление данных
        private void DeleteInfoClick(object sender, RoutedEventArgs e)
        {
            var selectedElement = (sender as Button).DataContext as Note; //выбор конкретного элемента из списка
            db.Notes.Remove(selectedElement); // удаление элемента из бд
            db.SaveChanges(); //сохраненеи изменений
            LoadData();//обновление списка
        }

        //метод для обновления данных в списке
        private void LoadData()
        {
            db = new ApplicationContext(); //создание нового контекста данных
            note = db.Notes.ToArray(); //загрузка в массив данных из бд
            lvInfo.ItemsSource = note; // заполнение списка из массива
        }
    }
}
