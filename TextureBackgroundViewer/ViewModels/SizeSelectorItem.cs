using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace TextureBackgroundViewer.ViewModels;

public partial class SizeSelectorItem : ObservableObject
{
    private readonly MainWindowViewModel parentViewModel;

    private readonly int width;
    private readonly int height;
    private readonly double ratio;

    public string SizeString => $"{width}*{height} x{ratio.ToString("G2", new CultureInfo("en-US"))}";

    public SizeSelectorItem(MainWindowViewModel parentViewModel,
        int width,
        int height,
        double ratio)
    {
        this.parentViewModel = parentViewModel;
        this.width = width;
        this.height = height;
        this.ratio = ratio;
    }

    [RelayCommand]
    public void SetSize()
    {
        parentViewModel.SetViewportSize(width, height);
    }
}
