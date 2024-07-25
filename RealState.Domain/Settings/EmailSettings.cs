﻿namespace RealState.Domain.Settings
{
    public class EmailSettings
    {
        public string EmailFrom { get; set; } = null!;

        public string SmtpHost { get; set; } = null!;

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; } = null!;

        public string SmtpPassword { get; set; } = null!;

        public string DisplayName { get; set; } = null!;
    }
}
