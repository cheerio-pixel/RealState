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
        public async Task<Result<PicturesSaveViewModel>> AddPictures(PicturesSaveViewModel vm)
        {

            var picturesEntity = _mapper.Map<List<Pictures>>(vm.Pictures);
            
            var task = picturesEntity.Select(async picture =>
            {
                picture.PropertyId = vm.PropertyId;
                await _pictureRepository.Create(picture);
            });

            await Task.WhenAll(task);
            return vm;
        }
    }
}
