﻿using ShareBook.Helper.Extensions;
using ShareBook.Helper.Image;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShareBook.Test.Unit.Helpers
{
    public class HelperTests
    {
        [Fact]
        public void GenerateSlugValid()
        {
            var phrase = "Harry Potter and the Philosopher's Stone";
          
            var actual = phrase.GenerateSlug();
            var expected = "harry-potter-and-the-philosophers-stone";

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GenerateSlugInvalid()
        {
            var phrase = "Harry Potter and the Philosopher's Stone";

            var actual = phrase.GenerateSlug();
            var expected = "Harry-potter-and-thE-philosophers-stone";

            Assert.NotEqual(actual, expected);
        }

        [Fact]
        public void FormatImageValid()
        {

            var imageName = "image-name.png";

            var title = "Harry Potter and the Philosopher's Stone";

            var expected = "harry-potter-and-the-philosophers-stone.png";

            var actual = ImageHelper.FormatImageName(imageName, title);

            Assert.Equal(actual, expected);
        }
    }
}
