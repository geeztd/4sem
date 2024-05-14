using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11.MVVM {
    class MainViewModel : ViewModelBase {
        public ObservableCollection<ConsultationViewModel> ConsultationsList { get; set; }

        public MainViewModel(List<Consultation> consultation) {
            ConsultationsList = new ObservableCollection<ConsultationViewModel>(consultation.Select(b => new ConsultationViewModel(b)));
        }
    }
}
