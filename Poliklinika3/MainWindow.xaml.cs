using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using Poliklinika3.Windows;

namespace Poliklinika3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Add_and_Edit_Patient AEP = new Add_and_Edit_Patient(null);
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            //Прописываем строку подключения
            SqlConnection sqlConnection = new SqlConnection(@"data source=DESKTOP-SD3UMCG;initial catalog=Poliklinika;persist security info=True;user id=sa;password=sa;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        public MainWindow()
        {
            InitializeComponent();
            DataTable dataTable = Select(
                "select [id Пациента] as ID, (Фамилия + ' ' + Имя + ' ' + Отчество) as [ФИО Пациента], CONVERT(varchar, [Дата рождения]) as [Дата рождения], [Серия Номер Паспорта], Адрес, Телефон, Пол.Наименование as Пол " +
                "from Пациенты, Пол " +
                "where Пациенты.[id Пола] = Пол.[id пола]");
            Patient.ItemsSource = dataTable.DefaultView;
            dataTable = Select(
                "select [id Записи] as ID, [Мед. карта пациента].[Номер карты] as [Номер мед карты], (Врач.Фамилия + ' ' + Врач.Имя + ' ' + Врач.Отчество) as [ФИО Врача], Диагноз.[Код Диагноза] as [Код диагноза], Диагноз.Наименование as [Диагноз], Назначения " +
                "from [Запись в медкарте], [Мед. карта пациента], Диагноз, Врач, Пациенты " +
                "where [Запись в медкарте].[id Карты] = [Мед. карта пациента].[id Карты] AND[Запись в медкарте].[id Врача] = Врач.[id Врача] AND[Запись в медкарте].[id Диагноза] = Диагноз.[id Диагноза]");
            Record.ItemsSource = dataTable.DefaultView;
            dataTable = Select(
                "select [id Полиса_Запись] as ID, [Кампания выдав Полиса].Название as Кампания, CONVERT(varchar, [Дата выдачи]) as [Дата выдачи], [Номер полиса] " +
                "from Пациенты, [Полис Пациента], [Кампания выдав Полиса] " +
                "where [Пациенты].[id Пациента] = [Полис Пациента].[id Пациента] AND [Полис Пациента].[id Компании] = [Кампания выдав Полиса].[id Компании]");
            Policy.ItemsSource = dataTable.DefaultView;
            dataTable = Select(
                "select [id Времени] as ID, (Врач.Фамилия + ' ' + Врач.Имя + ' ' + Врач.Отчество) as [ФИО Врача], Специализация.Наименование as Специализация, Отделение.Наименование as Отделение, [Время приема] , [День недели].Наименование as [День недели],(Пациенты.Фамилия + ' ' + Пациенты.Имя + ' ' + Пациенты.Отчество) as [ФИО Пациента] " +
                "from [Время приема], [Специализация врача], [День недели], Пациенты, Отделение, Специализация, Врач " +
                "where [Время приема].[id записи ] = [Специализация врача].[id записи] AND [Специализация врача].[id Врача] = Врач.[id Врача] AND [Специализация врача].[id Специал] = Специализация.[id Специал] AND [Специализация врача].[id Отделения] = Отделение.[id Отделения] AND [Время приема].[id Дня] = [День недели].[id дня] AND [Время приема].[id Пациента] = Пациенты.[id Пациента]");
            Reception_time.ItemsSource = dataTable.DefaultView;
        }
        private void Patient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var SI = Patient.SelectedItem;
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки!");
                return;
            }
            DataTable dataTable = Select(
                "select [id Записи] as ID, [Мед. карта пациента].[Номер карты] as [Номер мед карты], (Врач.Фамилия + ' ' + Врач.Имя + ' ' + Врач.Отчество) as [ФИО Врача], Диагноз.[Код Диагноза] as [Код диагноза], Диагноз.Наименование as [Диагноз], Назначения " +
                "from [Запись в медкарте], [Мед. карта пациента], Диагноз, Врач, Пациенты " +
                "where [Мед. карта пациента].[id Пациента] = " + (SI as DataRowView).Row.ItemArray[0] + " AND [Запись в медкарте].[id Карты] = [Мед. карта пациента].[id Карты] AND [Запись в медкарте].[id Врача] = Врач.[id Врача] AND [Запись в медкарте].[id Диагноза] = Диагноз.[id Диагноза]");
            Record.ItemsSource = dataTable.DefaultView;
            dataTable = Select(
                "select [id Полиса_Запись] as ID, [Кампания выдав Полиса].Название as Кампания, CONVERT(varchar, [Дата выдачи]) as [Дата выдачи], [Номер полиса] " +
                "from Пациенты, [Полис Пациента], [Кампания выдав Полиса] " +
                "where [Полис Пациента].[id Пациента] = " + (SI as DataRowView).Row.ItemArray[0] + " AND [Полис Пациента].[id Компании] = [Кампания выдав Полиса].[id Компании]");
            Policy.ItemsSource = dataTable.DefaultView;
            dataTable = Select(
                "select [id Времени] as ID, (Врач.Фамилия + ' ' + Врач.Имя + ' ' + Врач.Отчество) as [ФИО Врача], Специализация.Наименование as Специализация, Отделение.Наименование as Отделение, [Время приема] , [День недели].Наименование as [День недели],(Пациенты.Фамилия + ' ' + Пациенты.Имя + ' ' + Пациенты.Отчество) as [ФИО Пациента] " +
                "from [Время приема], [Специализация врача], [День недели], Пациенты, Отделение, Специализация, Врач " +
                "where  [Время приема].[id Пациента] = " + (SI as DataRowView).Row.ItemArray[0] + " AND [Время приема].[id записи ] = [Специализация врача].[id записи] AND [Специализация врача].[id Врача] = Врач.[id Врача] AND [Специализация врача].[id Специал] = Специализация.[id Специал] AND [Специализация врача].[id Отделения] = Отделение.[id Отделения] AND [Время приема].[id Дня] = [День недели].[id дня]");
            Reception_time.ItemsSource = dataTable.DefaultView;
        }
        private void Add_Patient(object sender, RoutedEventArgs e)
        {
            AEP = new Add_and_Edit_Patient(null);
            AEP.ShowDialog();
        }
        private void Edit_Patient(object sender, RoutedEventArgs e)
        {
            var SI = Patient.SelectedItem;
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки!");
                return;
            }
            DataTable dataTable = Select(
                "select * " +
                "from Пациенты " +
                "where [Пациенты].[id Пациента] = " + (SI as DataRowView).Row.ItemArray[0]);
            AEP = new Add_and_Edit_Patient(dataTable);
            AEP.ShowDialog();
        }
        private void Remove_Patient(object sender, RoutedEventArgs e)
        {
            if (Patient.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки!");
                return;
            }
            if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Подтверждение удаления данных",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                var SI = Patient.SelectedItem;
                DataTable dataTable = Select(
                    "delete from Пациенты " +
                    "where [id Пациента] = " + (SI as DataRowView).Row.ItemArray[0]);
                dataTable = Select(
                    "select [id Пациента] as ID, (Фамилия + ' ' + Имя + ' ' + Отчество) as [ФИО Пациента], CONVERT(varchar, [Дата рождения]) as [Дата рождения], [Серия Номер Паспорта], Адрес, Телефон, Пол.Наименование as Пол " +
                    "from Пациенты, Пол " +
                    "where Пациенты.[id Пола] = Пол.[id пола]");
                Patient.ItemsSource = dataTable.DefaultView;
            }
        }
        private void Refresh_Patient(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = Select(
                "select [id Пациента] as ID, (Фамилия + ' ' + Имя + ' ' + Отчество) as [ФИО Пациента], CONVERT(varchar, [Дата рождения]) as [Дата рождения], [Серия Номер Паспорта], Адрес, Телефон, Пол.Наименование as Пол " +
                "from Пациенты, Пол " +
                "where Пациенты.[id Пола] = Пол.[id пола]");
            Patient.ItemsSource = dataTable.DefaultView;
        }
    }
}
