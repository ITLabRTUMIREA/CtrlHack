using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CtrlHack.Models;
using CtrlHack.Views;
using System.Collections.Generic;
using System.Threading;

namespace CtrlHack.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Verify> Items { get; set; }

        public List<RegionPair> Regions { get; set; }
        bool searchOpened = false;
        public bool SearchOpened 
        {
            get { return searchOpened; }
            set { SetProperty(ref searchOpened, value); }
        }

        public int year = 2018;
        public int Year
        {
            get { return year; }
            set { UpdateFormValue(ref year, value); }
        }

        public string orgName;
        public string OrgName
        {
            get { return orgName; }
            set { UpdateFormValue(ref orgName, value); }
        }

        public string ogrn;
        public string Ogrn
        {
            get => ogrn;
            set => UpdateFormValue(ref ogrn, value);
        }

        public string inn;
        public string Inn
        {
            get { return inn; }
            set { UpdateFormValue(ref inn, value); }
        }

        RegionPair currentRegion;
        public RegionPair CurrentRegion
        {
            get { return currentRegion; }
            set { UpdateFormValue(ref currentRegion, value); }
        }

        

        public Command LoadItemsCommand { get; set; }
        

        public ItemsViewModel()
        {
            Title = "Список проверок";
            Regions = DataStore.Regions;
            Items = new ObservableCollection<Verify>();
            currentRegion = DataStore.Regions[0];
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private void UpdateFormValue<T>(ref T property, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                LoadItemsCommand.Execute(null);
            }
            
        }

        int i = 0;
        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                var my = ++i;
                await Task.Delay(TimeSpan.FromSeconds(1));
                if (my != i)
                    return;
                var items = await DataStore.GetVerifyesAsync(Year, OrgName, Ogrn, Inn, currentRegion.Number);
                Items.Clear();
                foreach (var item in items)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(10));
                    Items.Add(item);
                }
            }
            catch (ArgumentNullException)
            {
                Items.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }
    }
}