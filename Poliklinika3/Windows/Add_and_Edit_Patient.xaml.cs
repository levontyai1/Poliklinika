using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Poliklinika3.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_and_Edit_Patient.xaml
    /// </summary>
    public partial class Add_and_Edit_Patient : Window
    {
        DataTable BPatient;
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
        public Add_and_Edit_Patient(DataTable Patient)
        {
            DataTable dataTable;
            InitializeComponent();
            dataTable = Select(
                "select * from Пол");
            foreach (DataRow row in dataTable.Rows)
            {
                txbPol.Items.Add(row["Наименование"].ToString());
            }
            dataTable = Select(
                        "select [id Полиса_Запись], Название, [Дата выдачи], [Номер полиса] " +
                        "from [Полис Пациента], [Кампания выдав Полиса] " +
                        "where [Полис Пациента].[id Компании] = [Кампания выдав Полиса].[id Компании]");
            foreach (DataRow ROw in dataTable.Rows)
            {
                txbNameKompany.Items.Add(ROw["Название"].ToString());
            }
            if (Patient == null)
            {
                return;
            }
            BPatient = Patient;
            foreach (DataRow Row in Patient.Rows)
            {
                dataTable = Select(
                "select * from Пациенты " +
                "where [Пациенты].[id Пациента] = " + Row["id Пациента"].ToString());
                foreach (DataRow row in dataTable.Rows)
                {
                    txbFamile.Text = row[1].ToString();
                    txbName.Text = row[2].ToString();
                    txbSecondName.Text = row[3].ToString();
                    txbDate.Text = row[4].ToString();
                    txbSeriesNumber.Text = row[5].ToString();
                    txbAddress.Text = row[6].ToString();

                    dataTable = Select(
                        "select Наименование from Пациенты, Пол " +
                        "where[Пациенты].[id Пациента] = " + Row["id Пациента"].ToString() + " AND Пациенты.[id Пола] = Пол.[id пола]");
                    foreach (DataRow ROw in dataTable.Rows)
                    {
                        txbPol.Text = ROw["Наименование"].ToString();
                    }

                    txbTel.Text = row[8].ToString();
                }
                dataTable = Select(
                    "select * from [Полис Пациента], Пациенты " +
                    "where [Полис Пациента].[id Пациента] = " + Row["id Пациента"].ToString());
                foreach (DataRow row in dataTable.Rows)
                {
                    dataTable = Select(
                        "select [id Полиса_Запись], Название, [Дата выдачи], [Номер полиса] " +
                        "from [Полис Пациента], [Кампания выдав Полиса] " +
                        "where [Полис Пациента].[id Компании] = [Кампания выдав Полиса].[id Компании]");
                    foreach (DataRow ROw in dataTable.Rows)
                    {
                        txbNameKompany.Text = ROw["Название"].ToString();
                    }
                    txbDate_of_issue.Text = row["Дата выдачи"].ToString();
                    txbNumberPolicy.Text = row["Номер полиса"].ToString();
                }
                dataTable = Select(
                    "select * from [Мед. карта пациента], Пациенты " +
                    "where [Мед. карта пациента].[id Пациента] = " + Row["id Пациента"].ToString());
                foreach (DataRow row in dataTable.Rows)
                {
                    txbcard.Text = row["Номер карты"].ToString();
                }
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            if (BPatient != null)
            {
                foreach (DataRow Row in BPatient.Rows)
                {
                    string id = "";
                    DataTable dataTable = Select(
                        "select [Пол].[id пола] from Пациенты, Пол " +
                        "where [Пациенты].[id Пациента] = " + Row["id Пациента"].ToString() + " AND Пациенты.[id Пола] = Пол.[id пола]");
                    foreach (DataRow row in dataTable.Rows)
                    {
                        id = row["id пола"].ToString();
                    }
                    dataTable = Select(
                        "UPDATE Пациенты " +
                        "SET Фамилия = '" + txbFamile.Text +
                        "', Имя = '" + txbName.Text +
                        "', Отчество = '" + txbSecondName.Text +
                        "', [Дата рождения] = '" + txbDate.Text +
                        "', [Серия Номер Паспорта] = '" + txbSeriesNumber.Text +
                        "', Адрес = '" + txbAddress.Text +
                        "', [id Пола] = " + id +
                        ", Телефон = '" + txbTel.Text +
                        "' WHERE [id Пациента] = " + Row["id Пациента"].ToString());
                    Close();
                }
            }
            else
            {
                string id = "";
                DataTable dataTable = Select(
                    "select [id пола] from Пол " +
                    "where Наименование = '" + txbPol.Text + "'");
                foreach (DataRow row in dataTable.Rows)
                {
                    id = row["id пола"].ToString();
                }
                dataTable = Select(
                    "INSERT Пациенты " +
                    "VALUES ('" + txbFamile.Text +
                    "', '" + txbName.Text +
                    "', '" + txbSecondName.Text +
                    "', '" + txbDate.Text +
                    "', '" + txbSeriesNumber.Text +
                    "', '" + txbAddress.Text +
                    "', " + id +
                    ", '" + txbTel.Text + "')");
                Close();
            }
        }
    }
}
