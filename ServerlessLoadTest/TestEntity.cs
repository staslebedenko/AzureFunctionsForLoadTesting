namespace ServerlessLoadTest
{
    public class TestEntity
    {
        public TestEntity(int val1, int val2, int val3)
        {
            this.Value1 = val1.ToString();
            this.Value2 = val2.ToString();
            this.Value3 = val3.ToString();
        }

        private string Value1 { get; }

        private string Value2 { get; }

        private string Value3 { get; }
    }
}
