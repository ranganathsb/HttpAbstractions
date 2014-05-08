// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.FeatureModel;
using Microsoft.AspNet.HttpFeature;
using Xunit;

namespace Microsoft.AspNet.Owin
{
    public class OwinHttpEnvironmentTests
    {
        private T Get<T>(IFeatureCollection features)
        {
            object value;
            return features.TryGetValue(typeof(T), out value) ? (T)value : default(T);
        }

        [Fact]
        public void OwinHttpEnvironmentCanBeCreated()
        {
            var env = new Dictionary<string, object>
            {
                {"owin.RequestMethod", "POST"}
            };
            var features = new FeatureObject(new OwinFeatureCollection(env));

            Assert.Equal(Get<IHttpRequestFeature>(features).Method, "POST");
        }

        [Fact]
        public void ImplementedInterfacesAreEnumerated()
        {
            var env = new Dictionary<string, object>
            {
                {"owin.RequestMethod", "POST"}
            };
            var features = new FeatureObject(new OwinFeatureCollection(env));

            var entries = features.ToArray();
            var keys = features.Keys.ToArray();
            var values = features.Values.ToArray();

            Assert.Contains(typeof(IHttpRequestFeature), keys);
            Assert.Contains(typeof(IHttpResponseFeature), keys);
        }
    }
}
