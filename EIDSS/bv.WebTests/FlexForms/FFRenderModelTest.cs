using bv.common.Configuration;
using bv.model.BLToolkit;
using bv.webclient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eidss.model.Schema;

namespace bv.WebTests.FlexForms
{
    
    
    /// <summary>
    ///This is a test class for FFRenderModelTest and is intended
    ///to contain all FFRenderModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class FFRenderModelTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            DbManagerFactory.SetSqlFactory(Config.GetSetting("EidssConnectionString", ""));
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idfsFormTemplate"></param>
        /// <returns></returns>
        private Template GetTemplate(long idfsFormTemplate)
        {
            Template result;
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var acc = Template.Accessor.Instance(null);
                result = acc.SelectByKey(manager, idfsFormTemplate, null);
            }

            return result;
        }

        [TestMethod]
        public void SetTemplateTest()
        {
            var target = new FFPresenterModel();
            target.SetTemplate(GetTemplate(249540000000));
            Assert.IsNotNull(target.TemplateFlexNode);
        }

    }
}
