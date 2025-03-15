using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TextureBackgroundViewer.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentColorBackground))]
    public double opacitySliderValue = 0.9;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentColorBackground))]
    public Color selectedColor = Colors.White;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    public string selectedTexturePath = @"D:\Programming\Assets\Textures";

    [ObservableProperty]
    public ObservableCollection<TextureInfo> texturesCollection = new();

    [ObservableProperty]
    public ObservableCollection<SizeSelectorItem> sizeTemplates = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    public string texturesFolder = @"D:\Programming\Assets\Textures";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextureWidth))]
    [NotifyPropertyChangedFor(nameof(TextureHeight))]
    public ImageSource? currentTextureBackground = null;

    public SolidColorBrush CurrentColorBackground
    {
        get
        {
            byte opacityChannel = (byte)(byte.MaxValue - (byte.MaxValue * OpacitySliderValue));
            var gridBrush = Color.FromArgb(opacityChannel, SelectedColor.R, SelectedColor.G, SelectedColor.B);
            return new SolidColorBrush(gridBrush);
        }
    }

    public string Title
    {
        get => $"Textures Viewer | {SelectedTexturePath ?? "<Не выбрано>"} | Папка: {TexturesFolder ?? "<Не указано>"}";
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentTextureTileBrushRect))]
    public int textureWidth = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentTextureTileBrushRect))]
    public int textureHeight = 0;

    public Rect CurrentTextureTileBrushRect
    {
        get
        {
            return new Rect(0, 0, (double)TextureWidth, (double)TextureHeight);
        }
    }

    public MainWindowViewModel()
    {
        Loaded();
    }

    [RelayCommand]
    public async Task Loaded()
    {
        var textureFiles = Directory.GetFiles(TexturesFolder, "*.*", SearchOption.AllDirectories)
        .Take(50)
        .Select(file => new FileInfo(file))
        .OrderByDescending(file => file.CreationTime)
        .ToArray();

        foreach (var file in textureFiles)
        {
            TexturesCollection.Add(new TextureInfo(file, this));
        }
    }

    [RelayCommand]
    public void SetTexture(TextureInfo texture)
    {
        SelectedTexturePath = texture.FullName;
        BitmapImage? img = new(new Uri(SelectedTexturePath, UriKind.Relative));
        CurrentTextureBackground = img;
        TextureWidth = (int)img.Width;
        TextureHeight = (int)img.Height;

        SizeTemplates.Clear();


        double stepSize = 0.2;
        for (int i = 1; i < 10; i++)
        {
            double ratio = stepSize * i;
            int w = (int)(img.Width * ratio);
            int h = (int)(img.Height * ratio);
            bool isOriginal = w == TextureWidth && h == TextureHeight;

            SizeTemplates.Add(new SizeSelectorItem(this, w, h, isOriginal));
        }
    }

    [RelayCommand]
    public void SetColor(object tagColor)
    {
        SelectedColor = tagColor.ToString() switch
        {
            "White" => Colors.White,
            "Black" => Colors.Black,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetViewportSize(int w, int h)
    {
        this.TextureWidth = w;
        this.TextureHeight = h;
    }
}
