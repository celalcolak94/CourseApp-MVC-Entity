namespace MVCApp.Data
{
    public class Role
    {
        public string Id { get; init; } // sadece constructor içinden set edilebilir.
        public string Name { get; set; }

        //Bu role tanımlanmış olan kullanıcılar
        public List<User> Users { get; set; }

        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}