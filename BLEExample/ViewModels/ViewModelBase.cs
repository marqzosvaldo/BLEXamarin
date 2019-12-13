using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace BLEExample.ViewModels {
    public class ViewModelBase: BaseViewModel {
        protected Page page;

        public ViewModelBase(Page page) {
            this.page = page;
        }
    }
}
