namespace TelegramDlBot.Core.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class TelegramCommandAttribute(string name, string description) : Attribute
    {
        public string Name { get; } = name;

        public string Description { get; } = description;
    }
}
