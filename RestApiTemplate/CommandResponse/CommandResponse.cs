namespace RestApiTemplate.CommandResponse
{
    public class CommandResponse<T>
    {
        public int StatusCode { get; set; } = 200;  // HTTP status kod ili neki drugi kod statusa
        public string Message { get; set; }  // Poruka koja opisuje status
        public T Body { get; set; }  // Telo odgovora koje može biti bilo koji tip klase

        public CommandResponse(int statusCode, string message, T body)
        {
            StatusCode = statusCode;
            Message = message;
            Body = body;
        }
    }
}
