namespace CardStorageService.Abstractions.Requests
{
    public class ClientCreateRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
    }
}