using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = DocGen.Test.CSharpCodeFixVerifier<
    DocGen.DocGenAnalyzer,
    DocGen.DocGenCodeFixProvider>;

namespace DocGen.Test
{
    [TestClass]
    public class DocGenUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task TestAnalyze()
        {
            var test = @"
public class {|#0:TypeName|}
{
    public static void Test()
    {
    }
}";
            var expected = VerifyCS.Diagnostic("DocGenAnalyzer").WithLocation(0).WithArguments("TypeName");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
        }

        [TestMethod]
        public async Task TestFix()
        {
            var test = @"public class TypeName
{
    public static void Test()
    {
    }
}";

            var fixtest = @"/// <summary>
/// Type Name
/// </summary>
public class TypeName
{
    public static void Test()
    {
    }
}";
            await VerifyCS.VerifyCodeFixAsync(test, fixtest);
        }
    }
}
