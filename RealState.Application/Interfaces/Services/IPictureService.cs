using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.Pictures;

namespace RealState.Application.Interfaces.Services
{
    public interface IPictureService
    {
        Task<Result<PicturesSaveViewModel>> AddPictures(PicturesSaveViewModel vm);
    }
}