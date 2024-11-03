﻿using QuizConfig.Models;
using QuizConfig.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace QuizConfig
{
    internal class FileHandler
    {
        public MainVM MainVM { get; set; }

        public FileHandler(MainVM mainWindowViewModel)
        {
            this.MainVM = mainWindowViewModel;
        }


        public void LoadFromFile()
        {
            try
            {
                string path = GetFullPath("Resources/Data/Data.json");
                string jsonString = File.ReadAllText(path);
                var loadedPackList = JsonSerializer.Deserialize<ObservableCollection<QuestionPackModel>>(jsonString);
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


        public void SaveToFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(MainVM.QuestionPacks);
                string path = GetFullPath("Resources/Data/Data.json");
                File.WriteAllText(path, jsonString);
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