using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Helpers;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.ViewModel
{
    public partial class ImportViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<DicatadorDTO> items;

        [ObservableProperty]
        public ObservableCollection<AutorDTO> autores;

        public List<DicatadorDTO> itemsOriginales = new List<DicatadorDTO>();


        [ObservableProperty]
        public string idAutorFiltro;

        private readonly IFileService<DicatadorDTO> _fileService;

        [ObservableProperty]
        private ViewModelBase? _selectedViewModel;

        public ImportViewModel(IFileService<DicatadorDTO> fileService)
        {
            _fileService = fileService;

            Items = new ObservableCollection<DicatadorDTO>();
            Autores = new ObservableCollection<AutorDTO>();      
        }

        [RelayCommand]
        public void ImportButton()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constants.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var loadedItems = _fileService.Load(openFileDialog.FileName);
                Items = new ObservableCollection<DicatadorDTO>(loadedItems);
            }
        }

        [RelayCommand]
        public void ExportButton()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constants.JSON_FILTER
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _fileService.Save(saveFileDialog.FileName, Items);
            }
        }
    }
}


