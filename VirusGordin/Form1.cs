namespace VirusGordin
{
    public partial class Form1 : Form
    {
        //ВСЯ ТЕОРИЯ БУДЕТ НАПИСАНА В README ФАЙЛЕ, ВСЕ НЕОБХОДИМЫЕ ПОДСКАЗКИ И ПОЯСНЕНИЯ К КОДУ БУДУТ ТАМ
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ScanFolderBtn.Hide();
            DeleteSelectedVirusBtn.Hide();
            DeleteAllVirusesBtn.Hide();
        }

        #region BtnsClick
        private void SelectFolderBtn_Click(object sender, EventArgs e)
        {
            // Создаём объект OpenFileDialog  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Задаем фильтр для выбора типов файлов  
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // Проверяем, был ли файл успешно выбран  
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем полный путь к выбранному файлу  
                string fileName = openFileDialog.FileName;
                ScanFolderBtn.Show();
            }
        }

        private void ScanFolderBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteSelectedVirusBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteAllVirusesBtn_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
