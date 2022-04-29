﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.PowerApps.TestEngine.PowerApps;
using Microsoft.PowerFx;
using Microsoft.PowerFx.Core.Public.Types;
using Microsoft.PowerFx.Core.Public.Values;

namespace Microsoft.PowerApps.TestEngine.PowerFx.Functions
{
    /// <summary>
    /// This is the same functionality as the Power Apps Select function.
    /// https://docs.microsoft.com/en-us/power-apps/maker/canvas-apps/functions/function-select
    /// </summary>
    public class SelectFunction : ReflectionFunction
    {
        private readonly IPowerAppFunctions _powerAppFunctions;

        public SelectFunction(IPowerAppFunctions powerAppFunctions) : base("Select", FormulaType.Blank, FormulaType.UntypedObject)
        {
            _powerAppFunctions = powerAppFunctions;
        }

        public BlankValue Execute(UntypedObjectValue obj)
        {
            SelectAsync(obj).Wait();

            return FormulaValue.NewBlank();
        }

        private async Task SelectAsync(UntypedObjectValue obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(nameof(obj));
            }

            IUntypedObject untypedVal = obj.Impl;

            var powerAppControlModel = (PowerAppControlModel)untypedVal;
            var result = await _powerAppFunctions.SelectControlAsync(powerAppControlModel.Name, null, null);

            if (!result)
            {
                throw new Exception($"Unable to select control {powerAppControlModel.Name}");
            }

        }
    }
}
