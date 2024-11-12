using RestApiTemplate.CommandResponse;
using RestApiTemplate.Controllers;
using RestApiTemplate.Models.Domain;
using RestApiTemplate.Models.DTO;

namespace RestApiTemplate.BussinesLogic.Interface
{
    public interface IRestApiTemplateBussinesLogic
    {
        Task<List<Fakultet>> GetAllFaculties();
        Task<CommandResponse<Fakultet>> GetFakultetById(long id);
        Task<CommandResponse<Fakultet>> InsertNewFakultet(AddNewFakultet fakultet);
        Task<CommandResponse<Fakultet>> DeleteFakultetById(long id);
        Task<CommandResponse<Fakultet>> UpdateFakultet(long id, UpdateFakultet fakultet);


        Task<List<Mesto>> GetAllMesta();
        Task<CommandResponse<Mesto>> GetMestoByName(string name);
        Task<CommandResponse<Mesto>> InsertNewMesto(Mesto mesto);

    }
}
