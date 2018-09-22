using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CtrlHack.Models;
using CtrlHack.ViewModels;

namespace CtrlHack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}