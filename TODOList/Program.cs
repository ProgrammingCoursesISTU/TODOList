//Создание необходимых переменных
List<string> tasks = new List<string>();
string userInput;
string path = "TODOList.txt";

//Создание файла для хранения списка дел, если отсутствует
if (File.Exists(path) == false)
{
    using (File.Create(path)) { }
}

ReadTasksFromFile(tasks, path);

do
{
    //Вывод меню взаимодействия
    Console.WriteLine("Введите 1, чтобы добавить задачу;\n" +
                        "Введите 2, чтобы вывести задачи на экран;\n" +
                        "Введите 3, чтобы удалить задачу;\n" +
                        "Введите 4, чтобы выйти из приложения.");
    userInput = Console.ReadLine();
    Console.Clear();

    //Проверка ввода пользователя
    switch (userInput.ToLower())
    {
        //Добавление задачи
        case "1":
            AddNewTask(tasks, path);
            break;
        //Вывод списка задач
        case "2":
            ShowTasksList(tasks);
            break;
        //Удаление элемента списка
        case "3":
            if (tasks.Count == 0)
            {
                Console.WriteLine("Нельзя удалять элементы, список пустой!");
                break;
            }

            DeleteTaskFromList(tasks, path);
            break;
        //Выход из приложения
        case "4":
            Console.WriteLine("Выполняется выход из приложения.");
            WriteTasksToFile(tasks, path);
            break;
        //Вывод при неизвестной команде
        default:
            Console.WriteLine("Неизвестная команда!");
            break;
    }

    Console.WriteLine("Нажмите Enter для продолжения....");
    Console.ReadLine();
    Console.Clear();

} while (userInput != "4");

//Запись в файл
static void WriteTasksToFile(List<string> tasks, string path)
{
    using (StreamWriter streamWriter = new StreamWriter(path))
    {
        foreach (var str in tasks)
        {
            streamWriter.WriteLine(str);
        }
    }
}

//Удаление задачи
static void DeleteTaskFromList(List<string> tasks, string path)
{
    string userInput;
    Console.WriteLine("Текущие задачи:");

    //Вывод списка текущих задач
    for (int counter = 0; counter < tasks.Count; counter += 1)
    {
        int currentTaskIndex = counter + 1;
        Console.WriteLine(currentTaskIndex + "." + tasks[counter]);
    }

    //Проверка ввода номера задачи
    int result = 0;
    do
    {
        Console.WriteLine($"Введите номер задачи для удаления.");
        userInput = Console.ReadLine();
    } while (int.TryParse(userInput, out result) == false || result > tasks.Count || result < 1);

    //Удаление задач
    tasks.RemoveAt(result - 1);
    Console.WriteLine($"Задача {result} успешно удалена.");

    WriteTasksToFile(tasks, path);
}

//Вывод списка текущих задач
static void ShowTasksList(List<string> tasks)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("Нет текущих задач!");
    }
    else
    {
        Console.WriteLine("Текущие задачи:");

        for (int counter = 0; counter < tasks.Count; counter += 1)
        {
            int currentTaskIndex = counter + 1;
            Console.WriteLine(currentTaskIndex + "." + tasks[counter]);
        }
    }
}

//Добавление новой задачи
static void AddNewTask(List<string> tasks, string path)
{
    string userInput;
    Console.WriteLine("Введите текст задачи:");
    userInput = Console.ReadLine();

    //Проверка ввода пользователя
    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Строка пустая, задача не добавлена!");
    }
    else
    {
        //Добавление задачи
        string task = userInput + " " + DateTime.Now;
        tasks.Add(task);
        Console.WriteLine("Добавлена задача:");
        Console.WriteLine(task);

        WriteTasksToFile(tasks, path);
    }
}


//Чтение из файла
static void ReadTasksFromFile(List<string> tasks, string path)
{
    using (StreamReader streamReader = new StreamReader(path))
    {
        while (streamReader.EndOfStream == false)
        {
            string str = streamReader.ReadLine();

            if (string.IsNullOrEmpty(str) == false)
            {
                tasks.Add(str);
            }
        }
    }
}