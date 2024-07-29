using AutoMapper;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Pictures;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class PictureService(IPictureRepository pictureRepository, IMapper mapper) : GenericService<PicturesSaveViewModel, PicturesViewModel, Pictures>(pictureRepository, mapper), IPictureService
    {
        private readonly IPictureRepository _pictureRepository = pictureRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<List<PicturesSaveViewModel>>> AddPictures(List<PicturesSaveViewModel> vm)
        {
            var task = vm.Select(async picture =>
            {
                var pictureEntity = _mapper.Map<Pictures>(picture);
                await _pictureRepository.Create(pictureEntity);
            });

            await Task.WhenAll(task);
            return vm;
        }
    }
}
