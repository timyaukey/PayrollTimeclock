using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveUp.Net.Mail;

namespace MailTools
{
    public class IMap4Repository
    {
        private Imap4Client _client = null;

        public IMap4Repository(string mailServer, int port, bool ssl, string login, string password)
        {
            if (ssl)
                Client.ConnectSsl(mailServer, port);
            else
                Client.Connect(mailServer, port);
            Client.LoginFast(login, password);
            //Client.Command("capability");
        }

        public Imap4Client Client
        {
            get
            {
                if (_client == null)
                    _client = new Imap4Client();
                return _client;
            }
        }
    }
}