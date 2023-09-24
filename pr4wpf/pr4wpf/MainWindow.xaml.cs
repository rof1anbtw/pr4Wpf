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

namespace pr4wpf
{
 
    public partial class MainWindow : Window
    {
        private List<BudgetRecord> budgetRecords = new List<BudgetRecord>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox();

            // Загрузка данных при открытии приложения
            budgetRecords = DataSerializer.DeserializeData<BudgetRecord>("budget_data.json");
            budgetDataGrid.ItemsSource = budgetRecords;
        }

        private void InitializeComboBox()
        {
            // Здесь вы можете добавить элементы в выпадающий список (типы записей)
            typeComboBox.Items.Add("Тип 1");
            typeComboBox.Items.Add("Тип 2");
            typeComboBox.Items.Add("Тип 3");
            // ...
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из элементов управления
            string name = nameTextBox.Text;
            string type = typeComboBox.SelectedItem as string;
            decimal amount;
            if (decimal.TryParse(amountTextBox.Text, out amount))
            {
                bool isIncome = amount >= 0; // Определяем, является ли запись доходом или расходом
                BudgetRecord record = new BudgetRecord(name, type, Math.Abs(amount), isIncome, DateTime.Now);
                budgetRecords.Add(record);

                // Очищаем поля ввода после добавления записи
                nameTextBox.Text = string.Empty;
                typeComboBox.SelectedIndex = -1;
                amountTextBox.Text = string.Empty;

                // Обновляем DataGrid
                budgetDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Введите корректную сумму.");
            }
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную запись
            BudgetRecord selectedRecord = budgetDataGrid.SelectedItem as BudgetRecord;
            if (selectedRecord != null)
            {
                // Получаем новые данные из элементов управления
                string newName = nameTextBox.Text;
                string newType = typeComboBox.SelectedItem as string;
                decimal newAmount;
                if (decimal.TryParse(amountTextBox.Text, out newAmount))
                {
                    bool newIsIncome = newAmount >= 0;
                    selectedRecord.Name = newName;
                    selectedRecord.Type = newType;
                    selectedRecord.Amount = Math.Abs(newAmount);
                    selectedRecord.IsIncome = newIsIncome;

                    // Очищаем поля ввода после изменения записи
                    nameTextBox.Text = string.Empty;
                    typeComboBox.SelectedIndex = -1;
                    amountTextBox.Text = string.Empty;

                    // Обновляем DataGrid
                    budgetDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Введите корректную сумму.");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.");
            }
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную запись
            BudgetRecord selectedRecord = budgetDataGrid.SelectedItem as BudgetRecord;
            if (selectedRecord != null)
            {
                // Удаляем выбранную запись из списка
                budgetRecords.Remove(selectedRecord);

                // Очищаем поля ввода
                nameTextBox.Text = string.Empty;
                typeComboBox.SelectedIndex = -1;
                amountTextBox.Text = string.Empty;

                // Обновляем DataGrid
                budgetDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSerializer.SerializeData("C:\\Users\\percy\\source\\repos\\pr4wpf\\pr4wpf\\budget_data.json", budgetRecords);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
