namespace StudentSystemProgramming.Models;

// Это модель настроек приложения.
// Здесь пока нет работы с реестром. Класс только хранит данные в памяти.
public sealed class AppSettings
{
    public string Theme { get; set; } = "Light";

    public int LaunchCount { get; set; }
    public int SchemaVersion { get; set; } = 1;
}