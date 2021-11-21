using System.Reactive.Linq;

using Avalonia.Controls;
using Avalonia.ReactiveUI;

using BeatSaberModManager.Services.Implementations.Progress;
using BeatSaberModManager.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;


namespace BeatSaberModManager.Views.Implementations.Windows
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow() { }

        [ActivatorUtilitiesConstructor]
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.WhenAnyValue(x => x.ProgressBarStatusType)
                .Select(GetLocalizedStatus)
                .BindTo(ViewModel, x => x.ProgressBarStatusText);
        }

        private string? GetLocalizedStatus(ProgressBarStatusType statusType) => this.FindResource(statusType switch
        {
            ProgressBarStatusType.None => string.Empty,
            ProgressBarStatusType.Installing => "Status:Installing",
            ProgressBarStatusType.Uninstalling => "Status:Uninstalling",
            _ => string.Empty
        }) as string;
    }
}