using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace BLEExample {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {

        ObservableCollection<IDevice> deviceList = new ObservableCollection<IDevice>();
        IAdapter Adapter;
        IBluetoothLE Ble;

        public MainPage() {
            InitializeComponent();

            Ble = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            //var state = ble.State;

            //ble.StateChanged += (s, e) =>
            //{
            //    Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
            //};


        }
        async void OnScan(object sender, EventArgs args) {
            ScanDevices();
        }

        async void ScanDevices() {

            if (Ble.State == BluetoothState.On) {
                Debug.WriteLine("BLE State ON");

                Adapter.ScanTimeout = 10000;
                Adapter.ScanMode = ScanMode.Balanced;
                Adapter.DeviceDiscovered += (s, a) => {
                    Debug.WriteLine(a.Device);
                    //DisplayAlert("Founded", $"{a.Device.ToString()}","ok");
                    if (!deviceList.Contains(a.Device)) {

                        deviceList.Add(a.Device);
                    }

                };
                devicesCollection.ItemsSource = deviceList;
                await Adapter.StartScanningForDevicesAsync();
                Debug.WriteLine("Scanned");
            } else {
                Debug.WriteLine("BLE State is OFF ");
            }
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e) {


            var currentDevice = e.CurrentSelection.FirstOrDefault() as IDevice;

            try {
                await Adapter.ConnectToDeviceAsync(currentDevice);
                await DisplayAlert("Conectado", $"Status {currentDevice.State}", "Ok");

            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }


        }


        //adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
        //await adapter.StartScanningForDevicesAsync();
    }
}

