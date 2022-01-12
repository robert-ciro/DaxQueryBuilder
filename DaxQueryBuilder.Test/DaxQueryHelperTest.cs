using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using static FluentAssertions.FluentActions;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace DaxQueryBuilder.Test
{
    internal class DaxQueryHelperTest
    {
        [TestCase("")]
        [TestCase(null)]
        public void When_Table_Is_Null_Or_Empty_Throw_Exception(string table)
        {
            Invoking(() => DaxQueryHelper.ApplyFilterIn(table, "Col", "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIn(table, "Col", "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIsEqual(table, "Col", "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIsEqual(table, "Col", "Value1")).Should().Throw<ArgumentException>();
        }

        [TestCase("")]
        [TestCase(null)]
        public void When_Col_NullorEmpty_Throw_Exception(string col)
        {
            Invoking(() => DaxQueryHelper.ApplyFilterIn("Table", col, "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIn("Table", col, "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIsEqual("Table", col, "Value1")).Should().Throw<ArgumentException>();
            Invoking(() => DaxQueryHelper.ApplyFilterIsEqual("Table", col, "Value1")).Should().Throw<ArgumentException>();
        }

        public static IEnumerable<string[]> NullAdnEmptyArray
        {
            get
            {
                yield return null;
                yield return Array.Empty<string>();
            }
        }

        [TestCaseSource(nameof(NullAdnEmptyArray))]
        public void ApplyFilterIn_When_Values_NullOrEmpty_ReturnEmptyString(params string[] values)
        {
            DaxQueryHelper.ApplyFilterIn("Table", "Col", values).Should().BeSameAs("", "Because if you don't provide values there is no need to generate the dax statement.");
        }

        [Test]
        public void ApplyFilterIsEqual_WhenValueNull_ThrowArgumentNullException()
        {
            Invoking(() => DaxQueryHelper.ApplyFilterIsEqual("Table", "Col", null)).Should().Throw<ArgumentNullException>();
        }

        [TestCase("Product", "Name", "Coca cola", "KEEPFILTERS(TREATAS( {\"Coca cola\"}, Product[Name]))")]
        [TestCase("Person", "Surname", "Alex", "KEEPFILTERS(TREATAS( {\"Alex\"}, Person[Surname]))")]
        public void ApplyFilterIsEqual_Return_ExpectedDAXStatement(string table, string col, string value, string expectedStatement)
        {
            string result = DaxQueryHelper.ApplyFilterIsEqual(table, col, value);
            Assert.True(result.Equals(expectedStatement, StringComparison.OrdinalIgnoreCase));
        }


        public static IEnumerable<object> ApplyFilterInCases
        {
            get
            {
                yield return new object[] {
                    "Product",
                    "Name",
                    new[]{ "Coca cola", "Red Bull"},
                    "KEEPFILTERS(TREATAS( {\"Coca cola\", \"Red Bull\"}, Product[Name]))"
                };
                yield return new object[] {
                    "Person",
                    "Surname",
                    new[] { "Alex", "Ramon"},
                    "KEEPFILTERS(TREATAS( {\"Alex\", \"Ramon\"}, Person[Surname]))"
                };
            }
        }

        [TestCaseSource(nameof(ApplyFilterInCases))]

        public void ApplyFilterIn_Return_ExpectedDAXStatement(string table, string col, string[] values, string expectedStatement)
        {
            string result = DaxQueryHelper.ApplyFilterIn(table, col, values);
            Assert.True(result.Equals(expectedStatement, StringComparison.OrdinalIgnoreCase));
        }
    }
}
