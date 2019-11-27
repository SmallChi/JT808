using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808HeaderMessageBodyPropertyTest
    {
        [Fact]
        public void Test1()
        {
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty();
            jT808HeaderMessageBodyProperty.DataLength = 255;
            jT808HeaderMessageBodyProperty.IsPackage = true;
            jT808HeaderMessageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
            var result = jT808HeaderMessageBodyProperty.Wrap();
            Assert.Equal(9471, result);
        }

        [Fact]
        public void Test2()
        {
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty(9471);
            Assert.Equal(255, jT808HeaderMessageBodyProperty.DataLength);
            Assert.True(jT808HeaderMessageBodyProperty.IsPackage);
            Assert.Equal(JT808EncryptMethod.RSA, jT808HeaderMessageBodyProperty.Encrypt);
        }

        [Fact]
        public void Test2019_1()
        {
            //01 1 001 0011111111‬
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty();
            jT808HeaderMessageBodyProperty.DataLength = 255;
            jT808HeaderMessageBodyProperty.IsPackage = true;
            jT808HeaderMessageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
            jT808HeaderMessageBodyProperty.VersionFlag = true;
            var result = jT808HeaderMessageBodyProperty.Wrap();
            Assert.Equal(25855, result);
        }

        [Fact]
        public void Test2019_2()
        {
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty(25855);
            Assert.Equal(255, jT808HeaderMessageBodyProperty.DataLength);
            Assert.True(jT808HeaderMessageBodyProperty.IsPackage);
            Assert.True(jT808HeaderMessageBodyProperty.VersionFlag);
            Assert.Equal(JT808EncryptMethod.RSA, jT808HeaderMessageBodyProperty.Encrypt);
        }

        [Fact]
        public void Test2019_3()
        {
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty();
            jT808HeaderMessageBodyProperty.DataLength = 255;
            jT808HeaderMessageBodyProperty.IsPackage = false;
            jT808HeaderMessageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
            jT808HeaderMessageBodyProperty.VersionFlag = true;
            ushort result = jT808HeaderMessageBodyProperty.Wrap();
            Assert.Equal(17663, result);
        }
        [Fact]
        public void Test2019_4()
        {
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty(17663);
            Assert.Equal(255, jT808HeaderMessageBodyProperty.DataLength);
            Assert.False(jT808HeaderMessageBodyProperty.IsPackage);
            Assert.True(jT808HeaderMessageBodyProperty.VersionFlag);
            Assert.Equal(JT808EncryptMethod.RSA, jT808HeaderMessageBodyProperty.Encrypt);
        }
    }
}
