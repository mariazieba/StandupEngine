namespace StandupEngine.Shared
{
    public class PromptObject
    {
        public string[] intro { get; set; }
        public string[] loading { get; set; }
        public string[] added { get; set; }
        public Modifier[] modifiers { get; set; }
    }

    public class Modifier
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}
