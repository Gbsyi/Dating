using CommunityToolkit.Mvvm.Input;
using Dating.Client.Models;
using Dating.Client.Services.Api;
using Dating.Shared.Models.Pair;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<PairVm> Pairs { get; set; } = new ObservableCollection<PairVm>();
        public IList PairsSet
        {
            get { return Pairs; }
            set
            {
                Pairs.Clear();
                foreach (PairVm vm in value)
                {
                    Pairs.Add(vm);
                }
            }
        }

        public IAsyncRelayCommand Like { get; init; }
        public IAsyncRelayCommand Dislike { get; init; }

        private IPairService _pairService = null!;
        private IPictureService _pictureService = null!;
        public HomeWindowViewModel()
        {
            Like = new AsyncRelayCommand(async (cancellationToken) =>
            {
                if (_pair is null)
                {
                    return;
                }

                await _pairService.LikeProfileAsync(new LikePairVm
                {
                    LikedProfileId = _pair.UserId
                }, cancellationToken);
                await LoadNextPair(cancellationToken);
                await LoadPairs(cancellationToken);
            });

            Dislike = new AsyncRelayCommand(async (cancellationToken) =>
            {
                if (_pair is null)
                {
                    return;
                }

                await _pairService.LikeProfileAsync(new LikePairVm
                {
                    LikedProfileId = _pair.UserId
                }, cancellationToken);
                await LoadNextPair(cancellationToken);
                await LoadPairs(cancellationToken);
            });
        }

        public void InitDeps(IPairService pairService, IPictureService pictureService)
        {
            _pairService = pairService;
            _pictureService = pictureService;
        }
        public async Task LoadNextPair(CancellationToken cancellationToken = default)
        {
            var pair = await _pairService.GetNextPairAsync(cancellationToken);
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

        public async Task LoadPairs(CancellationToken cancellationToken = default)
        {
            var apiPairs = await _pairService.GetUserPairsAsync(cancellationToken);
            var pairs = apiPairs.Select(x => new UserPairVm
            {
                ChatId = x.ChatId,
                Name = x.Name,
                UserId = x.UserId,  
                PicturePath = new Uri($"{AppConstants.BaseUrl}/picture/{x.PictureId}")
            });
        }
    }
}
