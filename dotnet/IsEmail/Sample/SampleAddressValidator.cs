using System;
using System.Net.Mail;

namespace IsEmail.Sample
{
    public class SampleAddressValidator
    {
        public bool IsValid(string address)
        {
            try
            {
                // unfortunately I'm not aware of a public
                // validation method, so we have to try/catch
                // ... yay side effects =="
                var emailAddress = new MailAddress(address);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
