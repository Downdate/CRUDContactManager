namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MyMath mm = new MyMath();

            int a = 5;
            int b = 9;

            int expected = 14;

            int actual = mm.Add(a, b);

            Assert.Equal(expected, actual);
        }
    }
}