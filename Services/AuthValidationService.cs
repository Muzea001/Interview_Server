using System.Text.RegularExpressions;

namespace Interview_Server.Authentication
{
    public class AuthValidationService
    {
        public bool ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            return false;

            var regex = new Regex(@"^[a-zA-Z0-9]{3,15}$");
            return regex.IsMatch(userName);
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            return false;

                var regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            return false;

            var regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            return regex.IsMatch(email);
        }

        public bool ValidateMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            return false;

            var regex = new Regex(@"^\d{8}$");
            return regex.IsMatch(mobile);
        }
    }
}
