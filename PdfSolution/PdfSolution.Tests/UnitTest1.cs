using System.Xml;

namespace PdfSolution.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        //[Fact]
        //public void FailingTest()
        //{
        //    Assert.Equal(5, Add(2, 2));
        //}

        [Fact]
        public void TestXml()
        {
            string expectedValue = "Margaret";
            string actualValue = string.Empty;  
            var doc = new XmlDocument();
            doc.Load("bookstore.xml");
            //XmlNode? root = doc.DocumentElement;

            //if (root != null)
            //{
                XmlNode? node = doc.SelectSingleNode("bookstore/book/author/first-name");
                
                if (node != null)
                {
                    actualValue = node.InnerText;
                }

                Assert.Equal(expectedValue, actualValue);
            //}
        }

        private static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
