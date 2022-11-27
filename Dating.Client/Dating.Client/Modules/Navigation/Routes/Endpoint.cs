﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Modules.Navigation.Routes
{
    internal class Endpoint
    {
        private event Action? _listeners;

        public void AddListener(Action listener)
        {
            listener += _listeners;
        }

        public void Navigate()
        {
            _listeners?.Invoke();
        }
    }
}
