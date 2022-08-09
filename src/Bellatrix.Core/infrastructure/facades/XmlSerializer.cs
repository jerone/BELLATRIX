﻿// <copyright file="XmlSerializer.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Xml = System.Xml.Serialization;

namespace Bellatrix.Infrastructure
{
    public class XmlSerializer
    {
        public string Serialize<TEntity>(TEntity entityToBeSerialized)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = false,
                OmitXmlDeclaration = false,
            };
            var xmlSerializer = new Xml.XmlSerializer(typeof(TEntity));
            string result;

            using (var stringWriter = new StringWriter())
            {
                using var writer = XmlWriter.Create(stringWriter, settings);
                xmlSerializer.Serialize(writer, entityToBeSerialized);
                result = stringWriter.ToString();
            }

            result = result.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
            result = result.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty);
            var doc = XDocument.Parse(result);

            return doc.ToString();
        }

        public TEntity Deserialize<TEntity>(string content)
        {
            var serializer = new Xml.XmlSerializer(typeof(TEntity));
            var reader = new StringReader(content);
#pragma warning disable CA5369 // Use XmlReader For Deserialize
            var testRun = (TEntity)serializer.Deserialize(reader);
#pragma warning restore CA5369 // Use XmlReader For Deserialize
            reader.Close();

            return testRun;
        }
    }
}