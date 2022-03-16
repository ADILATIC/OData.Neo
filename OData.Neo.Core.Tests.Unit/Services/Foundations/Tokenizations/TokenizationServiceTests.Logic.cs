﻿//-----------------------------------------------------------------------
// Copyright (c) .NET Foundation and Contributors. All rights reserved.
// See License.txt in the project root for license information.
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OData.Neo.Core.Models;
using Xunit;


namespace OData.Neo.Core.Tests.Unit.Services.Foundations.Tokenizations
{
    public partial class TokenizationServiceTests
    {
        [Theory]
        [MemberData(nameof(OperandTokens))]
        [MemberData(nameof(SpecialTokens))]
        [MemberData(nameof(ComplexTokens))]
        public void ShouldTokenizeQuery(OToken possibleToken)
        {
            // given
            string rawQuery = possibleToken.Value;

            var expectedTokens = new OToken[]
            {
                possibleToken
            };

            // when
            OToken[] actualTokens =
                this.tokenizationService.Tokenize(rawQuery);

            // then
            actualTokens.Should().BeEquivalentTo(expectedTokens);
        }

        [Fact]
        public void ShouldTokenizeMultiTokenQuery()
        {
            // given
            OToken[] randomOTokens = CreateRandomOTokens();
            OToken[] expectedTokens = randomOTokens;

            var allTokenValues =
                randomOTokens.Select(token => token.Value);

            string inputQuery =
                String.Join(separator: null, values: allTokenValues);

            // when
            OToken[] actualTokens =
                this.tokenizationService.Tokenize(inputQuery);

            // then
            actualTokens.Should().BeEquivalentTo(expectedTokens);
        }
    }
}