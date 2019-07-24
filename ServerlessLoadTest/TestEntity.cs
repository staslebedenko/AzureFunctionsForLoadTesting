namespace ServerlessLoadTest
{
    public class TestEntity
    {
        public TestEntity(int val1, int val2, int val3)
        {
            Value1 = val1.ToString();
            Value2 = val2.ToString();
            Value3 = val3.ToString();
        }
        private string Value1 { get; }
        private string Value2 { get; }
        private string Value3 { get; }
    }
}
