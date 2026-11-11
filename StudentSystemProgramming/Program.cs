using StudentSystemProgramming.Models;
using StudentSystemProgramming.Services;

var service = new RegistrySettingsService();

try
{
    AppSettings settings = service.Load();

    Console.WriteLine("Настройки приложения");
    Console.WriteLine("--------------------");

    Console.WriteLine($"Тема: {settings.Theme}");
    Console.WriteLine($"Запусков: {settings.LaunchCount}");

    settings.LaunchCount++;

    Console.WriteLine();
    Console.WriteLine("Выберите тему:");
    Console.WriteLine("1 - Light");
    Console.WriteLine("2 - Dark");

    string? choice = Console.ReadLine();

    if (choice == "1")
        settings.Theme = "Light";
    else if (choice == "2")
        settings.Theme = "Dark";

    service.Save(settings);

    Console.WriteLine();
    Console.WriteLine("Настройки сохранены.");
    Console.WriteLine($"Запуск № {settings.LaunchCount}");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"Нет доступа к реестру: {ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"Ошибка работы с реестром: {ex.Message}");
}
// здесь используется как верхний уровень демонстрационной программы, чтобы вывести непредвиденную ошибку.
// В реальном приложении не следует без разбора перехватывать все исключения и продолжать работу так, будто сохранение прошло успешно.
catch (Exception ex)
{
    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
}