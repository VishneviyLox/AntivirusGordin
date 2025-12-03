namespace AntivirusGordin
{
    // Класс главной формы приложения (окна)
    public partial class Form1 : Form
    {
       
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
            SignatureWrite(); // Загрузка "базы данных" вирусных сигнатур
        }

        #region Lists
        // Список для хранения вирусных сигнатур (база данных)
        List<Signature> _signature = new List<Signature>();
        // Список путей к файлам, в которых найден вирус (для удобства удаления)
        List<string> _virusFile = new List<string>();
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
        // В скобках указаны параметры, которые метод принимает в себя, для заполнения списка Signature черех объект signatureInstance
        void SignatureWrite(string viruce, string signatureText, string descriptionViruce) 
        {
            Signature signatureInstance; // Объявляение объекта структуры
            signatureInstance.ViruceName = viruce;// Присваивает значения полю ViruceName
            signatureInstance.SignatureName = signatureText;// Присваивает значения полю SignatureName
            signatureInstance.DescriptionViruce = descriptionViruce; // Присваивает значения полю DescriptionViruce
            _signature.Add(signatureInstance); // Добавляет сигнатуру в список (базу данных)
        }

        // Метод для "загрузки" всей базы данных сигнатур (эмуляция)
        // Перегрузка метода. Внутри заполняются поля в список (базу данных), данные параметры передаются методу наверх
        void SignatureWrite()
        {
            //Заполнение параметров: viruce, signatureText, descriptionViruce
            SignatureWrite("A1B2.478", "A1B2C3D4", "Опасный замещающий вирус.  По  окончании  своей работы вирус  имитирует ошибку позиционирования на текущем диске");
            SignatureWrite("Adolf.475", "Adolf Hitler", "Опасный   резидентный   вирус.  С  вероятностью  1/8 блокирует удаление файлов.");
            SignatureWrite("Aija", "Tks to B.B., Z-VirX ..... [Aija]. ", "Очень опасный Boot-вирус. 25 марта уничтожает содержимое первого сектора на  всех  цилиндрах  активного  раздела  DOS");
            SignatureWrite("Als.339", "XA-XA 1.01 A.L.S. ", "Очень  опасный  нерезидентный,  замещающий  программный  код, вирус");
            SignatureWrite("Amz.789", "AMZ", "24 сентября пытается  уничтожить  некоторые  сектора  всех  доступных дисков");
            SignatureWrite("Amz.1100", "AMZ", "1 марта и 13  сентября пытается  уничтожить  некоторые  сектора  всех  доступных дисков, также  иногда  уничтожает  содержимое  CMOS-памяти и создает файл BOPS - BOP.S.");
            SignatureWrite("Anarchy.2048", "ГрОб", "Заражает EXE- и COM-файлы (COM-файлы первая команда  которых  не JMP NEAR PTR(0E9h ? ?)), внедряя в начало файла 2048 байт своего  тела в  формате EXE-файла.");
        }

        // Метод для записи информации о найденном зараженном файле
        void InfectedFileWrite(string path, Signature viruce)
        {
            InfectedFile infectedFileInstance; // Объект для хранения данных о зараженном файле
            infectedFileInstance.infectedFilePath = path; // Присваеваем полученный путь для файла (для отображения имени файла в таблице)
            infectedFileInstance.viruceFound = viruce; // добавляем информацию о вирусе по структуре Signature (ViruceName, SignatureName, DescriptionViruce)
            _infretctedFiles.Add(infectedFileInstance); // Добавляет файл в список найденных угроз (по этому списку мы заполняем таблицу DataGridView)
        }

        // Метод для обновления отображения списка зараженных файлов в таблице (DataGridView)
        void Read()
        {
            dataGridView1.Rows.Clear(); // Очистка таблицы (для корректного заполнения таблицы, если мы не очистим, данные будут наслаиваться каждый раз)

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
        // Событие клика на кнопку "Выбрать папку"
        void SelectFolderBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog(); // Показ диалогового окна выбора папки
            AdressFolderText.Text = $"Адрес папки: {folderBrowserDialog.SelectedPath}"; // Обновление текста, который отображает путь до выбранной папки
            ScanFolderBtn.Show(); // Показ кнопки "Сканировать"
        }

        // Событие клика на кнопку "Сканировать папку"
        void ScanFolderBtn_Click(object sender, EventArgs e)
        {
            // Получение массива файлов с расширением *.txt в выбранной папке
            string[] search = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.txt");

            if (search.Length > 0) // Если файлы найдены
            {
                // Очищение списков перед началом нового сканирования (та же логика, что и с очищением таблицы DataGridView, чтобы данные не наслаивались)
                _infretctedFiles.Clear();
                _virusFile.Clear();
                progressBar1.Maximum = search.Length; // Установка максимума для прогресс-бара (для демонстрации постепенного выполнения работы)

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
                        progressBar1.Increment(1); // Увеличение прогресса на 1 ед, после просмотра каждого файла (итак вплоть до того, пока весь массив search не переберётся)
                    }

                    if (nalVirusuces) // Проверка наличия вируса (если есть, выполняем условие ниже)
                    {
                        _virusFile.Add(item); // Добавление пути до файла в список файлов для удаления
                        // Показ кнопок удаления
                        DeleteSelectedVirusBtn.Show();
                        DeleteAllVirusesBtn.Show();
                    }
                    stream.Close(); // Закрытие (потока) файла после чтения (без закрытия мы не сможем удалять файлы)
                    Read(); // Обновление таблицы с результатами
                }

            }
            else
            {
                // Сообщение, если не найдено ни одного файла *.txt
                MessageBox.Show("Список для сканирования пуст, проверьте папку.", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        // Событие клика на кнопку "Удалить выбранный вирус"
        void DeleteSelectedVirusBtn_Click(object sender, EventArgs e)
        {
            // Проверка, выбрана ли строка в таблице (если нет, выводим сообщение об ошибке)
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Пожалуйста, выберите файл для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                // return в данном случае говорит о том, что код, который после условия не нужно читать, и у нас всё оборвётся на этой строке (дальше выполнения не будет)
            }
            // Объявляем объект выбранной строки (для идентификации выбранной строки пользователем)
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
            // создаём 2 переменных: для пути до файла и названия вируса (имя вируса мы выбираем для отображения в сообщении диалогового окна)
            string path;
            string viruceName;

            try
            {
                // Получение пути и имени вируса из выбранной строки (нулевая и первая колонка)
                path = selectedRow.Cells[0]?.Value?.ToString();
                viruceName = selectedRow.Cells[1]?.Value?.ToString();

                // проверка на пустое значение (если переменная с путём пустая, то выводим ошибку)
                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("Не удалось получить путь к файлу из выбранной строки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //Ловим ошибку и выводим её в сообщении
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении данных из таблицы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Запрос подтверждения у пользователя
            string message = $"Вы уверены, что хотите удалить файл:\n{path}\n\nНайденный вирус: {viruceName ?? "N/A"}";
            if (MessageBox.Show(message, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


                // Удаление файла с диска
                File.Delete(path);

            // Удаление пути из вспомогательного списка
            _virusFile.Remove(path);

            // Удаление объектов из списка инфицированных файлов по его пути
            for (int i = _infretctedFiles.Count - 1; i >= 0; i--)
            {
                if (_infretctedFiles[i].infectedFilePath == path)
                {
                    _infretctedFiles.RemoveAt(i);
                }
            }
            // Обновление таблицы: метод Read() очищает DataGridView и заполняет его 
            // данными из обновленного списка _infectedFiles
            Read();
            // Уведомляем об успешном удалении
                MessageBox.Show("Файл успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Важное напоминание о перезагрузке (для резидентных угроз)
                MessageBox.Show("Для удаления резидентных копий из оперативной памяти перезагрузите, пожалуйста, компьютер");

         
        }

        // Событие клика на кнопку "Удалить все вирусы"
        void DeleteAllVirusesBtn_Click(object sender, EventArgs e)
        {
            // Проверка, есть ли что удалять
            if (_virusFile.Count == 0)
            {
                MessageBox.Show("Список для удаления пуст.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string fn in _virusFile) // Перебор всех найденных зараженных файлов
            {
                // Удаление файла с диска
                File.Delete(fn);
            }

            // Очистка всех списков и обновление интерфейса
            _virusFile.Clear();
            _infretctedFiles.Clear();
            Read();

            MessageBox.Show("Вирусные файлы удалены. Для удаления резидентных копий из оперативной памяти перезагрузите, пожалуйста, компьютер ");
        }
        #endregion

    }
}