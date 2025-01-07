using System.Collections.ObjectModel;
using System.Windows.Input;
//using Android.Service.Credentials;
using AnnuaireEntreprise.Maui.Services.Employee;
using AnnuaireEntreprise.Maui.Services.Service;
using AnnuaireEntreprise.Maui.Services.Site;
using AnnuaireEntreprise.Maui.Views;
using AnnuaireEntreprise.Model.Models;
using CommunityToolkit.Maui.Views;


namespace AnnuaireEntreprise.Maui.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly EmployeeService _employeeService = new EmployeeService();
        private readonly SiteService _siteService = new SiteService();
        private readonly ServiceService _serviceService = new ServiceService();
        private ObservableCollection<SiteDTO> _sites = new();
        private ObservableCollection<EmployeeDTO> _pagedFilteredEmployees = new();
        public ObservableCollection<EmployeeDTO> PagedFilteredEmployees
        {
            get => _pagedFilteredEmployees;
            set => SetProperty(ref _pagedFilteredEmployees, value);
        }

        private ObservableCollection<ServiceDTO> _services = new();
        public ObservableCollection<ServiceDTO> Services
        {
            get => _services;
            set => SetProperty(ref _services, value);
        }
        public ObservableCollection<SiteDTO> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value);
        }

        private Popup? _popup;
        public Popup? Popup
        {
            get => _popup;
            set => SetProperty(ref _popup, value);
        }

        private int _titleClickCount = 0;
        private const int SecretKeyTapThreshold = 11;
        private const string AdminPassword = "Cesi";
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        private bool _isSearchActive;
        public bool IsSearchActive
        {
            get => _isSearchActive;
            set => SetProperty(ref _isSearchActive, value);
        }
        private string? _searchNom;
        public string? SearchNom
        {
            get => _searchNom;
            set => SetProperty(ref _searchNom, value);
        }
        private string? _searchPrenom;
        public string? SearchPrenom
        {
            get => _searchPrenom;
            set => SetProperty(ref _searchPrenom, value);
        }

        private string? _searchEmail;
        public string? SearchEmail
        {
            get => _searchEmail;
            set => SetProperty(ref _searchEmail, value);
        }

        private string? _searchTelephone;
        public string? SearchTelephone
        {
            get => _searchTelephone;
            set => SetProperty(ref _searchTelephone, value);
        }
        private string? _searchTelephoneFixe;
        public string? SearchTelephoneFixe
        {
            get => _searchTelephoneFixe;
            set => SetProperty(ref _searchTelephoneFixe, value);
        }
        private int _pageSize = 15;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (SetProperty(ref _pageSize, value))
                {
                    Task.Run(LoadEmployeesAsync);
                }
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (SetProperty(ref _currentPage, value))
                {
                    LoadEmployeesAsync().ConfigureAwait(false);
                    UpdateCommandStates();
                }
            }
        }

        private int _totalPages = 1;
        public int TotalPages
        {
            get => _totalPages;
            private set => SetProperty(ref _totalPages, value);
        }
        private EmployeeDTO? _selectedInfoEmployee;
        public EmployeeDTO? SelectedInfoEmployee
        {
            get => _selectedInfoEmployee;
            set => SetProperty(ref _selectedInfoEmployee, value);
        }

        private CreateEmployeeDTO? _selectedEmployee;
        public CreateEmployeeDTO? SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        private CreateEmployeeDTO? _selectedEmployeeForModification;
        public CreateEmployeeDTO? SelectedEmployeeForModification
        {
            get => _selectedEmployeeForModification;
            set => SetProperty(ref _selectedEmployeeForModification, value);
        }
        private CreateEmployeeDTO _newEmployee = new CreateEmployeeDTO
        {
            Nom = string.Empty,
            Prenom = string.Empty,
            Email = string.Empty,
            TelephonePortable = string.Empty,
            TelephoneFixe = string.Empty,
            SiteId = 0,
            ServiceId = 0
        };
        public CreateEmployeeDTO NewEmployee
        {
            get => _newEmployee;
            set => SetProperty(ref _newEmployee, value);
        }

        private string _newSiteName = string.Empty;
        public string NewSiteName
        {
            get => _newSiteName;
            set => SetProperty(ref _newSiteName, value);
        }

        private string _newServiceName = string.Empty;
        public string NewServiceName
        {
            get => _newServiceName;
            set => SetProperty(ref _newServiceName, value);
        }
        private SiteDTO? _selectedSite;
        public SiteDTO? SelectedSite
        {
            get => _selectedSite;
            set
            {
                if (SetProperty(ref _selectedSite, value) && value != null)
                {
                    if (SelectedEmployeeForModification != null)
                    {
                        SelectedEmployeeForModification.SiteId = value.Id; // Ensure no recursion
                    }
                }
            }
        }
        private SiteDTO? _selectedSiteForModification;
        public SiteDTO? SelectedSiteForModification
        {
            get => _selectedSiteForModification;
            set
            {
                SetProperty(ref _selectedSiteForModification, value);
                ModifiedSiteName = value?.Ville; // Automatically populate the entry with the current name
            }
        }
        private string? _modifiedSiteName;
        public string? ModifiedSiteName
        {
            get => _modifiedSiteName;
            set => SetProperty(ref _modifiedSiteName, value);
        }

        private ServiceDTO? _selectedService;
        public ServiceDTO? SelectedService
        {
            get => _selectedService;
            set
            {
                if (SetProperty(ref _selectedService, value) && value != null)
                {
                    NewEmployee.ServiceId = value.Id; // Update ServiceId in NewEmployee
                }
            }
        }
        private ServiceDTO? _selectedServiceForModification;
        public ServiceDTO? SelectedServiceForModification
        {
            get => _selectedServiceForModification;
            set
            {
                SetProperty(ref _selectedServiceForModification, value);
                ModifiedServiceName = value?.Nom; // Automatically populate the entry with the current name
            }
        }
        private string? _modifiedServiceName;
        public string? ModifiedServiceName
        {
            get => _modifiedServiceName;
            set => SetProperty(ref _modifiedServiceName, value);
        }

        public ICommand AddEmployeeCommand { get; }
        public ICommand SaveEmployeeChangesCommand { get; }
        public ICommand AddSiteCommand { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PerformSearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        public ICommand DeleteSiteCommand { get; }
        public ICommand ModifyServiceCommand { get; }
        public ICommand ModifySiteCommand { get; }
        public ICommand DeleteServiceCommand { get; }
        public ICommand ShowEmployeeDetailsPopupCommand { get; }
        public ICommand ShowSearchPopupCommand { get; }
        public ICommand ShowSitePopupCommand { get; }
        public ICommand ShowServicePopupCommand { get; }
        public ICommand ShowAddEmployeePopupCommand { get; }
        public ICommand ShowModifyEmployeePopupCommand { get; }
        public ICommand ClosePopupCommand { get; }
        public ICommand TitleClickedCommand { get; }


        public MainPageViewModel()
        {
            PreviousPageCommand = new Command(
                async () => await GoToPreviousPage(),
                () =>
                {
                    Console.WriteLine($"CanExecute PreviousPageCommand: {CurrentPage > 1}");
                    return CurrentPage > 1;
                });

            NextPageCommand = new Command(
                async () =>

                    await GoToNextPage());
            AddEmployeeCommand = new Command(async () => await AddEmployeeAsync());
            SaveEmployeeChangesCommand = new Command(async () => await SaveEmployeeAsync());
            DeleteEmployeeCommand = new Command(async () => await RemoveEmployeeIfPossible());
            AddSiteCommand = new Command(async () => await AddSiteAsync());
            DeleteSiteCommand = new Command<int>(async (Id) => await RemoveSiteIfPossible(Id));
            AddServiceCommand = new Command(async () => await AddServiceAsync());
            ModifyServiceCommand = new Command(async () => await ModifyServiceAsync());
            ModifySiteCommand = new Command(async () => await ModifySiteAsync());
            DeleteServiceCommand = new Command<int>(async (Id) => await RemoveServiceIfPossible(Id));
            _selectedSite = new SiteDTO { Id = 0, Ville = string.Empty };
            _selectedService = new ServiceDTO { Id = 0, Nom = string.Empty };
            ShowEmployeeDetailsPopupCommand = new Command<int>(ShowEmployeeDetailsPopup);

            ShowSearchPopupCommand = new Command(() =>
            {
                var popup = new SearchEmployeePopup { BindingContext = this };
                if (Application.Current?.MainPage != null)
                {
                    Application.Current.MainPage.ShowPopup(popup);
                    Popup = popup;
                }
            });
            ShowSitePopupCommand = new Command(() =>
            {
                var popup = new SitePopup { BindingContext = this };
                if (Application.Current?.MainPage != null)
                {
                    Application.Current.MainPage.ShowPopup(popup);
                    Popup = popup;
                }
            });
            ShowServicePopupCommand = new Command(() =>
            {
                var popup = new ServicePopup { BindingContext = this };
                if (Application.Current?.MainPage != null)
                {
                    Application.Current.MainPage.ShowPopup(popup);
                    Popup = popup;
                }
            });
            ShowAddEmployeePopupCommand = new Command(() =>
            {
                var popup = new AddEmployeePopup { BindingContext = this };
                if (Application.Current?.MainPage != null)
                {
                    Application.Current.MainPage.ShowPopup(popup);
                    Popup = popup;
                }
            });
            ShowModifyEmployeePopupCommand = new Command<int>(ShowModifyEmployeePopup);


            PerformSearchCommand = new Command(async () => await PerformSearchAsync());
            ClearSearchCommand = new Command(async () => await ClearSearchAsync());
            ClosePopupCommand = new Command(() => Popup?.Close());
            TitleClickedCommand = new Command(OnTitleClicked);


            Task.Run(LoadEmployeesAsync);
            Task.Run(LoadSitesAsync);
            Task.Run(LoadServicesAsync);
        }
        private async void ShowEmployeeDetailsPopup(int employeeId)
        {
            try
            {
                // Fetch employee details using the service
                EmployeeDTO employee = await _employeeService.GetEmployeeById(employeeId);

                // Set the SelectedEmployee for data binding in the popup
                SelectedInfoEmployee = employee;

                // Show the popup
                Popup = new ShowEmployeePopup
                {
                    BindingContext = this
                };

                Application.Current?.MainPage?.ShowPopup(Popup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error showing employee details popup: {ex.Message}");
            }
        }
        // private async Task G
        private async Task LoadEmployeesAsync()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees(CurrentPage, PageSize);

                // Update ObservableCollection
                PagedFilteredEmployees.Clear();
                PagedFilteredEmployees = new ObservableCollection<EmployeeDTO>(employees);
                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees found");
                    return;
                }
                foreach (var employee in employees)
                {
                    PagedFilteredEmployees.Add(employee);
                }

                var totalCount = await _employeeService.GetTotalEmployeeCount();
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employees: {ex.Message}");
            }
        }

        private async Task LoadSitesAsync()
        {
            try
            {
                var sites = await _siteService.GetAllSites();
                Sites = new ObservableCollection<SiteDTO>(sites);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sites: {ex.Message}");
            }
        }
        private async Task ModifyServiceAsync()
        {
            try
            {
                if (SelectedServiceForModification == null || string.IsNullOrWhiteSpace(ModifiedServiceName))
                {
                    Console.WriteLine("Invalid input. Please select a service and enter a new name.");
                    Application.Current?.MainPage?.DisplayAlert("Error", "Please select a service and enter a valid new name.", "OK");
                    return;
                }

                // Update the service
                SelectedServiceForModification.Nom = ModifiedServiceName;

                var isUpdated = await _serviceService.ModifyService(SelectedServiceForModification);

                if (isUpdated)
                {
                    Console.WriteLine($"Service {SelectedServiceForModification.Id} updated successfully.");
                    await LoadServicesAsync(); // Refresh the services list
                    ModifiedServiceName = string.Empty; // Clear the input field
                    Popup?.Close();
                    await LoadEmployeesAsync(); // Refresh the employee list
                    SelectedServiceForModification = null; // Clear the selected service
                }
                else
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "Failed to update the service. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating service: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Error", "An unexpected error occurred. Please try again.", "OK");
            }
        }
        private async void ShowModifyEmployeePopup(int employeeId)
        {
            try
            {
                Console.WriteLine($"Fetching employee details for Id: {employeeId}");

                // Fetch employee details using the service
                EmployeeDTO employee = await _employeeService.GetEmployeeById(employeeId);

                // Map the fetched EmployeeDTO to CreateEmployeeDTO
                SelectedEmployeeForModification = new CreateEmployeeDTO // Map the EmployeeDTO to CreateEmployeeDTO
                {
                    Id = employee.Id,
                    Nom = employee.Nom,
                    Prenom = employee.Prenom,
                    Email = employee.Email,
                    TelephonePortable = employee.TelephonePortable,
                    TelephoneFixe = employee.TelephoneFixe,
                    SiteId = employee.Site.Id,
                    ServiceId = employee.Service.Id
                };

                // Set SelectedSite and SelectedService for Pickers
                SelectedSite = Sites.FirstOrDefault(s => s.Id == SelectedEmployeeForModification.SiteId);
                SelectedService = Services.FirstOrDefault(s => s.Id == SelectedEmployeeForModification.ServiceId);


                if (Popup != null)
                {
                    Popup.Close();
                }

                Popup = new ModifyEmployeePopup
                {
                    BindingContext = this // Pass the ViewModel as the BindingContext
                };

                Application.Current?.MainPage?.ShowPopup(Popup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening Modify Popup: {ex.Message}");
            }
        }

        private async Task LoadServicesAsync()
        {
            try
            {
                var services = await _serviceService.GetAllServices();
                Services = new ObservableCollection<ServiceDTO>(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading services: {ex.Message}");
            }
        }
        private async Task PerformSearchAsync()
        {
            try
            {
                // Convert SelectedSite and SelectedService to their respective IDs
                int? siteId = SelectedSite?.Id;
                int? serviceId = SelectedService?.Id;

                // Create a new SearchEmployeeDTO object
                var searchEmployeeDTO = new SearchEmployeeDTO
                {
                    Nom = SearchNom ?? null,
                    Prenom = SearchPrenom ?? null,
                    Email = SearchEmail ?? null,
                    Telephone = SearchTelephone ?? null,
                    SiteId = siteId ?? null,
                    ServiceId = serviceId ?? null,
                    Page = CurrentPage,
                    PageSize = PageSize
                };

                // Send the search request to the API
                var employees = await _employeeService.SearchEmployeeByArg(searchEmployeeDTO);

                if (employees?.Count == 0)
                {
                    Console.WriteLine("No employees found.");
                    Application.Current?.MainPage?.DisplayAlert("Aucun employé trouvé", "Aucun employé trouvé avec les critères de recherche spécifiés.", "OK");
                    return;
                }
                if (employees != null)
                    // Update the filtered employee list
                    PagedFilteredEmployees = new ObservableCollection<EmployeeDTO>(employees);
                IsSearchActive = true;

                // Close the popup
                Popup?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during search: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Error", "An error occurred during the search. Please try again.", "OK");
            }
        }
        private async Task ModifySiteAsync()
        {
            try
            {
                if (SelectedSiteForModification == null || string.IsNullOrWhiteSpace(ModifiedSiteName))
                {
                    Console.WriteLine("Invalid input. Please select a site and enter a new name.");
                    Application.Current?.MainPage?.DisplayAlert("Error", "Please select a site and enter a valid new name.", "OK");
                    return;
                }

                // Update the site
                SelectedSiteForModification.Ville = ModifiedSiteName;

                var isUpdated = await _siteService.UpdateSite(SelectedSiteForModification);

                if (isUpdated)
                {
                    Console.WriteLine($"Site {SelectedSiteForModification.Id} updated successfully.");
                    await LoadSitesAsync(); // Refresh the sites list
                    ModifiedSiteName = string.Empty; // Clear the input field
                    Popup?.Close();
                    await LoadEmployeesAsync(); // Refresh the employee list
                    SelectedSiteForModification = null; // Clear the selected site
                }
                else
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "Failed to update the site. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating site: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Error", "An unexpected error occurred. Please try again.", "OK");
            }
        }
        private async Task RemoveSiteIfPossible(int siteId)
        {
            var res = await _siteService.DeleteSite(siteId);
            if (res)
            {
                Console.WriteLine($"Site with ID {siteId} deleted successfully.");
                await LoadSitesAsync();
                await Task.CompletedTask;
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Erreur", "Le site ne peut pas être supprimé car il est utilisé", "OK");
            }
        }

        private async Task<bool> RemoveEmployeeIfPossible()
        {
            if (SelectedEmployeeForModification == null)
            {
                Console.WriteLine("SelectedEmployeeForModification is null.");
                return false;
            }

            var confirmDelete = await (Application.Current?.MainPage?.DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cet employé ?", "Oui", "Non") ?? Task.FromResult(false));
            if (confirmDelete)
            {
                var res = await _employeeService.DeleteEmployee(SelectedEmployeeForModification.Id);
                if (res)
                {
                    Popup?.Close();
                    await LoadEmployeesAsync();

                    return true;
                }
            }
            return false;
        }

        private async Task ClearSearchAsync()
        {
            IsSearchActive = false;
            await LoadEmployeesAsync(); // Reload the default employee list
        }
        private async Task GoToPreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadEmployeesAsync();
            }
        }
        private async Task GoToNextPage()
        {
            if (CurrentPage < TotalPages)
            {
                try
                {
                    await LoadEmployeesAsync();
                }
                finally
                {
                    CurrentPage++;
                }
            }
        }
        private void UpdateCommandStates()
        {
            ((Command)PreviousPageCommand).ChangeCanExecute();
            ((Command)NextPageCommand).ChangeCanExecute();
        }
        private async Task AddSiteAsync()
        {
            if (string.IsNullOrWhiteSpace(NewSiteName))
            {
                Console.WriteLine("Site name cannot be empty.");
                return; // Stop execution if the input is invalid
            }
            try
            {
                var site = new SiteDTO
                {
                    Ville = NewSiteName
                };

                await _siteService.AddSite(site);
                Console.WriteLine($"Site '{NewSiteName}' added successfully.");

                NewSiteName = string.Empty; // Clear the input field
                await LoadSitesAsync(); // Refresh the site list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding site: {ex.Message}");
            }
        }
        private async Task AddEmployeeAsync()
        {
            try
            {
                // Validate the form inputs
                if (string.IsNullOrWhiteSpace(NewEmployee.Nom) ||
                    string.IsNullOrWhiteSpace(NewEmployee.Prenom) ||
                    string.IsNullOrWhiteSpace(NewEmployee.Email) ||
                    string.IsNullOrWhiteSpace(NewEmployee.TelephonePortable) ||
                    NewEmployee.SiteId == 0 ||
                    NewEmployee.ServiceId == 0)
                {
                    Console.WriteLine("Invalid input. Please fill all required fields.");
                    // Alert 
                    Application.Current?.MainPage?.DisplayAlert("Erreur", "Les données saisies sont invalides. Veuillez remplir tous les champs requis.", "OK");
                    Popup?.Close();
                    return;
                }

                // Send the request to the API
                var addedEmployee = await _employeeService.AddEmployee(NewEmployee);
                if (addedEmployee != null)
                {
                    // Reset the form
                    NewEmployee = new CreateEmployeeDTO
                    {
                        Nom = string.Empty,
                        Prenom = string.Empty,
                        Email = string.Empty,
                        TelephonePortable = string.Empty,
                        TelephoneFixe = string.Empty,
                        SiteId = 0,
                        ServiceId = 0
                    };
                    SelectedService = null;
                    SelectedSite = null;
                    Popup?.Close();
                    // Refresh the employee list
                    await LoadEmployeesAsync();

                }
                else
                {
                    Console.WriteLine("Failed to add employee.");
                    Popup?.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
            }
        }
        private async Task SaveEmployeeAsync()
        {
            try
            {

                if (SelectedEmployeeForModification != null)
                {
                    // Send the request to the API
                    var updatedEmployee = await _employeeService.UpdateEmployee(SelectedEmployeeForModification);

                    if (updatedEmployee)
                    {
                        Console.WriteLine("Employee updated successfully.");

                        // Refresh the employee list
                        await LoadEmployeesAsync();

                        // Close the popup
                        Popup?.Close();
                    }
                    else
                    {
                        Console.WriteLine("Failed to update employee.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
            }
        }

        private async Task AddServiceAsync()
        {
            if (string.IsNullOrWhiteSpace(NewServiceName))
            {
                Console.WriteLine("Service name cannot be empty.");
                return; // Stop execution if the input is invalid
            }

            try
            {
                var service = new ServiceDTO
                {
                    Nom = NewServiceName
                };

                await _serviceService.AddService(service);
                Console.WriteLine($"Service '{NewServiceName}' added successfully.");

                NewServiceName = string.Empty; // Clear the input field
                await LoadServicesAsync(); // Refresh the service list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding service: {ex.Message}");
            }
        }

        private async Task RemoveServiceIfPossible(int serviceId)
        {
            var res = await _serviceService.RemoveService(serviceId);
            if (res == true)
            {
                Console.WriteLine($"Service with ID {serviceId} deleted successfully.");
                await LoadServicesAsync();
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Erreur", "Le service ne peut pas être supprimé car il est utilisé", "OK");
            }
        }
        private async void OnTitleClicked()
        {
            _titleClickCount++;

            if (_titleClickCount == SecretKeyTapThreshold)
            {
                _titleClickCount = 0; // Reset the click count
                string password = await Application.Current.MainPage.DisplayPromptAsync(
                    "Enter Admin Password",
                    "Please enter the admin password to activate admin mode.",
                    "OK",
                    "Cancel",
                    placeholder: "Password",
                    maxLength: 20,
                    keyboard: Keyboard.Text);

                if (password == AdminPassword)
                {
                    IsAdmin = true;
                    await Application.Current.MainPage.DisplayAlert("Admin Mode", "You are now in admin mode!", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Incorrect password. Try again.", "OK");
                }
            }
        }

    }

}
