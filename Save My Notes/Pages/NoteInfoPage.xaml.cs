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
    /// Логика взаимодействия для NoteInfoPage.xaml
    /// </summary>
    public partial class NoteInfoPage : Page
    {
        ApplicationContext db;
        Frame previousFrame;
        Note currentNote;
        string? message = null;
        string action = "";

        public NoteInfoPage(ApplicationContext applicationContext, Frame frame, Note note, string resultString)
        {
            InitializeComponent();
            // присвоение значений переданных переменных локальным переменным
            db = applicationContext;
            previousFrame = frame;
            DataContext = note;
            action = resultString;
        }

        //проверка введенных данных на соответствование требованиям
        private bool CheckInfo()
        {
            currentNote = DataContext as Note;
            if ((currentNote.Title == null && currentNote.Content == null) 
                || (currentNote.Title == "" && currentNote.Content == "")) //проверка на полноту/пустоту текстовых полей (одно из полей может быть пустым)
                message += "Заметка не может быть абсолютно пустая\n";
            if (currentNote.Title == null) //присвоение "пустого текста" вместо полной пустоты
                currentNote.Title = ""; 
            if (currentNote.Content == null) //присвоение "пустого текста" вместо полной пустоты
                currentNote.Content = ""; 
            if (currentNote.Title.Length > 70) //проверка длины заголовка
                message += "Длина заголовка не может быть больше 70 символов\n";
            if (message is null) //если замечаний нет, то операция над данными совершится
                return true;
            else
                return false;
        }

        private void CloseInfoPage(object sender, RoutedEventArgs e)
        {
            previousFrame.Navigate(new NotesListPage(previousFrame)); //возвращение на предыдущее окно со списком
        }

        private void SaveInfo(object sender, RoutedEventArgs e)
        {
            if (CheckInfo()) //если замечаний нет, то операция над данными совершится
            {
                if (action == "add") //то самое "действие", которое нужно для понимания, добавление это или редактирование
                {
                    db.Notes.Add((Note)DataContext); //добавление данных в бд
                }
                db.SaveChanges(); //сохранение изменений
                previousFrame.Navigate(new NotesListPage(previousFrame)); //возвращение на предыдущее окно со списком
            }
            else //если замечания есть, то программа из покажет
            {
                MessageBox.Show(message);
                message = null;
            }
        }
    }
}
