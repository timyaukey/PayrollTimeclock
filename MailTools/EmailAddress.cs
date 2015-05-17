using ActiveUp.Net.Mail;

namespace MailTools
{
    public class EmailAddress
    {
        public readonly string Address;
        public readonly string Name;

        public EmailAddress(Address address)
        {
            Address = address.Email;
            Name = address.Name;
        }

        public EmailAddress(string address, string name)
        {
            Address = address;
            Name = name;
        }

        public EmailAddress(string input)
        {
            int indexOfBracket = input.IndexOf('<');
            if (indexOfBracket >= 0)
            {
                int indexOfBracket2 = input.IndexOf('>');
                Address = input.Substring(indexOfBracket + 1, indexOfBracket2 - indexOfBracket - 1);
                Name = input.Substring(0, indexOfBracket).Trim();
            }
            else if (input.Contains("@"))
            {
                Address = input;
                Name = "";
            }
            else
            {
                Address = "";
                Name = input;
            }
        }

        public string PackedFormat
        {
            get
            {
                string result;
                if (string.IsNullOrEmpty(Address))
                    result = Name;
                else if (string.IsNullOrEmpty(Name))
                    result = Address;
                else
                    result = Name + "<" + Address + ">";
                return result;
            }
        }

        public string ShortFormat
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return Name;
                else
                    return Address;
            }
        }

        public override string ToString()
        {
            return ShortFormat;
        }

        public bool IsSame(EmailAddress other)
        {
            return (other.Address == Address || other.Name == Name);
        }
    }
}
