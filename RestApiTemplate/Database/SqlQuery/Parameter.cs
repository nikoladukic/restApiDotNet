namespace RestApiTemplate.Database.SqlQuery
{
    public class Parameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public int Type { get; set; }

        public Parameter(string key, int type, object value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }
}
