﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSolution.Core
{
    public class TestCaseEqual(int pageNumber,int lineIndex,int beginCharacterIndex,int endCharacterIndex,string expectedText) : TestCaseBase(pageNumber)
    {
        public int LineIndex { get; private set; }=lineIndex;
        public int BeginCharacterIndex { get; set; }=beginCharacterIndex;
        public int EndCharacterIndex { get; internal set; }=endCharacterIndex;
        public string ExpectedText { get; private set; }=expectedText;
    }
}
