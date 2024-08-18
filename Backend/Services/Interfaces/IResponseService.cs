using Backend.Model.DTOs;

namespace Backend.Services.Interfaces
{
    public interface IResponseService
    {
        Task SaveResponseAsync(ResponseDto responseDto);
    }
    
}
