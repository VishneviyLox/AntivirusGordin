namespace VirusGordin
{
    // Класс главной формы приложения (окна)
    public partial class Form1 : Form
    {
        // ВСЯ ТЕОРИЯ БУДЕТ НАПИСАНА В README ФАЙЛЕ, ВСЕ НЕОБХОДИМЫЕ ПОДСКАЗКИ И ПОЯСНЕНИЯ К КОДУ БУДУТ ТАМ

        // Конструктор формы - вызывается при создании формы
        public Form1()
        {
            InitializeComponent(); // Инициализация элементов, созданных в дизайнере (кнопки, таблицы и т.д.)
        }

        // Метод, который выполняется при первой загрузке формы
        void Form1_Load(object sender, EventArgs e)
        {
            // Скрытие кнопок, пока папка не выбрана
            ScanFolderBtn.Hide();
            DeleteSelectedVirusBtn.Hide();
            DeleteAllVirusesBtn.Hide();

            Read(); // Обновление DataGridView (покажет пустую таблицу)
            signatureWrite(); // Загрузка "базы данных" вирусных сигнатур
        }

        #region Lists
        // Список для хранения вирусных сигнатур (база данных)
        List<Signature> _signature = new List<Signature>();
        // Список путей к файлам, в которых найден вирус (для удобства удаления)
        List<string> virusFile = new List<string>();
        // Список объектов, содержащих информацию об инфицированных файлах (путь + найденная сигнатура)
        List<InfectedFile> _infretctedFiles = new List<InfectedFile>();
        #endregion

        #region Structs
        // Структура, описывающая одну вирусную сигнатуру
        struct Signature
        {
            public string ViruceName; // Название вируса
            public string SignatureName; // Уникальный кусок кода/текста (сигнатура)
            public string DescriptionViruce; // Краткое описание вируса
        }

        // Структура, описывающая найденный инфицированный файл
        struct InfectedFile
        {
            public string infectedFilePath; // Полный путь к зараженному файлу
            public Signature viruceFound; // Информация о найденной сигнатуре
        }
        #endregion

        #region Methods

        // Метод для добавления одной сигнатуры в базу данных
        void signatureWrite(string viruce, string signatureText, string DescriptionViruce) // Принимает данные сигнатуры
        {
            Signature signatureInstance; // Объявляет объект структуры
            signatureInstance.ViruceName = viruce;
            signatureInstance.SignatureName = signatureText;
            signatureInstance.DescriptionViruce = DescriptionViruce; // Присваивает значения полям структуры
            _signature.Add(signatureInstance); // Добавляет сигнатуру в список (базу данных)
        }

        // Метод для "загрузки" всей базы данных сигнатур (эмуляция)
        void signatureWrite()
        {
            // Вызов метода добавления для каждой "вирусной" записи
            signatureWrite("A1B2.478", "A1B2C3D4", "Опасный замещающий вирус...");
            signatureWrite("Adolf.475", "Adolf Hitler", "Опасный резидентный вирус...");
            // ... и так далее, заполняя базу
            signatureWrite("Anarchy.2048", "ГрОб", "Заражает EXE- и COM-файлы...");
        }

        // Метод для записи информации о найденном зараженном файле
        void InfectedFileWrite(string path, Signature viruce)
        {
            InfectedFile infectedFileInstance; // Объект для хранения данных о зараженном файле
            infectedFileInstance.infectedFilePath = path;
            infectedFileInstance.viruceFound = viruce;
            _infretctedFiles.Add(infectedFileInstance); // Добавляет файл в список найденных угроз
        }

        // Метод для обновления отображения списка зараженных файлов в таблице (DataGridView)
        void Read()
        {
            dataGridView1.Rows.Clear(); // Очистка таблицы

            // Перебор всех найденных зараженных файлов
            foreach (var infect in _infretctedFiles)
            {
                // Добавление новой строки в таблицу с деталями об угрозе
                dataGridView1.Rows.Add(
                    infect.infectedFilePath,          // Путь к файлу
                    infect.viruceFound.ViruceName,    // Название вируса
                    infect.viruceFound.DescriptionViruce, // Описание
                    infect.viruceFound.SignatureName);  // Сама сигнатура
            }
            // Обновление текстового счетчика обнаруженных вирусов
            VirusCountText.Text = $"Обнаружено вирусов: {_infretctedFiles.Count().ToString()}";
        }
        #endregion

        #region BtnsClick
        // Обработчик нажатия кнопки "Выбрать папку"
        void SelectFolderBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog(); // Показ диалогового окна выбора папки
            AdressFolderText.Text = $"Адрес папки: {folderBrowserDialog.SelectedPath}"; // Обновление текста с путем
            ScanFolderBtn.Show(); // Показ кнопки "Сканировать"
        }

        // Обработчик нажатия кнопки "Сканировать папку"
        void ScanFolderBtn_Click(object sender, EventArgs e)
        {
            // Получение списка файлов *.txt в выбранной папке
            string[] search = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.txt");

            if (search.Length != 0) // Если файлы найдены
            {
                // Обнуление списков перед началом нового сканирования
                _infretctedFiles.Clear();
                virusFile.Clear();
                progressBar1.Maximum = search.Length; // Установка максимума для прогресс-бара

                foreach (string item in search) // Перебор каждого найденного файла
                {
                    // Создание объекта для чтения содержимого файла
                    StreamReader stream = new StreamReader(item);
                    bool nalVirusuces = false; // Флаг: вирус найден в текущем файле
                    string read = stream.ReadToEnd(); // Чтение всего содержимого файла в одну строку

                    foreach (var st in _signature) // Перебор всех сигнатур из базы
                    {
                        // ПОИСК СИГНАТУРЫ: Проверка, содержит ли содержимое файла текст сигнатуры
                        if (read.IndexOf(st.SignatureName) != -1)
                        {
                            nalVirusuces = true; // Вирус найден!
                            InfectedFileWrite(item, st); // Запись информации о найденной угрозе
                        }
                        progressBar1.Increment(1); // Увеличение прогресса (неточно, должно быть вне внутреннего цикла)
                    }

                    if (nalVirusuces) // Если в файле был найден хотя бы один вирус
                    {
                        virusFile.Add(item); // Добавление пути в список файлов для удаления
                        // Показ кнопок удаления
                        DeleteSelectedVirusBtn.Show();
                        DeleteAllVirusesBtn.Show();
                    }
                    stream.Close(); // Закрытие файла после чтения
                    Read(); // Обновление таблицы с результатами
                }

            }
            else
            {
                // Сообщение, если не найдено ни одного файла *.txt
                MessageBox.Show("Список для сканирования пуст, проверьте папку.", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        // Обработчик нажатия кнопки "Удалить выбранный вирус"
        void DeleteSelectedVirusBtn_Click(object sender, EventArgs e)
        {
            // Проверка, выбрана ли строка в таблице
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Пожалуйста, выберите файл для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
            string path;
            string viruceName;

            try
            {
                // Получение пути и имени вируса из выбранной строки (нулевая и первая колонка)
                path = selectedRow.Cells[0]?.Value?.ToString();
                viruceName = selectedRow.Cells[1]?.Value?.ToString();

                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("Не удалось получить путь к файлу из выбранной строки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении данных из таблицы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Запрос подтверждения у пользователя
            string message = $"Вы уверены, что хотите удалить файл:\n{path}\n\nНайденный вирус: {viruceName ?? "N/A"}";
            if (MessageBox.Show(message, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return; // Отмена удаления
            }

            try
            {
                // Удаление файла с диска
                File.Delete(path);

                // Удаление пути из вспомогательного списка
                virusFile.Remove(path);

                // Удаление объекта из списка инфицированных файлов по его пути
                var itemToRemove = _infretctedFiles.FirstOrDefault(f => f.infectedFilePath == path);
                _infretctedFiles.Remove(itemToRemove);

                // Удаление строки из отображения в таблице
                dataGridView1.Rows.Remove(selectedRow);

                MessageBox.Show("Файл успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Важное напоминание о перезагрузке (для резидентных угроз)
                MessageBox.Show("Для удаления резидентных копий из оперативной памяти перезагрузите, пожалуйста, компьютер");
            }
            catch (IOException ioEx)
            {
                // Обработка ошибок, если файл занят
                MessageBox.Show($"Не удалось удалить файл. Он может быть занят другим процессом.\nОшибка: {ioEx.Message}", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка прочих ошибок
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик нажатия кнопки "Удалить все вирусы"
        void DeleteAllVirusesBtn_Click(object sender, EventArgs e)
        {
            // Проверка, есть ли что удалять
            if (virusFile.Count == 0)
            {
                MessageBox.Show("Список для удаления пуст.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string fn in virusFile) // Перебор всех найденных зараженных файлов
            {
                // Удаление файла с диска
                File.Delete(fn);
            }

            // Очистка всех списков и обновление интерфейса
            virusFile.Clear();
            _infretctedFiles.Clear();
            Read();

            MessageBox.Show("Вирусные файлы удалены. Для удаления резидентных копий из оперативной памяти перезагрузите, пожалуйста, компьютер ");
        }
        #endregion

    }
}