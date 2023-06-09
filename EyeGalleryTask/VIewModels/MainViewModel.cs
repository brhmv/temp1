﻿using EyeGalleryTask.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace EyeGalleryTask.VIewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<string>? Paths { get; set; } = new();

        private Page? page;

        public Page? Page
        {
            get { return page; }
            set { Set(ref page, value); }
        }

       
        public RelayCommand OpenFolderCommand
        {
            get => new RelayCommand(() =>
            {
                using FolderBrowserDialog fbd = new();
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var photoFiles = Directory.EnumerateFiles(fbd.SelectedPath, "*.png");
                    Paths?.Clear();
                    foreach (var photo in photoFiles)
                    {
                        Paths?.Add(photo);
                    }
                    Page = new ListboxPhotos { DataContext = this };
                }
            });
        }

        public RelayCommand AddFileCommand
        {
            get => new RelayCommand(() =>
            {
                using OpenFileDialog ofd = new();
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Paths?.Add(ofd.FileName);
                }
                Page = new ListboxPhotos { DataContext = this };
            });
        }
    }
}
