// Copyright (c) Microsoft.  All Rights Reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using SarifWeb.PageObjectModels;
using SarifWeb.TestUtilities;
using Xunit;

namespace SarifWeb.UITests
{
    public class ValidationPageTests : PageTestBase
    {
        [Fact]
        [Trait(TestTraits.Category, TestCategories.Smoke)]
        [Trait(TestTraits.Category, TestCategories.UITest)]
        public void ValidationPage_WhenFileIsDropped_ShouldDisplayExpectedResults()
        {
            const string TestFilePath = @"TestData\Test.sarif";

            var page = new ValidationPage(Driver);
            page.NavigateTo();

            // Page initially shows no results.
            page.NumResults.Should().Be(0);
            page.CurrentResultIndex.Should().Be(0);

            page.DropFile(TestFilePath);
            page.WaitForRuleListEnabled();

            // The following explicit counts should be obtained by running the Multitool library's
            // ValidateCommand against the test file.
            //
            // The counts are also wrong, because of https://github.com/microsoft/sarif-website/issues/119,
            // "Online validator doesn't report all results". When we fix that bug, this test will
            // "break" and we'll need to fix it.
            page.NumResults.Should().Be(0);
            page.CurrentResultIndex.Should().Be(0);

            page.ClickAdditionalSuggestions();

            page.NumResults.Should().Be(0);
            page.CurrentResultIndex.Should().Be(0);

            page.ClickGitHubRules();

            page.NumResults.Should().Be(2);
            page.CurrentResultIndex.Should().Be(1);

            page.ClickAdditionalSuggestions();

            page.NumResults.Should().Be(2);
            page.CurrentResultIndex.Should().Be(1);
        }
    }
}
