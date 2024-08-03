using AutoMapper;

using RealState.Application.Enums;
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
            if (vm.Count == 0)
            {
                return "You must have at least 1 image.";
            }
            if (vm.Count > 4)
            {
                return "You cannot add more than 4 images.";
            }
            foreach (var picture in vm)
            {
                var pictureEntity = _mapper.Map<Pictures>(picture);
                await _pictureRepository.Create(pictureEntity);
            }

            return vm;
        }

        public async Task<Result<List<PicturesViewModel>>> GetAllByPropertyId(Guid propertyId)
        {
            var pictures = await _pictureRepository.GetAllByPropertyId(propertyId);
            return _mapper.Map<List<PicturesViewModel>>(pictures);
        }

        public async Task UpdatePicturesByPropertyId(List<PicturesSaveViewModel> vm, Guid propertyId)
        {
            var pictures = await _pictureRepository.GetAllByPropertyId(propertyId);

            foreach (var picture in pictures)
            {
                await _pictureRepository.Delete(picture.Id);
            }
            await AddPictures(vm);

        }

        public async Task DeleteByPropertyId(Guid propertyId)
        {
            var pictures = await _pictureRepository.GetAllByPropertyId(propertyId);

            foreach (var picture in pictures)
            {
                await _pictureRepository.Delete(picture.Id);
            }
        }
    }
}
