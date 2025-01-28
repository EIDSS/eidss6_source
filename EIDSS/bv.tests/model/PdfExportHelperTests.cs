using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using eidss.model.Helpers;

namespace bv.tests.model
{
    [TestClass]
    public class PdfExportHelperTests
    {
        private const string NotUsed = "";

        List<PdfExportHelper.CaptionString> _baseCaptions = new List<PdfExportHelper.CaptionString>(new PdfExportHelper.CaptionString[]
            {
                new PdfExportHelper.CaptionString("No return to normal for foreseeable future", false, NotUsed),
                new PdfExportHelper.CaptionString("Live updates", false, NotUsed),
                new PdfExportHelper.CaptionString("All virus stories", false, NotUsed),
                new PdfExportHelper.CaptionString("Manchester City", false, NotUsed)
            });

        [TestMethod]
        public void NoColumnsTestCase()
        {
            PdfExportHelper testObject = new PdfExportHelper(new PdfExportHelper.AllCaptionsRetValue(new PdfExportHelper.CaptionString[] { }, 2), new Font("Arial", 12));

            Assert.IsFalse(testObject.ShouldCaptionBeWordWrapped);
        }

        [TestMethod]
        public void NoWordWrappingPositiveTestCase()
        {
            PdfExportHelper testObject = new PdfExportHelper(new PdfExportHelper.AllCaptionsRetValue(_baseCaptions.ToArray(), 2), new Font("Arial", 12));

            Assert.IsFalse(testObject.ShouldCaptionBeWordWrapped);
        }

        [TestMethod]
        public void WordWrappingLowCornerTestCase()
        {
            List<PdfExportHelper.CaptionString> captions = new List<PdfExportHelper.CaptionString>(_baseCaptions.ToArray());

            captions.Add(new PdfExportHelper.CaptionString("ABC", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Rising US numbers could just", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString(" ", false, NotUsed));

            PdfExportHelper testObject = new PdfExportHelper(new PdfExportHelper.AllCaptionsRetValue(captions.ToArray(), 2), new Font("Arial", 12));

            Assert.IsTrue(testObject.ShouldCaptionBeWordWrapped);
        }

        [TestMethod]
        public void WordWrappingHighCornerTestCase()
        {
            List<PdfExportHelper.CaptionString> captions = new List<PdfExportHelper.CaptionString>(_baseCaptions.ToArray());

            captions.Add(new PdfExportHelper.CaptionString("ABC", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Rising US numbers could just be the tip of the iceberg", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Covid-19 immunity from antibodies may last only months: study", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("123456789", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Florida now has more cases than most countries in the world", false, NotUsed));

            PdfExportHelper testObject = new PdfExportHelper(new PdfExportHelper.AllCaptionsRetValue(captions.ToArray(), 2), new Font("Arial", 12));

            Assert.IsTrue(testObject.ShouldCaptionBeWordWrapped);
        }

        [TestMethod]
        public void NoWordWrappingNegativeTestCase()
        {
            List<PdfExportHelper.CaptionString> captions = new List<PdfExportHelper.CaptionString>(_baseCaptions.ToArray());

            captions.Add(new PdfExportHelper.CaptionString("ABC", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Rising US numbers could just be the tip of the iceberg", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Covid-19 immunity from antibodies may last only months: study", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("123456789", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("Florida now has more cases than most countries in the world", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("123456789ABCDEFGH", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("123456789ABCDEFGH", false, NotUsed));
            captions.Add(new PdfExportHelper.CaptionString("123456789ABCDEFGH", false, NotUsed));

            PdfExportHelper testObject = new PdfExportHelper(new PdfExportHelper.AllCaptionsRetValue(captions.ToArray(), 2), new Font("Arial", 12));

            Assert.IsFalse(testObject.ShouldCaptionBeWordWrapped);
        }
    }
}
