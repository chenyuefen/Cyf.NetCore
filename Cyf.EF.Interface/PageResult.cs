﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cyf.EntityFramework.Interface
{
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> DataList { get; set; }
    }
}