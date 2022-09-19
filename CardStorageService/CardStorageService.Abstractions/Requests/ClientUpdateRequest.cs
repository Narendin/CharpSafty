namespace CardStorageService.Abstractions.Requests
{
    public class ClientUpdateRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
    }
}