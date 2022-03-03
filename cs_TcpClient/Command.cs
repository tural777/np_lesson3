﻿namespace cs_TcpClient
{
    class Command
    {
        public const string PROCLIST = "proclist";
        public const string KILL = "kill";
        public const string RUN = "run";
        public const string HELP = "help";
        public string Text { get; set; }
        public string Param { get; set; }
    }
}
