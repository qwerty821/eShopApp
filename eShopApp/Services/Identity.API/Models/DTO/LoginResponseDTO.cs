using System.Data;

namespace Identity.API.Models.DTO
{
    public class LoginResponseDTO
    {
        public int code { get; set; }
        public string message { get; set; }

        public Dictionary<string, string> data;



        //var response = new
        //{
        //    code = 0,
        //    message = "ok",
        //    data = new
        //    {
        //        accessToken = tokenStr,
        //        realName = $"{user.Firstname} {user.Lastname}",
        //        username = user.Email,
        //        roles = roles
        //    }
        //};
    }
}
