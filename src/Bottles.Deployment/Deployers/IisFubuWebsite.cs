﻿namespace Bottles.Deployment.Deployers
{
    public class IisFubuWebsite : IDirective
    {

        public IisFubuWebsite()
        {
            Port = 80;
            DirectoryBrowsing = Activation.Disable;
            AnonAuth = Activation.Enable;
            BasicAuth = Activation.Disable;
            WindowsAuth = Activation.Disable;
        }

        public string WebsiteName { get; set; }
        public string WebsitePhysicalPath { get; set; }
        public string VDir { get; set; }
        public string VDirPhysicalPath { get; set; }
        public string AppPool { get; set; }
        public int Port { get; set; }


        //credentials
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HasCredentials()
        {
            return !string.IsNullOrEmpty(Username);
        }


        //host bottle?
        public string HostBottle { get; set; }
        public string[] Bottles { get; set; }


        //iis options
        public Activation DirectoryBrowsing { get; set; }
        public Activation AnonAuth { get; set; }
        public Activation BasicAuth { get; set; }
        public Activation WindowsAuth { get; set; }
    }
}