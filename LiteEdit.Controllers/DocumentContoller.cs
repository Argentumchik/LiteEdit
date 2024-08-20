
using LiteEdit.Models;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace LiteEdit.Controllers
{
    public class DocumentContoller : INotifyPropertyChanged
    {
        private string? _currentPath;
        private MyDocument _document = null;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MyDocument Document
        {
            get => _document;
            set
            {
                _document = value;
                OnPropertyChanged(nameof(Document));
            }
        }
        public string? Content
        {
            get => Document.Content;
            set
            {
                Document.Content = value;
                OnPropertyChanged(nameof(Document.Content));
            }
        }
        public ICommand NewDocumentCommand { get; }
        public ICommand OpenDocumentCommand { get; }
        public ICommand SaveDocumentCommand { get; }
        public ICommand SaveAsDocumentCommand { get; }
        public ICommand ExitCommand { get; }
        public DocumentContoller()
        {
            Document = new MyDocument { FileName = "Untitled", FilePath = AppDomain.CurrentDomain.BaseDirectory };
            NewDocumentCommand = new RelayCommand(NewDocument);
            OpenDocumentCommand = new RelayCommand(OpenDocument);
            SaveDocumentCommand = new RelayCommand(SaveDocument);
            SaveAsDocumentCommand = new RelayCommand(SaveAsDocument);
            ExitCommand = new RelayCommand(Exit);
        }

        public void NewDocument()
        {
            Document = new MyDocument
            {
                FileName = "Untitled",
                Content = string.Empty,
                FilePath = AppDomain.CurrentDomain.BaseDirectory,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
            OnPropertyChanged(nameof(Document));
        }
        public void OpenDocument()
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "Untitled";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                Document.FileName = dialog.SafeFileName;
                Document.Content = File.ReadAllText(dialog.FileName);
                OnPropertyChanged(nameof(Document));
            }
        }
        public void SaveDocument()
        {
            if (_currentPath != null)
            {
                File.WriteAllText(_currentPath, Content);
            }
            else
            {
                SaveAsDocument();
            }
        }
        public void SaveAsDocument()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Untitled";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            dialog.DefaultDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                _currentPath = dialog.FileName;
                File.WriteAllText(dialog.FileName, Content);
            }
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}