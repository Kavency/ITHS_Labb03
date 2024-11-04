using QuizConfig.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace QuizConfig.MiscClasses
{
    internal class FileHandler
    {
        public MainVM MainVM { get; set; }
        public JsonSerializerOptions Options { get; set; }

        public FileHandler(MainVM mainWindowViewModel)
        {
            MainVM = mainWindowViewModel;

            Options = new JsonSerializerOptions { Converters = { new JsonDifficultyConverter() } };
        }


        public async Task LoadFromFileAsync()
        {

            try
            {
                string path = GetFullPath("Resources/Data/Data.json");

                string jsonString = await File.ReadAllTextAsync(path);
                var loadedPackList = JsonSerializer.Deserialize<ObservableCollection<QuestionPackVM>>(jsonString, Options);
                MainVM.QuestionPacks = loadedPackList;

                Debug.WriteLine("Data loaded successfully.");
            }
            catch (JsonException e)
            {
                Debug.WriteLine($"JSON error: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error loading file: {e.Message}");
            }
        }


        public async Task SaveToFileAsync()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(MainVM.QuestionPacks, Options);
                string path = GetFullPath("Resources/Data/Data.json");
                await File.WriteAllTextAsync(path, jsonString);
                Debug.WriteLine("File saved successfully!");
            }
            catch (JsonException e)
            {
                Debug.WriteLine($"JSON error: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error saving file: {e.Message}");
            }
        }


        private string GetFullPath(string subPath)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subPath);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }
    }
}
