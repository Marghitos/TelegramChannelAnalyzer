using CsvHelper.Configuration.Attributes;

namespace TelegramChannelAnalyzer
{
    public class CsvOutput
    {
        [Format("yyyy-MM-ddTHH:mm:ss")]
        public string MessageDate { get; set; }
        public string Message { get; set; }
        public int Fire { get; set; }
        public int Clap { get; set; }
        public int Party { get; set; }
        public int Approve { get; set; }
        public int Love { get; set; }
        public int SmilingWithHearth { get; set; }
        public int Thinking { get; set; }
        public int Crying { get; set; }
        public int Beaming { get; set; }
        public int Horrified { get; set; }
        public int Shit { get; set; }
        public int StarStruck { get; set; }
        public int Angry { get; set; }
        public int Dislike { get; set; }
        public int Vomiting { get; set; }
        public int ExplodingHead { get; set; }
        public int Views { get; set; }
    }
}
