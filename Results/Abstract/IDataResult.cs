﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_OOP_Practice_V1
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
