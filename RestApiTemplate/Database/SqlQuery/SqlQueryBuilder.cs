using System.Text.Json;

namespace RestApiTemplate.Database.SqlQuery
{
    /*
     Ovu klasu korisitmo da sredimo upite koji se nalaze u nekom jsonu, tako sto prosledimo 
    Dicitionary sa parametrima i njihovim vrednostima i dobjiamo sredjen upit
     
     */
    public class SqlQueryBuilder
    {
        private readonly string _filePath;

        public SqlQueryBuilder(IConfiguration configuration)
        {
            _filePath = configuration["sqlFilePath"];
        }

        public Dictionary<string, SqlCommandDefinition> LoadSqlDefinition()
        {
            var jsonString = File.ReadAllText(_filePath);
            try
            {
                var sqlDefinitions = JsonSerializer.Deserialize<Dictionary<string, SqlCommandDefinition>>(jsonString);
                return sqlDefinitions;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserializacija nije uspela: {ex.Message}");
            }

            return null;
        }
        public string CreateSqlQuery(SqlCommandDefinition sqlCommand, List<Parameter> parameters)
        {

            string query = sqlCommand.CommandText;

            string value = null;

            foreach (var parameter in parameters)
            {
                switch (parameter.Type)
                {
                    case 1:
                        value = parameter.Value.ToString();
                        break;
                    case 2:
                        value = "'"+parameter.Value.ToString()+ "'";
                        break;
                    default:
                        value = parameter.Value.ToString();
                        break;
                }

                query = query.Replace(parameter.Key, value);
                
            }

            return query;
        }
    }
}
