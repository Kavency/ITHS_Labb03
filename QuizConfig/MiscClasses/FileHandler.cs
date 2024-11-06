using QuizConfig.Models;
using QuizConfig.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace QuizConfig.MiscClasses
{
    internal class FileHandler
    {
        private string _path;
        private JsonSerializerOptions _options;

        public MainVM MainVM { get; }

        public FileHandler(MainVM mainWindowViewModel)
        {
            MainVM = mainWindowViewModel;
            _options = new JsonSerializerOptions
            {
                Converters = { new JsonDifficultyConverter() },
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }


        public async Task LoadFromFileAsync()
        {

            try
            {
                _path = GetFullPath("Resources/Data/Data.json");

                string jsonString = await File.ReadAllTextAsync(_path);
                var loadedPackList = JsonSerializer.Deserialize<ObservableCollection<QuestionPackVM>>(jsonString, _options);
                MainVM.QuestionPacks = loadedPackList;

                Debug.WriteLine("Data loaded successfully.");
            }
            catch (JsonException e)
            {
                Debug.WriteLine($"JSON error: {e.Message}");
            }
            catch (Exception e)
            {
                MainVM.QuestionPacks.Add(new QuestionPackVM(new QuestionPackModel("Demo pack")));
                MainVM.QuestionPacks[0].Questions.Add(new QuestionVM(new QuestionModel("Is this a demo question?",
                    "Yes, it is.", "No, I don't think so.", "It's a trick question, not a demo.", "What was the question again?")));

                // Message to user on failed load.

                Debug.WriteLine($"Error loading file: {e.Message}");
            }
        }


        public async Task SaveToFileAsync()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(MainVM.QuestionPacks, _options);
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
