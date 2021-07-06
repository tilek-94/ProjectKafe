﻿using CV19.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;
        //TEst
        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
