using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
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
        IsRefreshed = true;
        parentViewModel.SetTexture(this);
    }
}