﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Actions
{
    public interface IRemoveRepository<T>
    {
        void Remove(T id);
    }
}
