using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TextureBackgroundViewer.ViewModels;

namespace TextureBackgroundViewer;

public partial class TextureInfo : ObservableObject
{
    private readonly string fileName;
    private Lazy<FileInfo> fi => new(() => new FileInfo(fileName));

    private readonly MainWindowViewModel parentViewModel;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    public bool isRefreshed = false;

    [ObservableProperty]
    public bool isSelected = false;

    public TextureInfo(string fileName, MainWindowViewModel parentViewModel)
    {
        this.fileName = fileName;
        this.parentViewModel = parentViewModel;
    }

    public string FullName => IsRefreshed ? fi.Value.FullName : "";
    public string ShortName => IsRefreshed ? fi.Value.Name : Path.GetFileName(fileName);

    [RelayCommand]
    public void SetThisTexture()
    {
        parentViewModel.TexturesCollection.ToList().ForEach(i => i.IsSelected = false);

        IsRefreshed = true;
        IsSelected = true;
        parentViewModel.SetTexture(this);

        Debug.WriteLine($"Текстура {ShortName} подгружена");
    }

    [RelayCommand]
    public void RevealThisTextureInExplorer()
    {
        Process.Start("explorer.exe", "/select," + this.FullName.Replace(@"/", @"\"));
    }
}