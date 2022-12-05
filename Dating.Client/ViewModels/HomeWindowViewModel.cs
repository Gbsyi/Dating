using CommunityToolkit.Mvvm.Input;
using Dating.Client.Services.Api;
using Dating.Shared.Models.Pair;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dating.Client.ViewModels
{
    public class HomeWindowViewModel : ViewModelBase
    {
        private Uri? _picture;
        public Uri? Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                ChangeProperty();
            }
        }

        private bool _loaded = false;
        public bool Loaded
        {
            get { return _loaded; }
            set
            {
                _loaded = value;
                ChangeProperty();
            }
        }

        private bool _hasPairs = false;
        public bool HasPairs
        {
            get { return _hasPairs; }
            set
            {
                _hasPairs = value;
                ChangeProperty();
            }
        }

        private NextPairVm? _pair;
        public NextPairVm? Pair
        {
            get { return _pair; }
            set
            {
                _pair = value;
                ChangeProperty();
            }
        }


        public IAsyncRelayCommand Like { get; init; }

        private IPairService _pairService;
        private IPictureService _pictureService;
        public HomeWindowViewModel()
        {
            Like = new AsyncRelayCommand(async () =>
            {
                throw new Exception();
            });
        }
        public void InitDeps(IPairService pairService, IPictureService pictureService)
        {
            _pairService = pairService;
            _pictureService = pictureService;
        }
        public async Task LoadNextPair()
        {
            var pair = await _pairService.GetNextPairAsync();
            if (pair is null)
            {
                HasPairs = false;
            }
            Pair = pair;

            if (pair is not null)
            {
                Picture = new Uri(@$"{AppConstants.BaseUrl}/picture/{pair.PictureId}");
            }

            Loaded = true;
        }
    }
}
