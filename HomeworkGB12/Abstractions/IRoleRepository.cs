namespace HomeworkGB12.Abstractions
{
    public interface IRoleRepository
    {
        public int AddRole(string newRole);
        public string? CheckRole(HttpContext httpContext);
    }
}
