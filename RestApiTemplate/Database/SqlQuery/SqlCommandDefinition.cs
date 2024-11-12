namespace RestApiTemplate.Database.SqlQuery
{
    public class SqlCommandDefinition
    {
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
