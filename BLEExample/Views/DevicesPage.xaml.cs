using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLEExample.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace BLEExample.Views {
    public partial class DevicesPage : ContentPage {

        DevicesViewModel _vm;

        public DevicesViewModel VM {
            get => _vm = new DevicesViewModel(this);
        }
        public DevicesPage() {
            InitializeComponent();

            BindingContext = VM;
            //Debug.WriteLine("");


        }



        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e) {

            try {


                var currentDevice = e.CurrentSelection.FirstOrDefault() as IDevice;


                VM.DeviceSelectedCommand.Execute(currentDevice);


                ((CollectionView)sender).SelectedItem = null;
                //((CollectionView)sender).is
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }

        }
    }
}
