using Blog.Common.Dtos;

namespace Blog.Services.Api.Extensions
{
    public static class ParseExtension
    {
        public static UserDto ParsToModel(this Data.Entities.User user)
        {
            return new UserDto()
            {
                id = user.id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                CreatedAt = user.CreatedAt,
                PhotoUrl = user.PhotoUrl
            };
        }

        public static List<UserDto> ParsToModels(this List<Data.Entities.User>? users)
        {
            if (users == null || users.Count == 0) return new List<UserDto>();
            var dtos = new List<UserDto>();
            foreach (var user in users)
            {

                dtos.Add(user.ParsToModel());
            }
            return dtos;
        }
    }
}
