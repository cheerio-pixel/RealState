using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.Pictures;

namespace RealState.Application.Interfaces.Services
{
    public interface IPictureService
    {
        Task<Result<List<PicturesSaveViewModel>>> AddPictures(List<PicturesSaveViewModel> vm);
        Task<Result<List<PicturesViewModel>>> GetAllByPropertyId(Guid propertyId);
        Task UpdatePicturesByPropertyId(List<PicturesSaveViewModel> vm, Guid propertyId);
        Task DeleteByPropertyId(Guid propertyId);
    }
}