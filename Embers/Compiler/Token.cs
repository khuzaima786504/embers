namespace Embers.Compiler
{
    public class Token(TokenType type, string value)
    {
        private readonly string value = value;
        private readonly TokenType type = type;

        public string Value { get { return value; } }

        public TokenType Type { get { return type; } }
    }
}
