using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestApiTemplate.BussinesLogic.Interface;
using RestApiTemplate.CommandResponse;
using RestApiTemplate.Controllers;
using RestApiTemplate.Database;
using RestApiTemplate.Database.SqlQuery;
using RestApiTemplate.Models.Domain;
using RestApiTemplate.Models.DTO;


namespace RestApiTemplate.BussinesLogic
{
    public class RestApiTemplateBussinesLogic : IRestApiTemplateBussinesLogic
    {
        private readonly RestApiTemplateDbContext _dbContext;
        private readonly ILogger<FakultetController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configration;


        public RestApiTemplateBussinesLogic(RestApiTemplateDbContext dbContext, ILogger<FakultetController> logger, IMapper mapper, IConfiguration configration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _configration = configration;
        }

        /**
         FAKULTET
         */

        public async Task<List<Fakultet>> GetAllFaculties()
        {
            List<Fakultet> fakultets = null;
            try
            {
                 fakultets =  _dbContext.Fakultet.ToList();
                _logger.LogInformation("Response metode GetInformtionAboutFakultet {@fakultet}", fakultets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Desila se greska pri izvrsavanju metode GetInformtionAboutFakultet {@error}", ex.StackTrace);
            }
            return fakultets;
        }
        
        public async Task<CommandResponse<Fakultet>> InsertNewFakultet(AddNewFakultet fakultet)
        {
            CommandResponse<Fakultet> response = new CommandResponse<Fakultet>(200,null,null);

            Fakultet newFakultet = _mapper.Map<Fakultet>(fakultet);
            newFakultet.FakultetId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            try
            {
                if (fakultet != null)
                {
                    await _dbContext.Fakultet.AddAsync(newFakultet);
                    await _dbContext.SaveChangesAsync();
                    response.Body = newFakultet;
                    response.Message = "Uspesno dodat fakultet";
                    
                    
                }
            }
            catch (Exception ex)
            {
                
                _logger.LogError("Desila se greska u metodi InsertNewFakultet {@error}",ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi InsertNewFakultet";
                response.Body = null;
            }

            return response;
        }

        public async Task<CommandResponse<Fakultet>> GetFakultetById(long id)
        {
            CommandResponse<Fakultet> response = new CommandResponse<Fakultet>(200, null, null);

            

            try
            {
                SqlQueryBuilder loader = new SqlQueryBuilder(_configration);
                var sqlDefinitions = loader.LoadSqlDefinition();

                if(sqlDefinitions.TryGetValue("SelectFakultetById", out SqlCommandDefinition sqlQuery))
{
                    var parameters = new List<Database.SqlQuery.Parameter>
                    {
                        new Database.SqlQuery.Parameter("@fakultetId", 1, id)
                    };

                    string sqlValue = loader.CreateSqlQuery(sqlQuery, parameters);
                



                var fakultet =  _dbContext.Fakultet.FromSqlRaw(sqlValue).FirstOrDefault();

                if (fakultet == null)
                {
                    response.Message = "Ne postoji fakultet za zadati ID";
                    response.Body = fakultet;
                }
                else
                {
                    response.Message = "Fakulte pronadjen";
                    response.Body = fakultet;
                }

                }

            }
            catch (Exception ex)
            {

                _logger.LogError("Desila se greska u metodi GetFakultetById {@error}", ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi GetFakultetById";
                response.Body = null;
            }

            return response;
        }

        public async Task<CommandResponse<Fakultet>> DeleteFakultetById(long id)
        {
            CommandResponse<Fakultet> response = new CommandResponse<Fakultet>(200, null, null);



            try
            {

                Fakultet fakultet = await _dbContext.Fakultet.FindAsync(id);
                if (fakultet == null)
                {
                     response.Message = "Fakultet nije izbrisan"; ; // Ako nije pronađen, vrati false
                     return response;
                }
                else
                {
                     _dbContext.Fakultet.Remove(fakultet);
                    await _dbContext.SaveChangesAsync();
                }


                response.Message = "Fakultet uspeno izbrisan!";



            }
            catch (Exception ex)
            {

                _logger.LogError("Desila se greska u metodi DeleteFakultetById {@error}", ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi DeleteFakultetById";
                response.Body = null;
            }

            return response;
        }

        public async Task<CommandResponse<Fakultet>> UpdateFakultet(long id, UpdateFakultet fakultet)
        {
            CommandResponse<Fakultet> response = new CommandResponse<Fakultet>(200, null, null);

            try
            {
                Fakultet fakultetForUpdate = _dbContext.Fakultet.Find(id);
                if (fakultetForUpdate == null)
                {
                    response.Message = "Fakultet nije pronadjen"; ; // Ako nije pronađen, vrati false
                    return response;
                }
                else
                {
                    fakultetForUpdate.Opis = fakultet.Opis;
                    fakultetForUpdate.Adresa = fakultet.Adresa;
                    fakultetForUpdate.Ime = fakultet.Ime;
                        

                    await _dbContext.SaveChangesAsync();
                }

                response.Message = "Fakultet uspesno updateovan"; ; // Ako nije pronađen, vrati false

            }
            catch (Exception ex)
            {

                _logger.LogError("Desila se greska u metodi UpdateFakultet {@error}", ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi UpdateFakultet";
                response.Body = null;
            }

            return response;
        }



        /*
         MESTO
         */

        public async Task<CommandResponse<Mesto>> InsertNewMesto(Mesto mesto)
        {
            CommandResponse<Mesto> response = new CommandResponse<Mesto>(200, null, null);


            try
            {
                if (mesto != null && mesto.PostanskiBroj!=0)
                {
                    await _dbContext.Mesto.AddAsync(mesto);
                    await _dbContext.SaveChangesAsync();
                    response.Body = mesto;
                    response.Message = "Uspesno dodato mesto";


                }
                else
                {
                    response.Message = "Request nije validan";
                    response.Body = mesto;
                    response.StatusCode = 500;  

                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Desila se greska u metodi InsertNewMesto {@error}", ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi InsertNewMesto";
                response.Body = null;
            }

            return response;
        }
        public async Task<List< Mesto>> GetAllMesta()
        {
            List<Mesto> mesta = null;
            try
            {
                 mesta = _dbContext.Mesto.ToList();
                _logger.LogInformation("Response metode GetAllMesta {@fakultet}", mesta);
            }
            catch (Exception ex)
            {
                _logger.LogError("Desila se greska pri izvrsavanju metode GetAllMesta {@error}", ex.StackTrace);
            }
            return mesta;
        }

        public async Task<CommandResponse<Mesto>> GetMestoByName(string name)
        {
            CommandResponse<Mesto> response = new CommandResponse<Mesto>(200, null, null);
            try
            {


                SqlQueryBuilder loader = new SqlQueryBuilder(_configration);
                var sqlDefinitions = loader.LoadSqlDefinition();

                if (sqlDefinitions.TryGetValue("SelectMestotByName", out SqlCommandDefinition sqlQuery))
                {
                    var parameters = new List<Database.SqlQuery.Parameter>
                    {
                        new Database.SqlQuery.Parameter("@nazivMesta", 2, name)
                    };

                    string sqlValue = loader.CreateSqlQuery(sqlQuery, parameters);


                    

                var mesto = _dbContext.Mesto.FromSqlRaw(sqlValue).FirstOrDefault();

                if (mesto != null)
                {
                    response.Message = "Mesto uspesno pronadjeno";
                    response.Body = mesto;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Message = "Ne postoji mesto za zadatim nazivom";
                    response.Body = null;
                    response.StatusCode = 500;
                }
            }
            }

            catch (Exception ex)
            {

                _logger.LogError("Desila se greska u metodi GetMestoByName {@error}", ex.StackTrace);
                response.StatusCode = 500;
                response.Message = "Desila se greska u metodi GetMestoByName";
                response.Body = null;
                return response;
            }
            return response;
        }

    }
}
