using CommunityToolkit.Mvvm.Input;
using Dating.Client.Services.Api;
using Dating.Client.Services.Store;
using Dating.Shared.Models;
using Dating.Shared.Models.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.ViewModels
{
    internal class CreateProfileViewModel : ViewModelBase
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                ChangeProperty();
            }
        }
        
        private string _description = "";
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                ChangeProperty();
            }
        }

        private int _age = 0;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                ChangeProperty();
            }
        }

        private GenderVm _selectedGender;
        public GenderVm SelectedGender
        {
            get { return _selectedGender; }
            set 
            { 
                _selectedGender = value; 
                ChangeProperty();
            }
        }

        public ObservableCollection<Guid> PrefferedGenders { get; set; } = new ObservableCollection<Guid>();
        public IList PrefferedGendersSet
        {
            get { return PrefferedGenders; }
            set
            {
                PrefferedGenders.Clear();
                foreach (GenderVm vm in value)
                {
                    PrefferedGenders.Add(vm.Id);
                }
            }
        }

        private ObservableCollection<GenderVm> _genders = new ObservableCollection<GenderVm>();
        public ObservableCollection<GenderVm> Genders
        {
            get { return _genders; }
            set
            {
                _genders = value;
                ChangeProperty();
            }
        }

        public IAsyncRelayCommand CreateProfile { get; private set; }
        public IAsyncRelayCommand LoadData { get; private set; }

        public event Action? OnProfileCreatedEvent;


        public CreateProfileViewModel(IProfileApiService profileApiService, ProfileStore profileStore, IGendersApiService gendersApiService)
        {
            CreateProfile = new AsyncRelayCommand(async (cancellationToken) =>
            {
                await profileApiService.CreateProfileAsync(new CreateProfileVm
                {
                    Name = Name,
                    Age= Age,
                    Description= Description,
                    GenderId = SelectedGender.Id,
                    PreferredGenders = PrefferedGenders
                }, cancellationToken);

                var profile = await profileApiService.GetProfileAsync();
                profileStore.Profile = profile!;
                OnProfileCreatedEvent?.Invoke();
            });
            LoadData = new AsyncRelayCommand(async () =>
            {
                var genders = await gendersApiService.GetGendersAsync();
                foreach (var gender in genders)
                {
                    Genders.Add(gender);
                }
            });


        }

    }
}
