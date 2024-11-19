namespace Interview_Server.Services
{
    public class InterviewValidationService
    {
        public bool ValidateTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length >= 3 && title.Length <= 50;
        }

        public bool ValidateDescription(string description)
        {
            return !string.IsNullOrWhiteSpace(description) && description.Length >= 5 && description.Length <= 500;
        }

        public bool ValidateTime(DateTime? time)
        {
            return time.HasValue && time.Value > DateTime.Now;
        }

        public bool ValidateAddress(string address)
        {
            return !string.IsNullOrWhiteSpace(address) && address.Length >= 5 && address.Length <= 100;
        }

        public bool ValidateDuration(int duration)
        {
            return duration > 0 && duration <= 300;
        }

        public bool ValidateCompanyName(string companyName)
        {
            return !string.IsNullOrWhiteSpace(companyName) && companyName.Length >= 3 && companyName.Length <= 50;
        }
    }
}



