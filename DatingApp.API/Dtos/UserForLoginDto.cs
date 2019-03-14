namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        //properties
        public string Username { get; set; }

        public string Password { get; set; }//API compare computed hash of thos password with the hash from DB
    }
}