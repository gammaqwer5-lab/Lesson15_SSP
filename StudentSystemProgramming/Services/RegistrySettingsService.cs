using Microsoft.Win32;
using StudentSystemProgramming.Models;

namespace StudentSystemProgramming.Services;

public sealed class RegistrySettingsService
{
    private const string RegistryPath =
        @"Software\StudentSystemProgramming";

    public AppSettings Load()
    {
        var settings = new AppSettings();

        // Открытие ключа
        // OpenSubKey возвращает `null`, если ключ отсутствует
        // Это не исключительная ситуация, потому что приложение может быть запущено впервые
        // Почему используется `using`? 
        // `RegistryKey` реализует `IDisposable`. После работы с ключом его необходимо освободить.
        using RegistryKey? key =
            Registry.CurrentUser.OpenSubKey(RegistryPath);

        // Если ключа нет, возвращаем настройки по умолчанию.
        if (key is null)
        {
            return settings;
        }

        // Чтение значения
        // `GetValue` возвращает `object?`, потому что в реестре могут храниться разные типы данных
        // Поэтому мы проверяем тип через `is string` чуть далее
        object? themeValue = key.GetValue("Theme");
        object? launchCountValue = key.GetValue("LaunchCount");


        if (themeValue is string theme &&
            (theme == "Light" || theme == "Dark"))
        {
            settings.Theme = theme;
        }

        if (launchCountValue is int launchCount &&
            launchCount >= 0)
        {
            settings.LaunchCount = launchCount;
        }

        return settings;
    }

    public void Save(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        using RegistryKey key =
            Registry.CurrentUser.CreateSubKey(
                RegistryPath,
                writable: true);

        if (settings.Theme != "Light" &&
            settings.Theme != "Dark")
        {
            throw new ArgumentException(
                "Недопустимая тема оформления.");
        }

        if (settings.LaunchCount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(settings.LaunchCount));
        }
        // Сохранение
        // Третий параметр явно указывает тип значения
        key.SetValue(
            "Theme",
            settings.Theme,
            RegistryValueKind.String);

        key.SetValue(
            "LaunchCount",
            settings.LaunchCount,
            RegistryValueKind.DWord);
    }
}