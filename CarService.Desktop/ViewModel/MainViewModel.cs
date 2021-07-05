using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarService.Desktop.Model;
using CarService.Desktop.Persistence;
using CarService.Data;

namespace CarService.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICarServiceModel _model;
        private ObservableCollection<ReservationDTO> _reservations;
        private ObservableCollection<Cost> _costs;
        private ObservableCollection<Cost> _addedCosts;
        private Cost _selectedCostList;
        private Cost _selectedCostDropDown;
        private ReservationDTO _selectedReservation;
        private Boolean _isLoaded;
        private int _totalCost;

        public ObservableCollection<ReservationDTO> Reservations
        {
            get { return _reservations; }
            private set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Cost> AddedCosts
        {
            get { return _addedCosts; }
            private set
            {
                if (_addedCosts != value)
                {
                    _addedCosts = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TotalCost
        {
            get { return _totalCost; }
            private set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cost> Costs
        {
            get { return _costs; }
            private set
            {
                if (_costs != value)
                {
                    _costs = value;
                    OnPropertyChanged();
                }
            }
        }

        public Boolean IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        public Cost SelectedCostList
        {
            get { return _selectedCostList; }
            set
            {
                if (_selectedCostList != value)
                {
                    _selectedCostList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Cost SelectedCostDropDown
        {
            get { return _selectedCostDropDown; }
            set
            {
                if (_selectedCostDropDown != value)
                {
                    _selectedCostDropDown = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand CreateWorksheetCommand { get; private set; }
        public DelegateCommand AddCostCommand { get; private set; }
        public DelegateCommand DeleteCostCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand LoadCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public event EventHandler ExitApplication;
        public event EventHandler WorksheetEditingStarted;
        public event EventHandler WorksheetEditingFinished;
        public event EventHandler Logout;

        public MainViewModel(ICarServiceModel model)
        {
            _model = model ?? throw new ArgumentNullException("model");
            _isLoaded = false;

            CreateWorksheetCommand = new DelegateCommand(param => CreateWorksheet(param as ReservationDTO));
            LoadCommand = new DelegateCommand(param => LoadAsync());
            SaveCommand = new DelegateCommand(param => SaveAsync());
            ExitCommand = new DelegateCommand(param => OnExitApplication());
            AddCostCommand = new DelegateCommand(param => AddCost(param as Cost));
            DeleteCostCommand = new DelegateCommand(param => DeleteCost(param as Cost));
            LogoutCommand = new DelegateCommand(param => OnLogout());
        }

        private void AddCost(Cost cost)
        {
            if (cost == null || AddedCosts.Contains(cost)) return;
            _model.AddCost(cost);
            TotalCost += cost.Amount;
            AddedCosts.Add(cost);
            OnPropertyChanged();
        }

        private void DeleteCost(Cost cost)
        {
            if (!AddedCosts.Contains(cost)) return;
            _model.RemoveCost(cost);
            TotalCost -= cost.Amount;
            AddedCosts.Remove(cost);
            OnPropertyChanged();
        }

        private void CreateWorksheet(ReservationDTO reservation)
        {
            if (reservation == null)
                return;
            TotalCost = 0;
            _model.SelectedReservation = SelectedReservation;
            _model.ClearAddedCosts();
            AddedCosts = new ObservableCollection<Cost>();

            OnWorksheetEditingStarted();
        }

        private async void LoadAsync()
        {
            try
            {
                await _model.LoadAsync();
                Reservations = new ObservableCollection<ReservationDTO>(_model.Reservations); 
                Costs = new ObservableCollection<Cost>(_model.Costs);
                IsLoaded = true;
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A betöltés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        private async void SaveAsync()
        {
            try
            {
                MessageBoxResult result =
                    MessageBox.Show("Do you want to save this worksheet?",
                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await _model.SaveAsync();
                    OnMessageApplication("A mentés sikeres!");
                    OnWorksheetEditingFinished();
                    LoadAsync();
                }
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A mentés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        private void OnExitApplication()
        {
            ExitApplication?.Invoke(this, EventArgs.Empty);
        }

        private void OnWorksheetEditingStarted()
        {
            WorksheetEditingStarted?.Invoke(this, EventArgs.Empty);
        }

        private void OnWorksheetEditingFinished()
        {
            WorksheetEditingFinished?.Invoke(this, EventArgs.Empty);
        }
        private void OnLogout()
        {
            Logout?.Invoke(this, EventArgs.Empty);
        }
    }
}
